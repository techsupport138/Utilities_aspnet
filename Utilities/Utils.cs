namespace Utilities_aspnet.Utilities;

public static class StartupExtension {
	public static void SetupUtilities<T>(
		this WebApplicationBuilder builder,
		string connectionStrings,
		DatabaseType databaseType = DatabaseType.SqlServer,
		string? redisConnectionString = null) where T : DbContext {
		builder.AddUtilitiesServices<T>(connectionStrings, databaseType);

		if (redisConnectionString != null) builder.AddRedis(redisConnectionString);

		IServiceProvider? serviceProvider = builder.Services.BuildServiceProvider().GetService<IServiceProvider>();

		builder.AddUtilitiesSwagger(serviceProvider);
		builder.AddUtilitiesIdentity();

		builder.Services.Configure<FormOptions>(x => {
			x.ValueLengthLimit = int.MaxValue;
			x.MultipartBodyLengthLimit = int.MaxValue;
			x.MultipartHeadersLengthLimit = int.MaxValue;
		});
		builder.Services.Configure<IISServerOptions>(options => options.MaxRequestBodySize = int.MaxValue);
	}

	private static void AddUtilitiesServices<T>(this WebApplicationBuilder builder, string connectionStrings, DatabaseType databaseType) where T : DbContext {
		builder.Services.AddOptions();
		builder.Services.AddMemoryCache();
		builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
		builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
		builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
		builder.Services.AddInMemoryRateLimiting();

		builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
		builder.Services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
		builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
		builder.Services.AddResponseCompression(options => {
			options.EnableForHttps = true;
			options.Providers.Add<BrotliCompressionProvider>();
			options.Providers.Add<GzipCompressionProvider>();
		});
		builder.Services.AddResponseCaching();
		builder.Services.AddScoped<DbContext, T>();

		builder.Services.AddDbContext<T>(options => {
			switch (databaseType) {
				case DatabaseType.SqlServer:
					options.UseSqlServer(connectionStrings, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
					break;
				case DatabaseType.MySql:
					options.UseMySql(connectionStrings, new MySqlServerVersion(new Version(8, 0, 28)),
					                 o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
			}
		});

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
		builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
		builder.Services.AddControllersWithViews(option => {
			option.EnableEndpointRouting = false;
			option.CacheProfiles.Add("default", new CacheProfile {
				Duration = 10,
				Location = ResponseCacheLocation.Any
			});
		}).AddNewtonsoftJson(options => {
			options.SerializerSettings.ContractResolver = new DefaultContractResolver();
			options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
			options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			options.UseCamelCasing(true);
		});

		builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		builder.Services.AddTransient<AppSettings>();
		builder.Services.AddTransient<ISmsSender, SmsSender>();
		builder.Services.AddTransient<IReportRepository, ReportRepository>();
		builder.Services.AddTransient<IUserRepository, UserRepository>();
		builder.Services.AddTransient<IMediaRepository, MediaRepository>();
		builder.Services.AddTransient<IUploadRepository, UploadRepository>();
		builder.Services.AddTransient<IFollowBookmarkRepository, FollowBookmarkRepository>();
		builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
		builder.Services.AddTransient<IProductRepository, ProductRepository>();
		builder.Services.AddTransient<IChatRepository, ChatRepository>();
		builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
		builder.Services.AddTransient<IFormRepository, FormRepository>();
		builder.Services.AddTransient<ICommentRepository, CommentRepository>();
		builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
		builder.Services.AddTransient<IContentRepository, ContentRepository>();
		builder.Services.AddTransient<IVoteRepository, VoteRepository>();
		builder.Services.AddTransient<ITopProductRepository, TopProductRepository>();
		builder.Services.AddTransient<IOrderRepository, OrderRepository>();
		builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
		builder.Services.AddTransient<IGlobalSearchRepository, GlobalSearchRepository>();
		builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
		builder.Services.AddTransient<IMailSmsRepository, MailSmsRepository>();
	}

	private static void AddUtilitiesSwagger(this WebApplicationBuilder builder, IServiceProvider? serviceProvider) {
		Server.Configure(serviceProvider?.GetService<IHttpContextAccessor>());
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c => {
			c.UseInlineDefinitionsForEnums();
			c.OrderActionsBy(s => s.RelativePath);
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
				Description = "JWT Authorization header.\r\n\r\nExample: \"Bearer 12345abcdef\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}},
					Array.Empty<string>()
				}
			});
		});
	}

	private static void AddRedis(this WebApplicationBuilder builder, string connectionString)
		=> builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));

	public static void AddUtilitiesIdentity(this WebApplicationBuilder builder) {
		builder.Services.AddIdentity<UserEntity, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; }).AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters {
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
	}

	public static void UseUtilitiesServices(this WebApplication app) {
		app.UseIpRateLimiting();
		app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
		app.UseDeveloperExceptionPage();
		app.UseResponseCaching();
		app.UseUtilitiesSwagger();
		app.UseStaticFiles();
		app.UseAuthentication();
		app.UseRouting();
		app.UseAuthorization();
	}

	private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
		app.UseSwagger();
		app.UseSwaggerUI(c => {
			c.DocExpansion(DocExpansion.None);
			c.DefaultModelsExpandDepth(-1);
		});
	}
}

public class ClaimRequirementAttribute : TypeFilterAttribute {
	public ClaimRequirementAttribute() : base(typeof(ClaimRequirementFilter)) {
		Arguments = new object[] {new Claim("IsLoggedIn", "true")};
	}
}

public class ClaimRequirementFilter : IAuthorizationFilter {
	private readonly DbContext _dbContext;

	public ClaimRequirementFilter(Claim claim, DbContext dbContext) => _dbContext = dbContext;

	public void OnAuthorization(AuthorizationFilterContext context) {
		UserEntity? entity = _dbContext.Set<UserEntity>()
			.FirstOrDefault(u => context.HttpContext.User.Identity != null && u.Id == context.HttpContext.User.Identity.Name);
		if (entity is {IsLoggedIn: false}) context.Result = new ForbidResult();
	}
}