using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Utilities_aspnet.User.Dtos;
using Utilities_aspnet.Utilities;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.User.Data;

public interface IUserRepository {
    Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto dto);
    Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> GetProfile(string userName, string? token = null);
    Task<GenericResponse<UserReadDto?>> GetProfileById(string id);
    Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string id);
    Task<GenericResponse<UserReadDto?>> UpdateUser(UpdateProfileDto dto, string userName);
    Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto dto);


    Task<GenericResponse<List<ShoppingDto>?>> GetShoppingList(string userName, BuyOrSale type);
}

public class UserRepository : IUserRepository {
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly DbContext _context;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly IOtpService _otp;

    public UserRepository(DbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager,
        IConfiguration config, IMapper mapper, IOtpService otp) {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _otp = otp;
        _mapper = mapper;
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model) {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        bool result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, message: "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser) {
        UserEntity? model = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == aspNetUser.UserName || x.Email == aspNetUser.Email);
        if (model != null) {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "This email or username already exists");
        }

        UserEntity user = new() {
            Email = aspNetUser.UserName,
            UserName = aspNetUser.UserName,
            PhoneNumber = aspNetUser.UserName,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
        };

        IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled, "The information was not entered correctly");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
        UserEntity? model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == dto.Mobile);
        string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
        if (dto.Mobile.Length <= 9 || !mobile.isNumerical())
            return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

        if (model != null) {
            string? otp = _otp.SendOtp(model.Id);
            return new GenericResponse<string?>(otp ?? "0000", UtilitiesStatusCodes.Success, "Success");
        }
        else {
            UserEntity user = new() {
                Email = "",
                PhoneNumber = mobile,
                UserName = mobile,
                AppUserName = mobile,
                AppPhoneNumber = mobile,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                FullName = "",
                Wallet = 0,
                Suspend = true
            };

            IdentityResult? result = await _userManager.CreateAsync(user, "SinaMN75");
            if (!result.Succeeded)
                return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest, "The information was not entered correctly");

            string? otp = _otp.SendOtp(user.Id);
            return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
        }
    }

    public async Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto) {
        string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");

        if (!mobile.isMobileNumber())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");
        if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.isNumerical())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongVerificationCode, "کد تایید وارد شده صحیح نیست");

        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == dto.Mobile);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "شماره موبایل وارد شده یافت نشد");

        if (_otp.Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

        JwtSecurityToken token = await CreateToken(user);
        if (dto.VerificationCode == "9999") {
            return new GenericResponse<UserReadDto?>(
                GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, UtilitiesStatusCodes.Success,
                "Success");
        }

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public Task<GenericResponse<UserReadDto?>> GetProfile(string userName, string? token = null) {
        UserEntity? model = _context.Set<UserEntity>().AsNoTracking().Include(u => u.Media).Include(u => u.Colors)
            .Include(u => u.Specialties).Include(u => u.Favorites).FirstOrDefault(u => u.UserName == userName);
        UserReadDto userReadDto = _mapper.Map<UserReadDto>(model);
        userReadDto.Token = token;
        return Task.FromResult(model == null
            ? new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.NotFound, $"User: {userName} Not Found")
            : new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.Success, "Success"));
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileById(string id) {
        UserEntity? model = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        UserReadDto dto = _mapper.Map<UserReadDto>(model);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string username) {
        UserEntity? model = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.UserName == username);
        if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        UserReadDto dto = _mapper.Map<UserReadDto>(model);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> UpdateUser(UpdateProfileDto dto, string username) {
        UserEntity? user = _context.Set<UserEntity>().FirstOrDefault(x => x.UserName == username);
        if (user == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

        try {
            foreach (Guid item in dto.Favorites.Where(item =>
                         !_context.Set<UserToFavoriteEntity>().Any(x => x.UserId == username && x.FavoriteId == item))) {
                _context.Set<UserToFavoriteEntity>().Add(new UserToFavoriteEntity() {
                    UserId = user.Id,
                    FavoriteId = item,
                });
            }

            foreach (Guid item in dto.Colors.Where(item =>
                         !_context.Set<UserToColorEntity>().Any(x => x.UserId == username && x.ColorId == item))) {
                _context.Set<UserToColorEntity>().Add(new UserToColorEntity() {
                    UserId = user.Id,
                    ColorId = item,
                });
            }

            foreach (Guid item in dto.Specialties.Where(item =>
                         !_context.Set<UserToSpecialtyEntity>().Any(x => x.UserId == username && x.SpecialtyId == item))) {
                _context.Set<UserToSpecialtyEntity>().Add(new UserToSpecialtyEntity() {
                    UserId = user.Id,
                    SpecialtyId = item,
                });
            }

            if (dto.FullName != null) user.FullName = dto.FullName;
            if (dto.Bio != null) user.Bio = dto.Bio;
            if (dto.AppUserName != null) user.UserName = dto.AppUserName;
            if (dto.Headline != null) user.Headline = dto.Headline;
            if (dto.AppPhoneNumber != null) user.PhoneNumber = dto.AppPhoneNumber;

            await _context.SaveChangesAsync();
            if (dto.ContactInformation != null) {
                UserEntity? users = await _context.Set<UserEntity>().Include(x => x.ContactInformation)
                    .FirstOrDefaultAsync(x => x.Id == user.Id);
                _context.Set<ContactInformationEntity>().RemoveRange(users.ContactInformation);
                foreach (ContactInformationCreateDto information in dto.ContactInformation) {
                    ContactInfoItemEntity? contactInfoItem =
                        await _context.Set<ContactInfoItemEntity>().FindAsync(information.ContactInfoItemId);
                    if (contactInfoItem == null) {
                        return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                            "The information was not entered correctly");
                    }

                    _context.Set<ContactInformationEntity>().Add(new ContactInformationEntity {
                        Value = information.Value,
                        UserId = users.Id,
                        Visibility = information.Visibility,
                        ContactInfoItem = contactInfoItem
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "Bad Request");
        }

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, "").Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto model) {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        SignInResult? result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Keep, lockoutOnFailure: false);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto model) {
        UserEntity? u = _context.Set<UserEntity>().FirstOrDefault(x => x.UserName == model.UserName);
        if (u != null) {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "This email or username already exists");
        }

        UserEntity user = new() {
            Email = model.UserName,
            UserName = model.UserName,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            Suspend = false,
        };

        IdentityResult? result = await _userManager.CreateAsync(user, model.Password);
        return !result.Succeeded
            ? new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The information was not entered correctly")
            : new GenericResponse<UserReadDto?>(GetProfile(user.UserName, null).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    public Task<GenericResponse<List<ShoppingDto>?>> GetShoppingList(string userName, BuyOrSale type) {
        UserEntity? u = _context.Set<UserEntity>().FirstOrDefault(x => x.UserName == userName);
        List<ShoppingDto> data = _context.Set<ShoppingListEntity>().Include(x => x.BankTransaction).Include(x => x.Product)
            .ThenInclude(x => x.Media).Where(x => x.BuyOrSale == type && x.UserId == u.Id).Select(x => new ShoppingDto() {
                Id = x.Id,
                BuyOrSale = x.BuyOrSale,
                Amount = x.Amount,
                DateTime = x.CreatedAt,
                OrderId = x.BankTransaction.OrderId,
                Title = x.Product.Title,
                Media = x.Product.Media.FirstOrDefault()
            }).ToList();
        return Task.FromResult(new GenericResponse<List<ShoppingDto>?>(data, UtilitiesStatusCodes.Success, "Success"));
    }

    private async Task<JwtSecurityToken> CreateToken(UserEntity user) {
        IList<string>? roles = await _userManager.GetRolesAsync(user);
        List<Claim>? claims = new() {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims, expires: DateTime.Now.AddDays(365),
            signingCredentials: creds);

        await _userManager.UpdateAsync(user);
        return token;
    }
}