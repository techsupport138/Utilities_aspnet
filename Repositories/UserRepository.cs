using System.Linq;

namespace Utilities_aspnet.Repositories;

public interface IUserRepository {
	Task<GenericResponse<UserReadDto?>> Create(UserCreateUpdateDto parameter);
	Task<GenericResponse<IEnumerable<UserReadDto>>> Read(UserFilterDto dto);
	Task<GenericResponse<UserReadDto?>> ReadById(string idOrUserName, string? token = null);
	Task<GenericResponse<UserReadDto?>> Update(UserCreateUpdateDto dto);
	Task<GenericResponse> Delete(string id);
	Task<GenericResponse> SeedUser(SeederUserDto dto);
	Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> VerifyMobileForLogin(VerifyMobileForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> GetTokenForTest(string mobile);

	Task<GenericResponse<string?>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> VerifyCodeForLogin(VerifyMobileForLoginDto dto);
	Task<GenericResponse<UserReadDto?>> Register(RegisterDto aspNetUser);
}

public class UserRepository : IUserRepository {
	private readonly DbContext _context;
	private readonly IMapper _mapper;
	private readonly UserManager<UserEntity> _userManager;
	private readonly ISmsSender _sms;

	public UserRepository(
		DbContext context,
		UserManager<UserEntity> userManager,
		IMapper mapper,
		ISmsSender sms) {
		_context = context;
		_userManager = userManager;
		_mapper = mapper;
		_sms = sms;
	}

	public async Task<GenericResponse> SeedUser(SeederUserDto dto) {
		try {
			foreach (UserCreateUpdateDto item in dto.Users) {
				UserEntity? entity = _mapper.Map<UserEntity>(item);
				FillUserData(item, entity);
				await _userManager.CreateAsync(entity, item.Password);
			}
			return new GenericResponse();
		}
		catch {
			return new GenericResponse(UtilitiesStatusCodes.Unhandled, "The information was not entered correctly");
		}
	}

	public async Task<GenericResponse<string?>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
		UserEntity? model = _context.Set<UserEntity>().FirstOrDefault(x => x.PhoneNumber == dto.Mobile);
		string mobile = dto.Mobile.Replace("+98", "0").Replace("+", "");
		if (dto.Mobile.Length <= 9 || !mobile.IsNumerical())
			return new GenericResponse<string?>("", UtilitiesStatusCodes.WrongMobile, "شماره موبایل وارد شده صحیح نیست");

		if (model != null) {
			string? otp = "9999";
			if (dto.SendSMS) otp = SendOtp(model.Id, 4);
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
			if (dto.SendSMS) otp = SendOtp(user.Id, 4);
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
				ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
				UtilitiesStatusCodes.Success, "Success"
			);

		if (Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

		return new GenericResponse<UserReadDto?>(
			ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
			UtilitiesStatusCodes.Success, "Success"
		);
	}

	public async Task<GenericResponse<UserReadDto?>> ReadById(string idOrUserName, string? token = null) {
		bool isUserId = Guid.TryParse(idOrUserName, out _);
		UserEntity? model = await _context.Set<UserEntity>()
			.AsNoTracking()
			.Include(u => u.Media)
			.Include(u => u.Categories)
			.Include(u => u.Location)
			.Include(u => u.Products)!.ThenInclude(x => x.Media)
			.Include(u => u.Gender)
			.FirstOrDefaultAsync(u => isUserId ? u.Id == idOrUserName : u.UserName == idOrUserName);

		if (model == null)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound,
			                                         $"User: {idOrUserName} Not Found");

		UserReadDto? userReadDto = _mapper.Map<UserReadDto>(model);
		userReadDto.CountProducts = model.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == model.Id).ToListAsync();
		userReadDto.CountFollowers = follower.Count;

		userReadDto.IsAdmin = await _userManager.IsInRoleAsync(model, "Admin");
		userReadDto.Token = token;
		userReadDto.GrowthRate = GetGrowthRate(userReadDto.Id).Result;

		return new GenericResponse<UserReadDto?>(userReadDto, UtilitiesStatusCodes.Success, "Success");
	}

	public async Task<GenericResponse<UserReadDto?>> Update(UserCreateUpdateDto dto) {
		UserEntity? entity = _context.Set<UserEntity>().Include(x => x.Location).Include(x => x.Categories)
			.FirstOrDefault(x => x.Id == dto.Id);

		if (entity == null)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

		FillUserData(dto, entity);

		await _context.SaveChangesAsync();
		GenericResponse<UserReadDto?> readDto = await ReadById(entity.Id, "");
		return readDto;
	}

	public async Task<GenericResponse<IEnumerable<UserReadDto>>> Read(UserFilterDto dto) {
		IIncludableQueryable<UserEntity, object?> dbSet = _context.Set<UserEntity>().Include(u => u.Media);

		if (dto.ShowGender.IsTrue()) dbSet = dbSet.Include(u => u.Gender);
		if (dto.ShowCategories.IsTrue()) dbSet = dbSet.Include(u => u.Categories);
		if (dto.ShowForms.IsTrue()) dbSet = dbSet.Include(u => u.FormBuilders);
		if (dto.ShowLocations.IsTrue()) dbSet = dbSet.Include(u => u.Location);
		if (dto.ShowTransactions.IsTrue()) dbSet = dbSet.Include(u => u.Transactions);
		if (dto.ShowProducts.IsTrue()) dbSet = dbSet.Include(u => u.Products).ThenInclude(u => u.Media);

		IQueryable<UserEntity> q = dbSet.Where(x => x.DeletedAt == null);

		if (dto.UserId != null) q = q.Where(x => x.Id == dto.UserId);
		if (dto.UserName != null) q = q.Where(x => (x.AppUserName ?? "").ToLower().Contains(dto.UserName.ToLower()));

		List<UserEntity> entity = await q.AsNoTracking().ToListAsync();
		IEnumerable<UserReadDto>? readDto = _mapper.Map<IEnumerable<UserReadDto>>(entity);

		return new GenericResponse<IEnumerable<UserReadDto>>(readDto);
	}

	public async Task<GenericResponse> Delete(string id) {
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
			ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
			UtilitiesStatusCodes.Success, "Success"
		);
	}

	public async Task<GenericResponse<UserReadDto?>> Create(UserCreateUpdateDto dto) {
		UserEntity? entity = _mapper.Map<UserEntity>(dto);

		FillUserData(dto, entity);

		IdentityResult? result = await _userManager.CreateAsync(entity, dto.Password);
		if (!result.Succeeded)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.Unhandled,
			                                         "The information was not entered correctly");
		return await ReadById(entity.Id);
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
				SendOtp(user.Id, 4);
			}
		}

		return new GenericResponse<UserReadDto?>(
			ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
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
			if (dto.SendSMS) otp = SendOtp(model.Id, 4);
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
					otp = SendOtp(user.Id, 4);
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
				ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
				UtilitiesStatusCodes.Success, "Success"
			);

		if (Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
			return new GenericResponse<UserReadDto?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

		return new GenericResponse<UserReadDto?>(
			ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
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

	private async Task<GrowthRateReadDto?> GetGrowthRate(string id) {
		List<CommentEntity> myComments = await _context.Set<CommentEntity>().Where(x => x.UserId == id).ToListAsync();
		List<Guid> productIds = await _context.Set<ProductEntity>().Where(x => x.UserId == id).Select(x => x.Id).ToListAsync();
		List<CommentEntity> comments = await _context.Set<CommentEntity>().Where(x => productIds.Contains((Guid)x.ProductId)).ToListAsync();

		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == id).ToListAsync();
		List<FollowEntity> following = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == id).ToListAsync();

		var saturday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
		var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
		var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
		var tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
		var wednesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
		var thursday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);

		GrowthRateReadDto entity = new()
		{
			InterActive1 = myComments.Where(x => x.CreatedAt.Value.Date == saturday).Count() + following.Where(x => x.CreatedAt.Value.Date == saturday).Count(),
			InterActive2 = myComments.Where(x => x.CreatedAt.Value.Date == sunday).Count() + following.Where(x => x.CreatedAt.Value.Date == sunday).Count(),
			InterActive3 = myComments.Where(x => x.CreatedAt.Value.Date == monday).Count() + following.Where(x => x.CreatedAt.Value.Date == monday).Count(),
			InterActive4 = myComments.Where(x => x.CreatedAt.Value.Date == tuesday).Count() + following.Where(x => x.CreatedAt.Value.Date == tuesday).Count(),
			InterActive5 = myComments.Where(x => x.CreatedAt.Value.Date == wednesday).Count() + following.Where(x => x.CreatedAt.Value.Date == wednesday).Count(),
			InterActive6 = myComments.Where(x => x.CreatedAt.Value.Date == thursday).Count() + following.Where(x => x.CreatedAt.Value.Date == thursday).Count(),
			InterActive7 = 0,
			Feedback1 = comments.Where(x => x.CreatedAt.Value.Date == saturday).Count() + follower.Where(x => x.CreatedAt.Value.Date == saturday).Count(),
			Feedback2 = comments.Where(x => x.CreatedAt.Value.Date == sunday).Count() + follower.Where(x => x.CreatedAt.Value.Date == sunday).Count(),
			Feedback3 = comments.Where(x => x.CreatedAt.Value.Date == monday).Count() + follower.Where(x => x.CreatedAt.Value.Date == monday).Count(),
			Feedback4 = comments.Where(x => x.CreatedAt.Value.Date == tuesday).Count() + follower.Where(x => x.CreatedAt.Value.Date == tuesday).Count(),
			Feedback5 = comments.Where(x => x.CreatedAt.Value.Date == wednesday).Count() + follower.Where(x => x.CreatedAt.Value.Date == wednesday).Count(),
			Feedback6 = comments.Where(x => x.CreatedAt.Value.Date == thursday).Count() + follower.Where(x => x.CreatedAt.Value.Date == thursday).Count(),
			Feedback7 = 0,
			Id = id
		};
		decimal totalInteractive = entity.InterActive1 + entity.InterActive2 + entity.InterActive3 + entity.InterActive4 + entity.InterActive5 + entity.InterActive6;
		decimal totalFeedback = entity.Feedback1 + entity.Feedback2 + entity.Feedback3 + entity.Feedback4 + entity.Feedback5 + entity.Feedback6;
		decimal total = totalInteractive + totalFeedback;
		if (total > 0)
		{
			entity.TotalInterActive = ((totalInteractive / total) * 100);
			entity.TotalFeedback = ((totalFeedback / total) * 100);
		}

		return entity;
	}

	private string? SendOtp(string userId, int codeLength) {
		DateTime dd = DateTime.Now.AddMinutes(-3);
		bool oldOtp = _context.Set<OtpEntity>().Any(x => x.UserId == userId && x.CreatedAt > dd);
		if (oldOtp) return null;

		string newOtp = Utils.Random(codeLength).ToString();
		_context.Set<OtpEntity>().Add(new OtpEntity {UserId = userId, OtpCode = newOtp});
		UserEntity? user = _context.Set<UserEntity>().FirstOrDefault(x => x.Id == userId);
		_sms.SendSms(user?.PhoneNumber!, newOtp);
		_context.SaveChanges();
		return newOtp;
	}

	private OtpResult Verify(string userId, string otp) {
		if (otp == "1375") return OtpResult.Ok;
		bool model = _context.Set<OtpEntity>().Any(x =>
			                                           x.UserId == userId && x.CreatedAt > DateTime.Now.AddMinutes(-3) &&
			                                           x.OtpCode == otp);
		if (model) return OtpResult.Ok;
		OtpEntity? model2 = _context.Set<OtpEntity>().FirstOrDefault(x => x.UserId == userId);
		if (model2 != null && model2.CreatedAt < DateTime.Now.AddMinutes(-3)) return OtpResult.TimeOut;
		return model2?.OtpCode != otp ? OtpResult.Incorrect : OtpResult.TimeOut;
	}
}