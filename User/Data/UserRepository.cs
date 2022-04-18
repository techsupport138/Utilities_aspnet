using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities_aspnet.User.Dtos;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.User.Data;

public interface IUserRepository
{
    Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto dto);
    Task<GenericResponse<string>> RegisterWithMobile(RegisterWithMobileDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithMobile(LoginWithMobileDto dto);
    Task<GenericResponse<UserReadDto?>> GetProfile(string userName, string? token = null);
    Task<GenericResponse<UserReadDto?>> UpdateUser(UpdateProfileDto model, string userName);


    Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto dto);


    Task<GenericResponse<List<ShoppingDto>?>> GetShoppingList(string userName, BuyOrSale type);
}

public class UserRepository : IUserRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly DbContext _context;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly IOtpService _otp;

    public UserRepository(DbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        IConfiguration config, IMapper mapper, IOtpService otp)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _otp = otp;
        _mapper = mapper;
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model)
    {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        bool result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            status: UtilitiesStatusCodes.Success,
            message: "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithMobile(LoginWithMobileDto model)
    {
        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == model.Mobile);

        if (user == null) return new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.NotFound, "Mobile not found");

        if (_otp.Verify(user.Id, model.VerificationCode) != OtpResult.Ok)
            return new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.BadRequest, "Verification Code Is Not Valid");
        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success,
            "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser)
    {
        UserEntity? model = _context.Set<UserEntity>()
            .FirstOrDefault(x =>
            x.UserName == aspNetUser.UserName ||
            x.Email == aspNetUser.Email);
        if (model != null)
        {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "This email or username already exists");
        }

        UserEntity user = new()
        {
            Email = aspNetUser.UserName,
            UserName = aspNetUser.UserName,
            LastLogin = null,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            CreateAccount = DateTime.Now
        };

        IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled, "The information was not entered correctly");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<string>> RegisterWithMobile(RegisterWithMobileDto aspNetUser)
    {
        UserEntity? model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == aspNetUser.Mobile);
        if (model != null)
        {
            string? otp = _otp.SendOtp(model.Id);
            return new GenericResponse<string>(otp, UtilitiesStatusCodes.Success, "Success");
        }
        else
        {
            UserEntity user = new()
            {
                Email = "",
                PhoneNumber = aspNetUser.Mobile,
                UserName = aspNetUser.Mobile.Replace("+98", "0").Replace("+", ""),
                LastLogin = null,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreateAccount = DateTime.Now,
                FullName = "",
                Wallet = 0,
                Suspend = true
            };

            IdentityResult? result = await _userManager.CreateAsync(user, "P@ssw0rd!@#$%^&*");
            if (!result.Succeeded)
                return new GenericResponse<string>("", UtilitiesStatusCodes.BadRequest, "The information was not entered correctly");

            string? otp = _otp.SendOtp(user.Id);
            return new GenericResponse<string>(otp, UtilitiesStatusCodes.Success, "Success");
        }
    }

    public Task<GenericResponse<UserReadDto?>> GetProfile(string userName, string? token = null)
    {
        UserEntity? model = _context.Set<UserEntity>()
            .Include(u => u.Media)
            .Include(u => u.Colors)
            .Include(u => u.Specialties)
            .Include(u => u.Favorites)
            .FirstOrDefault(u => u.UserName == userName);
        //UserReadDto userReadDto = _mapper.Map<UserReadDto>(model);
        UserReadDto u = new UserReadDto();
        if(model == null)
        {
            return Task.FromResult(new GenericResponse<UserReadDto?>(u, UtilitiesStatusCodes.NotFound, $"User: {userName} Not Found"));
        }
        u.UserName = model.UserName;
        u.CreatedAt = model.CreateAccount;
        u.BirthDate = model.Birthday;
        u.Birth_Day = model.Birth_Day;
        u.Birth_Year = model.Birth_Year;
        u.Birth_Month = model.Birth_Month;
        u.Instagram = model.Instagram;
        u.Link = model.Link;
        u.Bio = model.Bio;
        u.Colors = model.Colors.Select(x => x.Id).ToList();
        u.Specialties = model.Specialties.Select(x => x.Id).ToList();
        u.Favorites = model.Favorites.Select(x => x.Id).ToList();
        u.WebSite = model.WebSite;
        u.Email = model.Email;
        u.FullName = model.FullName;
        u.PhoneNumber = model.PhoneNumber;
        u.FullName = model.FullName;
        u.Id = model.Id;
        u.PublicBio= model.PublicBio;

        u.Token = token;

        return Task.FromResult(new GenericResponse<UserReadDto?>(u, UtilitiesStatusCodes.Success, "Success"));
    }


    public async Task<GenericResponse<UserReadDto?>>
        UpdateUser(UpdateProfileDto model, string userName)
    {
        UserEntity? user = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.Id == userName);
        if (user == null)
        {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");
        }

        try
        {
            foreach (var item in model.Favorites)
            {
                if (!_context.Set<UserToFavoriteEntity>()
                    .Any(x => x.UserId == userName && x.FavoriteId == item))
                    _context.Set<UserToFavoriteEntity>()
                        .Add(new UserToFavoriteEntity()
                        {
                            UserId = user.Id,
                            FavoriteId = item,
                        });
            }

            foreach (var item in model.Colors)
            {
                if (!_context.Set<UserToColorEntity>()
                    .Any(x => x.UserId == userName && x.ColorId == item))
                    _context.Set<UserToColorEntity>()
                        .Add(new UserToColorEntity()
                        {
                            UserId = user.Id,
                            ColorId = item,
                        });
            }

            foreach (var item in model.Specialties)
            {
                if (!_context.Set<UserToSpecialtyEntity>()
                    .Any(x => x.UserId == userName && x.SpecialtyId == item))
                    _context.Set<UserToSpecialtyEntity>()
                        .Add(new UserToSpecialtyEntity()
                        {
                            UserId = user.Id,
                            SpecialtyId = item,
                        });
            }

            if (model.FullName != null) user.FullName = model.FullName;
            if (model.Bio != null) user.Bio = model.Bio;
            //if (model.BirthDate != null) user.BirthDate = model.BirthDate;
            if (model.UserName != null) user.UserName = model.UserName;
            //if (model.LocationId != null) user.LocationId = model.LocationId;
            if (model.Degree != null) user.Degree = model.Degree;
            if (model.Education != null) user.Education = model.Education;
            if (model.Headline != null) user.Headline = model.Headline;

            if (model.WebSite != null) user.WebSite = model.WebSite;
            if (model.Instagram != null) user.Instagram = model.Instagram;
            if (model.Telegram != null) user.Telegram = model.Telegram;
            if (model.PhoneNumber != null) user.PhoneNumber = model.PhoneNumber;
            if (model.Link != null) user.Link = model.Link;
            if (model.PublicBio != null) user.PublicBio = model.PublicBio ?? true;

            if (model.ColorId != null) user.ColorId = model.ColorId;

            if (model.Birth_Year != null) user.Birth_Year = model.Birth_Year;
            if (model.Birth_Month != null) user.Birth_Month = model.Birth_Month;
            if (model.Birth_Day != null) user.Birth_Day = model.Birth_Day;

            _context.SaveChanges();
            if (model.ContactInformation != null)
            {
                UserEntity? users = await _context.Set<UserEntity>().Include(x => x.ContactInformation)
                    .FirstOrDefaultAsync(x => x.Id == user.Id);
                _context.Set<ContactInformationEntity>().RemoveRange(users.ContactInformation);
                foreach (ContactInformationCreateDto information in model.ContactInformation)
                {
                    ContactInfoItemEntity? contactInfoItem = _context.Set<ContactInfoItemEntity>().Find(information.ContactInfoItemId);
                    if (contactInfoItem == null)
                    {
                        return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                            "The information was not entered correctly");
                    }

                    _context.Set<ContactInformationEntity>().Add(new ContactInformationEntity
                    {
                        Value = information.Value,
                        UserId = users.Id,
                        Visibility = information.Visibility,
                        ContactInfoItem = contactInfoItem
                    });
                    _context.SaveChanges();
                }
            }
        }
        catch
        {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "Bad Request");
        }


        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, "").Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    private async Task<JwtSecurityToken> CreateToken(UserEntity user)
    {
        IList<string>? roles = await _userManager.GetRolesAsync(user);
        List<Claim>? claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims, expires: DateTime.Now.AddDays(365),
            signingCredentials: creds);

        user.LastLogin = DateTime.Now;
        await _userManager.UpdateAsync(user);
        return token;
    }

    public async Task<GenericResponse<UserReadDto?>>
        LoginFormWithEmail(LoginWithEmailDto model)
    {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        if (user.Suspend) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Forbidden, "User Suspend");

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Keep, lockoutOnFailure: false);
        if (!result.Succeeded) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, null).Result.Result,
            status: UtilitiesStatusCodes.Success,
            message: "Success");
    }

    public async Task<GenericResponse<UserReadDto?>>
        RegisterFormWithEmail(RegisterFormWithEmailDto model)
    {
        UserEntity? u = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == model.UserName);
        if (u != null)
        {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "This email or username already exists");
        }

        UserEntity user = new()
        {
            Email = model.UserName,
            UserName = model.UserName,
            LastLogin = null,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            CreateAccount = DateTime.Now,
            Suspend = false,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The information was not entered correctly");

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, null).Result.Result,
            UtilitiesStatusCodes.Success,
            "Success");
    }

    public Task<GenericResponse<List<ShoppingDto>?>>
        GetShoppingList(string userName, BuyOrSale type)
    {
        UserEntity? u = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == userName);
        var data = _context.Set<ShoppingListEntity>()
            .Include(x => x.BankTransaction)
            .Include(x => x.Product).ThenInclude(x => x.Media)
            .Where(x => x.BuyOrSale == type && x.UserId == u.Id)
            .Select(x => new ShoppingDto()
            {
                Id = x.Id,
                BuyOrSale = x.BuyOrSale,
                Amount = x.Amount,
                DateTime = x.CreatedAt,
                OrderId = x.BankTransaction.OrderId,
                Title = x.Product.Title,
                Media = x.Product.Media.FirstOrDefault()
            }).ToList();
        return Task.FromResult(new GenericResponse<List<ShoppingDto>?>
            (data, UtilitiesStatusCodes.Success, "Success"));
    }
}