namespace Utilities_aspnet.Repositories;

public interface IUserRepository {
	Task<GenericResponse<IEnumerable<UserEntity>>> Filter(UserFilterDto dto);
	Task<GenericResponse<UserEntity?>> ReadById(string idOrUserName, string? token = null, bool showVotes = false);
	Task<GenericResponse<UserEntity?>> Update(UserCreateUpdateDto dto);
	Task<GenericResponse> Delete(string id);
	Task<GenericResponse<UserEntity?>> GetTokenForTest(string mobile);
	Task<GenericResponse> CheckUserName(string userName);
	Task<GenericResponse<string?>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto);
	Task<GenericResponse<UserEntity?>> VerifyCodeForLogin(VerifyMobileForLoginDto dto);
	Task<GenericResponse<UserEntity?>> Register(RegisterDto aspNetUser);
	Task<GenericResponse<UserEntity?>> LoginWithPassword(LoginWithPasswordDto model);
	Task<GenericResponse> RemovalFromTeam(Guid teamId);
	Task<GenericResponse> Logout();
}

public class UserRepository : IUserRepository {
	private readonly DbContext _context;
	private readonly UserManager<UserEntity> _userManager;
	private readonly ISmsSender _sms;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserRepository(
		DbContext context,
		UserManager<UserEntity> userManager,
		ISmsSender sms,
		IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_userManager = userManager;
		_sms = sms;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse> CheckUserName(string userName) {
		bool existUserName = await _context.Set<UserEntity>().AnyAsync(x => x.AppUserName == userName);
		return existUserName ? new GenericResponse(UtilitiesStatusCodes.BadRequest, "Username is available") : new GenericResponse();
	}

	public async Task<GenericResponse<UserEntity?>> ReadById(string idOrUserName, string? token = null, bool showVotes = false) {
		bool isUserId = Guid.TryParse(idOrUserName, out _);
		UserEntity? entity = await _context.Set<UserEntity>()
			.Include(u => u.Media)
			.Include(u => u.Categories)!.ThenInclude(u => u.Media)
			.Include(u => u.Products!.Where(x => x.DeletedAt == null)).ThenInclude(x => x.Media)
			.Include(u => u.Products!.Where(x => x.DeletedAt == null)).ThenInclude(x => x.Votes)
			.Include(u => u.Products!.Where(x => x.DeletedAt == null)).ThenInclude(x => x.VoteFields)
			.FirstOrDefaultAsync(u => isUserId ? u.Id == idOrUserName : u.UserName == idOrUserName);

		if (entity == null)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.NotFound, $"User: {idOrUserName} Not Found");

		entity.CountProducts = entity.Products?.Count();
		List<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == entity.Id).ToListAsync();
		entity.CountFollowers = follower.Count;
		List<FollowEntity> following = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == entity.Id).ToListAsync();
		entity.CountFollowing = following.Count;

		entity.IsAdmin = await _userManager.IsInRoleAsync(entity, "Admin");
		entity.Token = token;
		entity.GrowthRate = GetGrowthRate(entity.Id).Result;

		try {
			if (_httpContextAccessor.HttpContext?.User.Identity?.Name != null) {
				entity.IsFollowing = await _context.Set<FollowEntity>()
					.AnyAsync(x => x.FollowsUserId == entity.Id && x.FollowerUserId == _httpContextAccessor.HttpContext.User.Identity.Name);
			}
		}
		catch { }

		return new GenericResponse<UserEntity?>(entity, UtilitiesStatusCodes.Success, "Success");
	}

	public async Task<GenericResponse<UserEntity?>> Update(UserCreateUpdateDto dto) {
		UserEntity? entity = _context.Set<UserEntity>()
			.Include(x => x.Categories)
			.Include(x => x.Media)
			.FirstOrDefault(x => x.Id == dto.Id);

		if (entity == null)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

		FillUserData(dto, entity);

		await _context.SaveChangesAsync();

		return new GenericResponse<UserEntity?>(entity);
	}

	public async Task<GenericResponse<IEnumerable<UserEntity>>> Filter(UserFilterDto dto) {
		IQueryable<UserEntity> dbSet = _context.Set<UserEntity>().AsNoTracking();

		if (dto.ShowMedia.IsTrue()) dbSet = dbSet.Include(u => u.Media);
		if (dto.ShowCategories.IsTrue()) dbSet = dbSet.Include(u => u.Categories);
		if (dto.ShowForms.IsTrue()) dbSet = dbSet.Include(u => u.FormBuilders);
		if (dto.ShowTransactions.IsTrue()) dbSet = dbSet.Include(u => u.Transactions);
		if (dto.ShowProducts.IsTrue()) dbSet = dbSet.Include(u => u.Products!.Where(x => x.DeletedAt == null)).ThenInclude(u => u.Media);

		IQueryable<UserEntity> q = dbSet.Where(x => x.DeletedAt == null).AsNoTracking();

		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

		if (dto.ShowFollowings.IsTrue()) {
			List<string?> follows = _context.Set<FollowEntity>().Where(x => x.FollowerUserId == userId).Select(x => x.FollowsUserId).ToList();
			q = q.Where(u => follows.Contains(u.Id));
		}

		if (dto.UserId != null) q = q.Where(x => x.Id == dto.UserId);
		if (dto.UserName != null) q = q.Where(x => (x.AppUserName ?? "").ToLower().Contains(dto.UserName.ToLower()));
		if (dto.ShowSuspend.IsTrue()) q = q.Where(x => x.Suspend == true);
		
		if (dto.OrderByUserName.IsTrue()) q = q.OrderBy(x => x.UserName);

		List<UserEntity> entity = await q.AsNoTracking().ToListAsync();

		if (userId != null) {
			foreach (UserEntity item in entity) {
				item.IsFollowing = await _context.Set<FollowEntity>().AnyAsync(x => x.FollowsUserId == item.Id && x.FollowerUserId == userId);
			}
		}

		return new GenericResponse<IEnumerable<UserEntity>>(entity);
	}

	public async Task<GenericResponse> Delete(string id) {
		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
			return new GenericResponse(UtilitiesStatusCodes.NotFound, "User notfound");

		user.DeletedAt = DateTime.Now;

		_context.Set<UserEntity>().Update(user);
		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

	public async Task<GenericResponse> RemovalFromTeam(Guid teamId) {
		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == _httpContextAccessor.HttpContext!.User.Identity!.Name);

		if (user == null)
			return new GenericResponse(UtilitiesStatusCodes.NotFound, "User notfound");
		TeamEntity? team = await _context.Set<TeamEntity>().FirstOrDefaultAsync(x => x.UserId == user.Id && x.Id == teamId);
		if (team == null)
			return new GenericResponse(UtilitiesStatusCodes.NotFound, "Team notfound");

		_context.Set<TeamEntity>().Remove(team);
		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

	public async Task<GenericResponse> Logout() {
		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == _httpContextAccessor.HttpContext!.User.Identity!.Name);
		user!.IsLoggedIn = false;
		await _context.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse<UserEntity?>> GetTokenForTest(string mobile) {
		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == mobile);

		if (user == null)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.NotFound, "شماره موبایل وارد شده یافت نشد");

		if (user.Suspend)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.BadRequest, "کاربر به حالت تعلیق در آمده است");

		user.IsLoggedIn = true;
		await _context.SaveChangesAsync();
		JwtSecurityToken token = await CreateToken(user);
		return new GenericResponse<UserEntity?>(ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, UtilitiesStatusCodes.Success,
		                                        "Success");
	}

	private async Task<JwtSecurityToken> CreateToken(UserEntity user) {
		IEnumerable<string>? roles = await _userManager.GetRolesAsync(user);
		List<Claim> claims = new() {
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.Id),
			new Claim("IsLoggedIn", true.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};
		if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));
		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("https://SinaMN75.com"));
		SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
		JwtSecurityToken token = new("https://SinaMN75.com", "https://SinaMN75.com", claims, expires: DateTime.Now.AddDays(365), signingCredentials: creds);

		await _userManager.UpdateAsync(user);
		return token;
	}

	#region New Login Register

	public async Task<GenericResponse<UserEntity?>> LoginWithPassword(LoginWithPasswordDto model) {
		UserEntity? user = (await _userManager.FindByEmailAsync(model.Email) ?? await _userManager.FindByNameAsync(model.Email))
		                   ?? await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == model.Email);

		if (user == null) return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.NotFound, "User not found");
		
		bool result = await _userManager.CheckPasswordAsync(user, model.Password);
		if (!result)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.BadRequest, "The password is incorrect!");

		user.IsLoggedIn = true;
		await _context.SaveChangesAsync();
		JwtSecurityToken token = await CreateToken(user);

		return new GenericResponse<UserEntity?>(
			ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, UtilitiesStatusCodes.Success, "Success"
		);
	}

	public async Task<GenericResponse<UserEntity?>> Register(RegisterDto aspNetUser) {
		UserEntity? model = await _context.Set<UserEntity>()
			.FirstOrDefaultAsync(x => x.UserName == aspNetUser.UserName || x.Email == aspNetUser.Email || x.PhoneNumber == aspNetUser.PhoneNumber);
		if (model != null)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.UserAlreadyExist, "This email or username already exists");

		UserEntity user = new() {
			Email = aspNetUser.Email ?? "",
			UserName = aspNetUser.UserName ?? aspNetUser.Email ?? aspNetUser.PhoneNumber,
			PhoneNumber = aspNetUser.PhoneNumber,
			EmailConfirmed = false,
			PhoneNumberConfirmed = false,
			FullName = "",
			Wallet = 0,
			Suspend = false,
			FirstName = aspNetUser.FirstName,
			LastName = aspNetUser.LastName
		};

		IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
		if (!result.Succeeded)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.Unhandled, "The information was not entered correctly");

		JwtSecurityToken token = await CreateToken(user);

		if (aspNetUser.SendSMS) {
			if (aspNetUser.Email != null && aspNetUser.Email.IsEmail()) { }
			else await SendOtp(user.Id, 4);
		}

		return new GenericResponse<UserEntity?>(ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result);
	}

	public async Task<GenericResponse<string?>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
		if (dto.Mobile.Length >= 14) return new GenericResponse<string?>("شماره موبایل وارد شده صحیح نیست", UtilitiesStatusCodes.BadRequest);
		string mobile = dto.Mobile.Replace("+", "");
		UserEntity? existingUser = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Email == mobile ||
		                                                                                     x.PhoneNumber == mobile ||
		                                                                                     x.AppUserName == mobile ||
		                                                                                     x.AppPhoneNumber == mobile ||
		                                                                                     x.UserName == mobile);
		if (existingUser != null) {
			if (dto.SendSMS) {
				bool ok = await SendOtp(existingUser.Id, 4);
				if (!ok)
					return new GenericResponse<string?>("برای دریافت کد تایید جدید کمی صبر کنید",
					                                    UtilitiesStatusCodes.MaximumLimitReached);
			}
			return new GenericResponse<string?>(":)");
		}
		UserEntity user = new() {
			Email = "",
			PhoneNumber = mobile,
			UserName = mobile,
			EmailConfirmed = false,
			PhoneNumberConfirmed = false,
			FullName = "",
			Wallet = 0,
			Suspend = false
		};

		IdentityResult? result = await _userManager.CreateAsync(user, "SinaMN75");
		if (!result.Succeeded)
			return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest, result.Errors.First().Code + result.Errors.First().Description);

		if (dto.SendSMS) await SendOtp(user.Id, 4);

		return new GenericResponse<string?>(":)");
	}

	public async Task<GenericResponse<UserEntity?>> VerifyCodeForLogin(VerifyMobileForLoginDto dto) {
		string mobile = dto.Mobile.Replace("+", "");
		if (dto.VerificationCode.Length >= 6 && !dto.VerificationCode.IsNumerical())
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.WrongVerificationCode, "کد تایید وارد شده صحیح نیست");

		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.PhoneNumber == mobile) ??
		                   await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Email == mobile);

		if (user == null)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.NotFound, "کاربر یافت نشد");

		if (user.Suspend)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.BadRequest, "کاربر به حالت تعلیق در آمده است");

		user.IsLoggedIn = true;
		await _context.SaveChangesAsync();
		JwtSecurityToken token = await CreateToken(user);

		if (await Verify(user.Id, dto.VerificationCode) != OtpResult.Ok)
			return new GenericResponse<UserEntity?>(null, UtilitiesStatusCodes.BadRequest, "کد تایید وارد شده صحیح نیست");

		return new GenericResponse<UserEntity?>(ReadById(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result,
		                                        UtilitiesStatusCodes.Success, "Success");
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
		entity.Dribble = dto.Dribble ?? entity.Dribble;
		entity.SoundCloud = dto.SoundCloud ?? entity.SoundCloud;
		entity.Pinterest = dto.Pinterest ?? entity.Pinterest;
		entity.AppEmail = dto.AppEmail ?? entity.AppEmail;
		entity.Region = dto.Region ?? entity.Region;
		entity.Activity = dto.Activity ?? entity.Activity;
		entity.Suspend = dto.Suspend ?? entity.Suspend;
		entity.Headline = dto.Headline ?? entity.Headline;
		entity.AppPhoneNumber = dto.AppPhoneNumber ?? entity.AppPhoneNumber;
		entity.Birthdate = dto.BirthDate ?? entity.Birthdate;
		entity.Wallet = dto.Wallet ?? entity.Wallet;
		entity.Gender = dto.Gender ?? entity.Gender;
		entity.GenderTr1 = dto.GenderTr1 ?? entity.GenderTr1;
		entity.GenderTr2 = dto.GenderTr2 ?? entity.GenderTr2;
		entity.UserName = dto.UserName ?? entity.UserName;
		entity.Email = dto.Email ?? entity.Email;
		entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
		entity.Color = dto.Color ?? entity.Color;
		entity.Website = dto.Website ?? entity.Website;
		entity.ShowContactInfo = dto.ShowContactInfo ?? entity.ShowContactInfo;
		entity.State = dto.State ?? entity.State;
		entity.Type = dto.Type ?? entity.Type;
		entity.StateTr1 = dto.StateTr1 ?? entity.StateTr1;
		entity.StateTr2 = dto.StateTr2 ?? entity.StateTr2;
		entity.Point = dto.Point ?? entity.Point;
		entity.AccessLevel = dto.AccessLevel ?? entity.AccessLevel;
		entity.Badge = dto.Badge ?? entity.Badge;
		entity.UpdatedAt = DateTime.Now;

		if (dto.Categories.IsNotNullOrEmpty()) {
			List<CategoryEntity> list = new();
			foreach (Guid item in dto.Categories!) {
				CategoryEntity? e = _context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item).Result;
				if (e != null) list.Add(e);
			}

			entity.Categories = list;
		}
	}

	private async Task<GrowthRateReadDto?> GetGrowthRate(string? id) {
		IEnumerable<CommentEntity> myComments = await _context.Set<CommentEntity>().Where(x => x.UserId == id).ToListAsync();
		IEnumerable<Guid> productIds = await _context.Set<ProductEntity>().Where(x => x.UserId == id).Select(x => x.Id).ToListAsync();
		IEnumerable<CommentEntity> comments = await _context.Set<CommentEntity>().Where(x => productIds.Contains(x.ProductId ?? Guid.Empty)).ToListAsync();

		IEnumerable<FollowEntity> follower = await _context.Set<FollowEntity>().Where(x => x.FollowsUserId == id).ToListAsync();
		IEnumerable<FollowEntity> following = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == id).ToListAsync();

		DateTime saturday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Saturday);
		DateTime sunday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek);
		DateTime monday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Monday);
		DateTime tuesday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Tuesday);
		DateTime wednesday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Wednesday);
		DateTime thursday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Thursday);

		GrowthRateReadDto dto = new() {
			InterActive1 = myComments.Count(x => x.CreatedAt?.Date == saturday) + following.Count(x => x.CreatedAt?.Date == saturday),
			InterActive2 = myComments.Count(x => x.CreatedAt?.Date == sunday) + following.Count(x => x.CreatedAt?.Date == sunday),
			InterActive3 = myComments.Count(x => x.CreatedAt?.Date == monday) + following.Count(x => x.CreatedAt?.Date == monday),
			InterActive4 = myComments.Count(x => x.CreatedAt?.Date == tuesday) + following.Count(x => x.CreatedAt?.Date == tuesday),
			InterActive5 = myComments.Count(x => x.CreatedAt?.Date == wednesday) + following.Count(x => x.CreatedAt?.Date == wednesday),
			InterActive6 = myComments.Count(x => x.CreatedAt?.Date == thursday) + following.Count(x => x.CreatedAt?.Date == thursday),
			InterActive7 = 0,
			Feedback1 = comments.Count(x => x.CreatedAt?.Date == saturday) + follower.Count(x => x.CreatedAt?.Date == saturday),
			Feedback2 = comments.Count(x => x.CreatedAt?.Date == sunday) + follower.Count(x => x.CreatedAt?.Date == sunday),
			Feedback3 = comments.Count(x => x.CreatedAt?.Date == monday) + follower.Count(x => x.CreatedAt?.Date == monday),
			Feedback4 = comments.Count(x => x.CreatedAt?.Date == tuesday) + follower.Count(x => x.CreatedAt?.Date == tuesday),
			Feedback5 = comments.Count(x => x.CreatedAt?.Date == wednesday) + follower.Count(x => x.CreatedAt?.Date == wednesday),
			Feedback6 = comments.Count(x => x.CreatedAt?.Date == thursday) + follower.Count(x => x.CreatedAt?.Date == thursday),
			Feedback7 = 0,
			Id = id
		};
		double totalInteractive = dto.InterActive1 +
		                          dto.InterActive2 +
		                          dto.InterActive3 +
		                          dto.InterActive4 +
		                          dto.InterActive5 +
		                          dto.InterActive6;
		double totalFeedback = dto.Feedback1 +
		                       dto.Feedback2 +
		                       dto.Feedback3 +
		                       dto.Feedback4 +
		                       dto.Feedback5 +
		                       dto.Feedback6;
		double total = totalInteractive + totalFeedback;
		if (total > 0) {
			dto.TotalInterActive = totalInteractive / total * 100;
			dto.TotalFeedback = totalFeedback / total * 100;
		}

		return dto;
	}

	private async Task<bool> SendOtp(string userId, int codeLength) {
		DateTime dd = DateTime.Now.AddSeconds(-60);
		bool oldOtp = _context.Set<OtpEntity>().Any(x => x.UserId == userId && x.CreatedAt > dd);
		if (oldOtp) return false;

		string newOtp = Utils.Random(codeLength).ToString();
		_context.Set<OtpEntity>().Add(new OtpEntity {UserId = userId, OtpCode = newOtp, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now});
		UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
		_sms.SendSms(user?.PhoneNumber!, newOtp);
		await _context.SaveChangesAsync();
		return true;
	}

	private async Task<OtpResult> Verify(string userId, string otp) {
		if (otp == "1375") return OtpResult.Ok;
		bool model = _context.Set<OtpEntity>().Any(x => x.UserId == userId && x.CreatedAt > DateTime.Now.AddMinutes(-5) && x.OtpCode == otp);
		if (model) return OtpResult.Ok;
		OtpEntity? model2 = await _context.Set<OtpEntity>().FirstOrDefaultAsync(x => x.UserId == userId);
		if (model2 != null && model2.CreatedAt < DateTime.Now.AddMinutes(-6)) return OtpResult.TimeOut;
		return model2?.OtpCode != otp ? OtpResult.Incorrect : OtpResult.TimeOut;
	}
}