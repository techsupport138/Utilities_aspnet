namespace Utilities_aspnet.Utilities;

public static class IdentityExtensions {
	public static void AddUtilitiesIdentity(this WebApplicationBuilder builder) {
		builder.Services.AddIdentity<UserEntity, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
			.AddRoles<IdentityRole>().AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions => {
			jwtBearerOptions.RequireHttpsMetadata = false;
			jwtBearerOptions.SaveToken = true;
			jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
				RequireSignedTokens = true,
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				RequireExpirationTime = true,
				ClockSkew = TimeSpan.Zero,
				ValidAudience = "https://SinaMN75.com",
				ValidIssuer = "https://SinaMN75.com",
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("https://SinaMN75.com"))
			};
		});

		builder.Services.Configure<IdentityOptions>(options => {
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 4;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;
			options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
		});

		builder.Services.Configure<CookiePolicyOptions>(options => {
			options.CheckConsentNeeded = _ => false;
			options.MinimumSameSitePolicy = SameSiteMode.None;
			options.Secure = CookieSecurePolicy.Always;
		});

		builder.Services.ConfigureApplicationCookie(options => {
			options.AccessDeniedPath = new PathString("/error/403");
			options.Cookie.Name = "Cookie.phopx";
			options.Cookie.HttpOnly = false;
			options.ExpireTimeSpan = TimeSpan.FromMinutes(604800);
			options.LoginPath = new PathString("/Dashboard/Account/Login");
			options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
			options.SlidingExpiration = true;
		});
	}

	public static void SeedUser<T>(
		this IHost host,
		string role,
		string username,
		string email,
		string password)
		where T : IdentityDbContext {
		try {
			host.Services.CreateScope();
			IServiceScope scope = host.Services.CreateScope();
			T context = scope.ServiceProvider.GetRequiredService<T>();
			UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
			RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			context.Database.EnsureCreated();
			IdentityRole adminRole = new(role);

			if (!context.Roles.Any()) roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

			if (context.Users.Any(i => i.UserName == username)) return;
			UserEntity adminUser = new() {UserName = username, Email = email};
			userManager.CreateAsync(adminUser, password).GetAwaiter().GetResult();
			userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
		}
		catch (Exception e) {
			Console.WriteLine(e);
		}
	}
}