using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Utilities_aspnet.User;

public interface IUserRepository {
    Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto dto);
    Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto);
    Task<GenericResponse<UserReadDto?>> GetProfile(string id, string? token = null);
    Task<GenericResponse<UserReadDto?>> GetProfileById(string id);
    Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string id);
    Task<GenericResponse<UserReadDto?>> UpdateUser(UserCreateUpdateDto dto);
    Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto dto);
    Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto dto);
    Task<GenericResponse<IEnumerable<UserReadDto>>> GetUsers();
    Task<GenericResponse<UserReadDto?>> CreateUser(UserCreateUpdateDto parameter);
    Task<GenericResponse> DeleteUser(string id);
    Task<GenericResponse<UserReadDto?>> GetTokenForTest(string mobile);
    Task<GenericResponse<UserReadDto?>> RegisterByMobile(RegisterByMobileDto dto);
    Task<GenericResponse<UserReadDto?>> LoginWithMobileOrUserName(LoginWithMobileOrUserNameDto dto);
}

public class UserRepository : IUserRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IOtpService _otp;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly ISmsSender _smsSender;

    public UserRepository(
        DbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        IConfiguration config,
        IMapper mapper,
        IOtpService otp,
        ISmsSender smsSender)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _otp = otp;
        _mapper = mapper;
        _smsSender = smsSender;
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model) {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        bool result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser) {
        UserEntity? model = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == aspNetUser.UserName || x.Email == aspNetUser.Email);
        if (model != null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "This email or username already exists");

        UserEntity user = new() {
            Email = aspNetUser.Email,
            UserName = aspNetUser.UserName,
            PhoneNumber = aspNetUser.UserName,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false
        };

        IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
                "The information was not entered correctly");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
        UserEntity? model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == dto.Mobile);
        string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
        if (dto.Mobile.Length <= 9 || !mobile.isNumerical())
            return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

        if (model != null) {
            string? otp = "9999";
            if (dto.SendSMS) otp = _otp.SendOtp(model.Id ,4);
            return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
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
                Suspend = false
            };

            IdentityResult? result = await _userManager.CreateAsync(user, "SinaMN75");
            if (!result.Succeeded)
                return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest,
                    "The information was not entered correctly");

            string? otp = "9999";
            if (dto.SendSMS) otp = _otp.SendOtp(user.Id , 4);
            return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
        }
    }

    public async Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto) {
        string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");

        if (!mobile.isMobileNumber())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");
        if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.isNumerical())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongVerificationCode,
                "کد تایید وارد شده صحیح نیست");

        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == dto.Mobile);

        if (user == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "شماره موبایل وارد شده یافت نشد");

        if (user.Suspend)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کاربر به حالت تعلیق در آمده است");

        JwtSecurityToken token = await CreateToken(user);
        if (dto.VerificationCode == "9999")
            return new GenericResponse<UserReadDto?>(
                GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
                UtilitiesStatusCodes.Success, "Success"
            );


        if (_otp.Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success"
        );
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfile(string id, string? token = null) {
        UserEntity? model = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Include(u => u.Media)
            .Include(u => u.Colors)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (model == null)
            return new GenericResponse<UserReadDto?>(new UserReadDto(), UtilitiesStatusCodes.NotFound,
                $"User: {id} Not Found");

        UserReadDto? userReadDto = _mapper.Map<UserReadDto>(model);
        userReadDto.IsAdmin = await _userManager.IsInRoleAsync(model, "Admin");
        userReadDto.Token = token;
        return new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileById(string id) {
        UserEntity? model = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        UserReadDto? dto = _mapper.Map<UserReadDto>(model);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string username) {
        UserEntity? entity = await _context.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.UserName == username);
        if (entity == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
        UserReadDto? dto = _mapper.Map<UserReadDto>(entity);
        return new GenericResponse<UserReadDto?>(dto);
    }

    public async Task<GenericResponse<UserReadDto?>> UpdateUser(UserCreateUpdateDto dto) {
        UserEntity? entity = _context.Set<UserEntity>().FirstOrDefault(x => x.Id == dto.Id);

        if (entity == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

        FillUserData(dto, entity);

        await _context.SaveChangesAsync();
        GenericResponse<UserReadDto?> readDto = await GetProfile(entity.Id, "");
        return readDto;
    }

    public async Task<GenericResponse<UserReadDto?>> LoginFormWithEmail(LoginWithEmailDto model) {
        UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Email not found");

        SignInResult? result =
            await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Keep, false);
        return !result.Succeeded
            ? new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!")
            : new GenericResponse<UserReadDto?>(GetProfile(user.Id).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterFormWithEmail(RegisterFormWithEmailDto model) {
        UserEntity? u = _context.Set<UserEntity>().FirstOrDefault(x => x.Email == model.Email);
        if (u != null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "This email or username already exists");

        UserEntity user = new() {
            Email = model.Email,
            UserName = model.Email,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            Suspend = false
        };

        IdentityResult? result = await _userManager.CreateAsync(user, model.Password);
        return !result.Succeeded
            ? new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "The information was not entered correctly")
            : new GenericResponse<UserReadDto?>(GetProfile(user.Id).Result.Result, UtilitiesStatusCodes.Success, "Success");
    }

    private async Task<JwtSecurityToken> CreateToken(UserEntity user) {
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

    public async Task<GenericResponse<IEnumerable<UserReadDto>>> GetUsers() {
        List<UserEntity> users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Include(u => u.Media)
            .Include(u => u.Colors)
            .ToListAsync();

        IEnumerable<UserReadDto>? result = _mapper.Map<IEnumerable<UserReadDto>>(users);

        return new GenericResponse<IEnumerable<UserReadDto>>(result);
    }

    public async Task<GenericResponse> DeleteUser(string id) {
        UserEntity? user = await _context.Set<UserEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return new GenericResponse(UtilitiesStatusCodes.NotFound, "User notfound");

        user.DeletedAt = DateTime.Now;

        _context.Set<UserEntity>().Update(user);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }

    public async Task<GenericResponse<UserReadDto?>> GetTokenForTest(string mobile) {
        if (!mobile.isMobileNumber())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == mobile);

        if (user == null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "شماره موبایل وارد شده یافت نشد");

        if (user.Suspend)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کاربر به حالت تعلیق در آمده است");

        JwtSecurityToken token = await CreateToken(user);
        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success"
        );
    }

    public async Task<GenericResponse<UserReadDto?>> CreateUser(UserCreateUpdateDto dto) {
        UserEntity? entity = _mapper.Map<UserEntity>(dto);

        FillUserData(dto, entity);

        await _context.Set<UserEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return await GetProfileById(entity.Id);
    }

    private async void FillUserData(UserCreateUpdateDto dto, UserEntity entity) {
        entity.FirstName = dto.FirstName ?? entity.FirstName;
        entity.LastName = dto.LastName ?? entity.LastName;
        entity.FullName = dto.FullName ?? entity.FullName;
        entity.Bio = dto.Bio ?? entity.Bio;
        entity.AppUserName = dto.AppUserName ?? entity.AppUserName;
        entity.AppEmail = dto.AppEmail ?? entity.AppEmail;
        entity.Region = dto.Region ?? entity.Region;
        entity.Activity = dto.Activity ?? entity.Activity;
        entity.Suspend = dto.Suspend ?? entity.Suspend;
        entity.Headline = dto.Headline ?? entity.Headline;
        entity.AppPhoneNumber = dto.AppPhoneNumber ?? entity.AppPhoneNumber;
        entity.Birthdate = dto.BirthDate ?? entity.Birthdate;
        entity.Wallet = dto.Wallet ?? entity.Wallet;
        entity.IdentityType = dto.IdentityType ?? entity.IdentityType;

        if (dto.Colors.IsNotNullOrEmpty()) {
            List<ColorEntity> list = new();
            foreach (Guid item in dto.Colors ?? new List<Guid>()) {
                ColorEntity? e = await _context.Set<ColorEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Colors = list;
        }
        
        if (dto.Locations.IsNotNullOrEmpty()) {
            List<LocationEntity> list = new();
            foreach (int item in dto.Locations ?? new List<int>()) {
                LocationEntity? e = await _context.Set<LocationEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Location = list;
        }

        if (dto.Specialties.IsNotNullOrEmpty()) {
            List<SpecialityEntity> list = new();
            foreach (Guid item in dto.Specialties ?? new List<Guid>()) {
                SpecialityEntity? e = await _context.Set<SpecialityEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Specialties = list;
        }

        #region TODO - Refactor

        //if (dto.Media != null && dto.Media.Count() != 0)
        //{
        //    List<MediaEntity> list = new();
        //    foreach (var item in dto.Media ?? new List<UploadDto>())
        //    {
        //        MediaEntity? e = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == Guid.Parse(item.UserId));
        //        if (e != null) list.Add(e);
        //    }

        //    entity.Media = list;
        //}

        #endregion
    }

    public async Task<GenericResponse<UserReadDto?>> RegisterByMobile(RegisterByMobileDto dto)
    {
        string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
        if (dto.Mobile.Length <= 9 || !mobile.isNumerical())
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده معتبر نیست");

        if(dto.Password != dto.ConfirmPassword)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "کلمه عبور با تکرار آن مطابفت ندارد");

        UserEntity? model = _context.Set<UserEntity>()
            .FirstOrDefault(x => x.UserName == dto.UserName || x.PhoneNumber == dto.Mobile);
        if (model != null)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
                "اطلاعات وارد شده قبلا به ثبت رسیده است");

        var confirmationCode = new Random().Next(10000, 99999).ToString();

        UserEntity user = new()
        {
            UserName = dto.UserName,
            PhoneNumber = dto.Mobile,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
        };

        IdentityResult? result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
                "اطلاعات وارد شده معتبر نیست");

        JwtSecurityToken token = await CreateToken(user);

        _smsSender.SendSms(dto.Mobile, $"کد فعال سازی شما : {confirmationCode}");

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }

    public async Task<GenericResponse<UserReadDto?>> LoginWithMobileOrUserName(LoginWithMobileOrUserNameDto dto)
    {
        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == dto.Mobile || x.UserName == dto.UserName);

        if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "اطلاعات وارد شده معتبر نیست");

        bool result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result)
            return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "اطلاعات وارد شده معتبر نیست");

        JwtSecurityToken token = await CreateToken(user);

        return new GenericResponse<UserReadDto?>(
            GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
            UtilitiesStatusCodes.Success, "Success");
    }
}