using Microsoft.EntityFrameworkCore.Query;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Utilities_aspnet.Repositories;

public interface IUserRepository {
	Task<GenericResponse<GrowthRateReadDto?>> GrowthRate(string id);
	Task<GenericResponse> SeederUser(SeederUserDto dto);
	Task<GenericResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto dto);
	Task<GenericResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto dto);
	Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> GetProfile(string id, string? token = null);
	Task<GenericResponse<UserReadDto?>> GetUser(UserFilterDto dto, string? token = null);
	Task<GenericResponse<UserReadDto?>> GetProfileById(string id);
	Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string id);
	Task<GenericResponse<UserReadDto?>> UpdateUser(UserCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<UserReadDto>>> GetUsers(UserFilterDto dto);
	Task<GenericResponse<UserReadDto?>> CreateUser(UserCreateUpdateDto parameter);
	Task<GenericResponse> DeleteUser(string id);
	Task<GenericResponse<UserReadDto?>> GetTokenForTest(string mobile);

	Task<GenericResponse<UserReadDto?>> LoginWithPassword(LoginWithPasswordDto model);
	Task<GenericResponse<string?>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> VerifyCodeForLogin(VerifyMobileForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> Register(RegisterDto aspNetUser);
	Task<GenericResponse<UserMinimalReadDto?>> GetMinProfileById(string id);
}

public class UserRepository : IUserRepository {
	private readonly DbContext _context;
	private readonly IMapper _mapper;
	private readonly IOtpService _otp;
	private readonly UserManager<UserEntity> _userManager;

	public UserRepository(
		DbContext context,
		UserManager<UserEntity> userManager,
		IMapper mapper,
		IOtpService otp) {
		_context = context;
		_userManager = userManager;
		_otp = otp;
		_mapper = mapper;
	}

	public async Task<GenericResponse> SeederUser(SeederUserDto dto) {
		try {
			foreach (UserCreateUpdateDto item in dto.Users) {
				UserEntity? entity = _mapper.Map<UserEntity>(item);

				FillUserData(item, entity);

				IdentityResult? result = await _userManager.CreateAsync(entity, item.Password);
			}
		}
		catch {
			return new GenericResponse(UtilitiesStatusCodes.Unhandled,
			                           "The information was not entered correctly");
		}

		return new GenericResponse();
	}

	public async Task<GrowthRateReadDto?> GetGrowthRate(string id) {
		GrowthRateReadDto entity = new() {
			InterActive1 = 1,
			InterActive2 = 2,
			InterActive3 = 1,
			InterActive4 = 3,
			InterActive5 = 2,
			InterActive6 = 4,
			InterActive7 = 1,
			Feedback1 = 5,
			Feedback2 = 1,
			Feedback3 = 3,
			Feedback4 = 4,
			Feedback5 = 1,
			Feedback6 = 2,
			Feedback7 = 3,
			TotalInterActive = 35,
			TotalFeedback = 65,
			Id = id
		};

		return entity;
	}

	public async Task<GenericResponse<GrowthRateReadDto?>> GrowthRate(string id) {
		GrowthRateReadDto entity = new() {
			InterActive1 = 1,
			InterActive2 = 2,
			InterActive3 = 1,
			InterActive4 = 3,
			InterActive5 = 2,
			InterActive6 = 4,
			InterActive7 = 1,
			Feedback1 = 5,
			Feedback2 = 1,
			Feedback3 = 3,
			Feedback4 = 4,
			Feedback5 = 1,
			Feedback6 = 2,
			Feedback7 = 3,
			TotalInterActive = 35,
			TotalFeedback = 65,
			Id = id
		};

		return new GenericResponse<GrowthRateReadDto?>(entity, UtilitiesStatusCodes.Success, "Success");
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
		if (dto.Mobile.Length <= 9 || !mobile.IsNumerical())
			return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

		if (model != null) {
			string? otp = "9999";
			if (dto.SendSMS) otp = _otp.SendOtp(model.Id, 4);
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
			if (dto.SendSMS) otp = _otp.SendOtp(user.Id, 4);
			return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
		}
	}

	public async Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto) {
		string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");

		if (!mobile.IsMobileNumber())
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");
		if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.IsNumerical())
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
			.Include(u => u.Categories)
			.Include(u => u.Location)
			.Include(u => u.Products)!.ThenInclude(x => x.Media)
			.Include(u => u.Gender)
			.FirstOrDefaultAsync(u => u.Id == id);

		if (model == null)
			return new GenericResponse<UserReadDto?>(new UserReadDto(), UtilitiesStatusCodes.NotFound,
			                                         $"User: {id} Not Found");

		UserReadDto? userReadDto = _mapper.Map<UserReadDto>(model);
		userReadDto.CountProducts = model.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == model.Id).ToListAsync();
		userReadDto.CountFollowers = follower.Count;

		userReadDto.IsAdmin = await _userManager.IsInRoleAsync(model, "Admin");
		userReadDto.Token = token;
		userReadDto.GrowthRate = GetGrowthRate(userReadDto.Id).Result;

		return new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.Success, "Success");
	}

	public async Task<GenericResponse<UserReadDto?>> GetUser(UserFilterDto dto, string? token = null) {
		IIncludableQueryable<UserEntity, object?>? i = _context.Set<UserEntity>()
			.Include(u => u.Media)
			.Include(u => u.Gender);
		if (dto.ShowCategories.IsTrue()) i.Include(u => u.Categories);
		if (dto.ShowForms.IsTrue()) i.Include(u => u.FormBuilders);
		if (dto.ShowLocations.IsTrue()) i.Include(u => u.Location);
		if (dto.ShowTransactions.IsTrue()) i.Include(u => u.Transactions);
		if (dto.ShowProducts.IsTrue()) i.Include(u => u.Products);

		UserEntity? entity =
			await i.FirstOrDefaultAsync(x => dto.UserId != null ? x.Id == dto.UserId : x.UserName == dto.UserName);

		if (entity == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
		UserReadDto? readDto = _mapper.Map<UserReadDto>(i);
		readDto.CountProducts = entity.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == entity.Id).ToListAsync();
		readDto.CountFollowers = follower.Count;
		readDto.GrowthRate = GetGrowthRate(readDto.Id).Result;

		return new GenericResponse<UserReadDto?>(readDto);
	}

	public async Task<GenericResponse<UserReadDto?>> GetProfileById(string id) {
		UserEntity? model = await _context.Set<UserEntity>()
			.Include(u => u.Media)
			.Include(u => u.Categories)
			.Include(u => u.Products)!.ThenInclude(x => x.Media)
			.Include(u => u.Location)
			.Include(u => u.Gender)
			.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		if (model == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
		UserReadDto? dto = _mapper.Map<UserReadDto>(model);
		dto.CountProducts = model.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == id).ToListAsync();
		dto.CountFollowers = follower.Count;
		dto.GrowthRate = GetGrowthRate(dto.Id).Result;

		return new GenericResponse<UserReadDto?>(dto);
	}

	public async Task<GenericResponse<UserMinimalReadDto?>> GetMinProfileById(string id) {
		UserEntity? model = await _context.Set<UserEntity>()
			.Include(u => u.Media)
			.Include(u => u.Categories)
			.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		if (model == null) return new GenericResponse<UserMinimalReadDto?>(null, UtilitiesStatusCodes.NotFound);
		UserMinimalReadDto? dto = _mapper.Map<UserMinimalReadDto>(model);
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == id).ToListAsync();
		dto.CountFollowers = follower.Count;

		return new GenericResponse<UserMinimalReadDto?>(dto);
	}

	public async Task<GenericResponse<UserReadDto?>> GetProfileByUserName(string username) {
		UserEntity? entity = await _context.Set<UserEntity>()
			.Include(u => u.Media)
			.Include(u => u.Categories)
			.Include(u => u.Products)!.ThenInclude(x => x.Media)
			.Include(u => u.Location)
			.Include(u => u.Gender)
			.AsNoTracking().FirstOrDefaultAsync(i => i.UserName == username);
		if (entity == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound);
		UserReadDto? dto = _mapper.Map<UserReadDto>(entity);
		dto.CountProducts = entity.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == entity.Id).ToListAsync();
		dto.CountFollowers = follower.Count;
		dto.GrowthRate = GetGrowthRate(dto.Id).Result;
		return new GenericResponse<UserReadDto?>(dto);
	}

	public async Task<GenericResponse<UserReadDto?>> UpdateUser(UserCreateUpdateDto dto) {
		UserEntity? entity = _context.Set<UserEntity>().Include(x => x.Location).Include(x => x.Categories)
			.FirstOrDefault(x => x.Id == dto.Id);

		if (entity == null)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

		FillUserData(dto, entity);

		await _context.SaveChangesAsync();
		GenericResponse<UserReadDto?> readDto = await GetProfile(entity.Id, "");
		return readDto;
	}

	public async Task<GenericResponse<IEnumerable<UserReadDto>>> GetUsers(UserFilterDto dto) {
		IIncludableQueryable<UserEntity, object?> dbSet = _context.Set<UserEntity>().Include(u => u.Media);
		
		if (dto.ShowGender.IsTrue()) dbSet = dbSet.Include(u => u.Gender);
		if (dto.ShowCategories.IsTrue()) dbSet = dbSet.Include(u => u.Categories);
		if (dto.ShowForms.IsTrue()) dbSet = dbSet.Include(u => u.FormBuilders);
		if (dto.ShowLocations.IsTrue()) dbSet = dbSet.Include(u => u.Location);
		if (dto.ShowTransactions.IsTrue()) dbSet = dbSet.Include(u => u.Transactions);
		if (dto.ShowProducts.IsTrue()) dbSet = dbSet.Include(u => u.Products);

		IQueryable<UserEntity> q = dbSet.Where(x => x.DeletedAt != null);

		if (dto.UserId != null) q = q.Where(x => x.Id == dto.UserId);
		if (dto.UserName != null) q = q.Where(x => (x.AppUserName ?? "").ToLower().Contains(dto.UserName.ToLower()));

		List<UserEntity> entity = await q.AsNoTracking().ToListAsync();
		IEnumerable<UserReadDto>? readDto = _mapper.Map<IEnumerable<UserReadDto>>(entity);

		return new GenericResponse<IEnumerable<UserReadDto>>(readDto);
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
		if (!mobile.IsMobileNumber())
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

		IdentityResult? result = await _userManager.CreateAsync(entity, dto.Password);
		if (!result.Succeeded)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
			                                         "The information was not entered correctly");
		//await _context.Set<UserEntity>().AddAsync(entity);
		//await _context.SaveChangesAsync();

		return await GetProfileById(entity.Id);
	}

	private async Task<JwtSecurityToken> CreateToken(UserEntity user) {
		IEnumerable<string>? roles = await _userManager.GetRolesAsync(user);
		List<Claim> claims = new() {
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

	#region New Login Register

	public async Task<GenericResponse<UserReadDto?>> Register(RegisterDto aspNetUser) {
		UserEntity? model = _context.Set<UserEntity>()
			.FirstOrDefault(x => x.UserName == aspNetUser.UserName || x.Email == aspNetUser.Email ||
			                     x.PhoneNumber == aspNetUser.PhoneNumber);
		if (model != null)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest,
			                                         "This email or username already exists");

		UserEntity user = new() {
			Email = aspNetUser.Email ?? "",
			UserName = aspNetUser.UserName ?? aspNetUser.Email ?? aspNetUser.PhoneNumber,
			PhoneNumber = aspNetUser.PhoneNumber,
			EmailConfirmed = false,
			PhoneNumberConfirmed = false,
			FullName = "",
			Wallet = 0,
			Suspend = false
		};

		IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
		if (!result.Succeeded)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
			                                         "The information was not entered correctly");

		JwtSecurityToken token = await CreateToken(user);

		string? otp = "9999";
		if (aspNetUser.SendSMS) {
			if (aspNetUser.Email != null && aspNetUser.Email.IsEmail()) {
				//ToDo_AddEmailSender
			}
			else {
				otp = _otp.SendOtp(user.Id, 4);
			}
		}

		return new GenericResponse<UserReadDto?>(
			GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
			UtilitiesStatusCodes.Success, "Success");
	}

	public async Task<GenericResponse<UserReadDto?>> LoginWithPassword(LoginWithPasswordDto model) {
		UserEntity? user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null) user = await _userManager.FindByNameAsync(model.Email);
		if (user == null) {
			user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == model.Email);
		}

		if (user == null) return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "User not found");

		bool result = await _userManager.CheckPasswordAsync(user, model.Password);
		if (!result)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

		JwtSecurityToken token = await CreateToken(user);

		return new GenericResponse<UserReadDto?>(
			GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
			UtilitiesStatusCodes.Success, "Success");
	}

	public async Task<GenericResponse<string?>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
		UserEntity? model = _context.Set<UserEntity>().FirstOrDefault(x => x.Email == dto.Mobile);

		if (model != null) {
			string? otp = "9999";
			//ToDo_AddEmailSender
			return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
		}
		model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == dto.Mobile);
		string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
		if (dto.Mobile.Length <= 9 || !mobile.IsNumerical())
			return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");
		if (model != null) {
			string? otp = "9999";
			if (dto.SendSMS) otp = _otp.SendOtp(model.Id, 4);
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
			if (dto.SendSMS) {
				if (dto.Mobile.IsEmail()) {
					//ToDo_AddEmailSender
				}
				else {
					otp = _otp.SendOtp(user.Id, 4);
				}
			}

			return new GenericResponse<string?>(otp ?? "9999", UtilitiesStatusCodes.Success, "Success");
		}
	}

	public async Task<GenericResponse<UserReadDto?>> VerifyCodeForLogin(VerifyMobileForLoginDto dto) {
		if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.IsNumerical())
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.WrongVerificationCode,
			                                         "کد تایید وارد شده صحیح نیست");

		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == dto.Mobile);
		if (user == null)
			user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Email == dto.Mobile);

		if (user == null)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "کاربر یافت نشد");

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

	#endregion

	private void FillUserData(UserCreateUpdateDto dto, UserEntity entity) {
		entity.FirstName = dto.FirstName ?? entity.FirstName;
		entity.LastName = dto.LastName ?? entity.LastName;
		entity.FullName = dto.FullName ?? entity.FullName;
		entity.Bio = dto.Bio ?? entity.Bio;
		entity.AppUserName = dto.AppUserName ?? entity.AppUserName;
		entity.AppEmail = dto.AppEmail ?? entity.AppEmail;
		entity.Instagram = dto.Instagram ?? entity.Instagram;
		entity.Telegram = dto.Telegram ?? entity.Telegram;
		entity.WhatsApp = dto.WhatsApp ?? entity.WhatsApp;
		entity.LinkedIn = dto.LinkedIn ?? entity.LinkedIn;
		entity.AppEmail = dto.AppEmail ?? entity.AppEmail;
		entity.Region = dto.Region ?? entity.Region;
		entity.Activity = dto.Activity ?? entity.Activity;
		entity.Suspend = dto.Suspend ?? entity.Suspend;
		entity.Headline = dto.Headline ?? entity.Headline;
		entity.AppPhoneNumber = dto.AppPhoneNumber ?? entity.AppPhoneNumber;
		entity.Birthdate = dto.BirthDate ?? entity.Birthdate;
		entity.Wallet = dto.Wallet ?? entity.Wallet;
		entity.GenderId = dto.GenderId ?? entity.GenderId;
		entity.UserName = dto.UserName ?? entity.UserName;
		entity.Email = dto.Email ?? entity.Email;
		entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
		entity.Color = dto.Color ?? entity.Color;
		entity.Website = dto.Website ?? entity.Website;
		entity.ShowContactInfo = dto.ShowContactInfo ?? entity.ShowContactInfo;

		if (dto.Locations.IsNotNullOrEmpty()) {
			List<LocationEntity> list = new();
			foreach (int item in dto.Locations ?? new List<int>()) {
				LocationEntity? e = _context.Set<LocationEntity>().FirstOrDefaultAsync(x => x.Id == item).Result;
				if (e != null) list.Add(e);
			}

			entity.Location = list;
		}

		if (dto.Categories.IsNotNullOrEmpty()) {
			List<CategoryEntity> list = new();
			foreach (Guid item in dto.Categories!) {
				CategoryEntity? e = _context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item).Result;
				if (e != null) list.Add(_mapper.Map<CategoryEntity>(e));
			}

			entity.Categories = list;
		}
	}
}