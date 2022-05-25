using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Utilities_aspnet.User.Data;

public interface IUserRepository
{
    Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto dto);
    Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> GetProfile(string id, string? token = null);
    Task<GenericResponse<UserReadDto?>> GetProfileById(string id);
    Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string id);
    Task<GenericResponse<UserReadDto?>> UpdateUser(UpdateProfileDto dto, string userName);
    Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto dto);
}

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IOtpService _otp;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;

    public UserRepository(
        DbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        IConfiguration config,
        IMapper mapper,
        IOtpService otp)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _otp = otp;
        _mapper = mapper;
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        var token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser)
    {
        var model = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == aspNetUser.UserName || x.Email == aspNetUser.Email);
        if (model != null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "This email or username already exists");

        UserEntity user = new()
        {
            Email = aspNetUser.Email,
            UserName = aspNetUser.UserName,
            PhoneNumber = aspNetUser.UserName,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, aspNetUser.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
                "The information was not entered correctly");

        var token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto)
    {
        var model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == dto.Mobile);
        var mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
        if (dto.Mobile.Length <= 9 || !mobile.isNumerical())
            return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

        if (model != null)
        {
            var otp = "9999";
            if (dto.SendSMS) otp = _otp.SendOtp(model.Id);
            return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
        }
        else
        {
            UserEntity user = new()
            {
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

            var result = await _userManager.CreateAsync(user, "SinaMN75");
            if (!result.Succeeded)
                return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest,
                    "The information was not entered correctly");

            var otp = "9999";
            if (dto.SendSMS) otp = _otp.SendOtp(user.Id);
            return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
        }
    }

    public async Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto)
    {
        var mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");

        if (!mobile.isMobileNumber())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");
        if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.isNumerical())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongVerificationCode,
                "کد تایید وارد شده صحیح نیست");

        var user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == dto.Mobile);

        if (user == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "شماره موبایل وارد شده یافت نشد");

        var token = await CreateToken(user);
        if (dto.VerificationCode == "9999")
            return new GenericResponse<UserReadDto?>(
                GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
                UtilitiesStatusCodes.Success, "Success"
            );


        if (_otp.Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.UserName, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success"
        );
    }

    public Task<GenericResponse<UserReadDto?>> GetProfile(string id, string? token = null)
    {
        var model = _context.Set<UserEntity>()
            .AsNoTracking()
            .Include(u => u.Media)
            .Include(u => u.Colors)
            .Include(u => u.Favorites)
            .FirstOrDefault(u => u.Id == id);
        var userReadDto = _mapper.Map<UserReadDto>(model);
        userReadDto.Token = token;
        return Task.FromResult(model == null
            ? new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.NotFound, $"User: {id} Not Found")
            : new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.Success, "Success"));
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileById(string id)
    {
        var model = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        var dto = _mapper.Map<UserReadDto>(model);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string username)
    {
        var model = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.UserName == username);
        if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        var dto = _mapper.Map<UserReadDto>(model);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> UpdateUser(UpdateProfileDto dto, string username)
    {
        var user = _context.Set<UserEntity>()
            .Include(x => x.Colors)
            .Include(x => x.Location)
            .Include(x => x.Media)
            .Include(x => x.Specialties)
            .Include(x => x.ContactInformation)
            .FirstOrDefault(x => x.UserName == username);

        if (user == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

        try
        {
            if (!string.IsNullOrEmpty(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Bio))
                user.Bio = dto.Bio;

            if (!string.IsNullOrEmpty(dto.AppUserName))
                user.UserName = dto.AppUserName;

            if (!string.IsNullOrEmpty(dto.Headline))
                user.Headline = dto.Headline;

            if (!string.IsNullOrEmpty(dto.AppPhoneNumber))
                user.PhoneNumber = dto.AppPhoneNumber;

            if (dto.BirthDate.HasValue)
                user.Birthdate = dto.BirthDate.GetValueOrDefault();

            if (!string.IsNullOrEmpty(dto.AppEmail))
                user.Email = dto.AppEmail;

            if (dto.Colors.Any())
            {
                _context.Set<ColorEntity>().RemoveRange(user.Colors);

                var colors = await _context.Set<ColorEntity>()
                    .AsNoTracking()
                    .Where(x => dto.Colors.Contains(x.Id))
                    .ToListAsync();

                user.Colors.AddRange(colors);
            }

            if (dto.Locations.Any())
            {
                _context.Set<LocationEntity>().RemoveRange(user.Location);

                var locations = await _context.Set<LocationEntity>()
                    .AsNoTracking()
                    .Where(x => dto.Locations.Contains(x.Id))
                    .ToListAsync();

                user.Location.AddRange(locations);
            }

            if (dto.Specialties.Any())
            {
                _context.Set<SpecialityEntity>().RemoveRange(user.Specialties);

                var specialties = await _context.Set<SpecialityEntity>()
                    .AsNoTracking()
                    .Where(x => dto.Specialties.Contains(x.Id))
                    .ToListAsync();

                user.Specialties.AddRange(specialties);
            }

            if (dto.Media != null)
            {
                var media = _mapper.Map<MediaEntity>(dto.Media);
                user.Media.Add(media);
            }

            if (dto.ContactInformation != null)
            {
                _context.Set<ContactInformationEntity>().RemoveRange(user.ContactInformation);

                dto.ContactInformation.ForEach(x =>
                {
                    _context.Set<ContactInformationEntity>().Add(new ContactInformationEntity()
                    {
                        UserId = user.Id,
                        Link = x.Link,

                    });
                });
            }

            await _context.SaveChangesAsync();
        }
        catch
        {
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "Bad Request");
        }

        return new GenericResponse<UserReadDto?>(GetProfile(user.UserName, "").Result.Result, UtilitiesStatusCodes.Success,
            "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        var result =
            await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Keep, false);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        return new GenericResponse<UserReadDto?>(GetProfile(user.Id).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto model)
    {
        var u = _context.Set<UserEntity>().FirstOrDefault(x => x.Email == model.Email);
        if (u != null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "This email or username already exists");

        UserEntity user = new()
        {
            Email = model.Email,
            UserName = model.Email,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            Suspend = false
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        return !result.Succeeded
            ? new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "The information was not entered correctly")
            : new GenericResponse<UserReadDto?>(GetProfile(user.Id).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    private async Task<JwtSecurityToken> CreateToken(UserEntity user)
    {
        IList<string>? roles = await _userManager.GetRolesAsync(user);
        List<Claim>? claims = new() {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("https://SinaMN75.com"));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new("https://SinaMN75.com", "https://SinaMN75.com", claims,
            expires: DateTime.Now.AddDays(365),
            signingCredentials: creds);

        await _userManager.UpdateAsync(user);
        return token;
    }
}