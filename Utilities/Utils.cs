using Microsoft.AspNetCore.SignalR;
using Utilities_aspnet.Category;
using Utilities_aspnet.Chat;
using Utilities_aspnet.Comment;
using Utilities_aspnet.Transaction;
using Utilities_aspnet.User;
using Utilities_aspnet.Utilities.Seeder;

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
        builder.Services.Configure<IISServerOptions>(options => {
            options.MaxRequestBodySize = int.MaxValue; // or your desired value
        });
        builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromSeconds(604800); });
    }

    private static void AddUtilitiesServices<T>(
        this WebApplicationBuilder builder,
        string connectionStrings,
        DatabaseType databaseType) where T : DbContext {
        builder.Services.AddCors(c =>
            c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        builder.Services.AddScoped<DbContext, T>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddDbContext<T>(options => {
            switch (databaseType) {
                case DatabaseType.SqlServer:
                    options.UseSqlServer(connectionStrings).EnableSensitiveDataLogging();
                    break;
                case DatabaseType.MySql:
                    options.UseMySql(connectionStrings, new MySqlServerVersion(new Version(8, 0, 28)))
                        .EnableSensitiveDataLogging();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
            }
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        builder.Services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.UseCamelCasing(true);
        });

        builder.Services.AddSignalR(i => i.EnableDetailedErrors = true);

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        
        builder.Services.AddTransient<AppSettings>();
        builder.Services.AddTransient<ISmsSender, SmsSender>();
        builder.Services.AddTransient<IOtpService, OtpService>();
        builder.Services.AddTransient<IReportRepository, ReportRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IMediaRepository, MediaRepository>();
        builder.Services.AddTransient<IUploadRepository, UploadRepository>();
        builder.Services.AddTransient<IFollowBookmarkRepository, FollowBookmarkRepository>();
        builder.Services.AddTransient<IAppSettingRepository, AppSettingRepository>();
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IChatRepository, ChatRepository>();
        builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
        builder.Services.AddTransient<IFormRepository, FormRepository>();
        builder.Services.AddTransient<ICommentRepository, CommentRepository>();
        builder.Services.AddTransient<ISeedRepository, SeedRepository>();
        builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
        builder.Services.AddTransient<IContentRepository, ContentRepository>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    private static void AddUtilitiesSwagger(this WebApplicationBuilder builder, IServiceProvider serviceProvider) {
        NetworkUtil.Configure(serviceProvider.GetService<IHttpContextAccessor>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => {
            c.UseInlineDefinitionsForEnums();
            c.OrderActionsBy(s => s.RelativePath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    private static void AddRedis(this WebApplicationBuilder builder, string connectionString) {
        builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
    }

    public static void UseUtilitiesServices(this WebApplication app) {
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseDeveloperExceptionPage();
        app.UseUtilitiesSwagger();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<UtilitiesHub>("/utilitiesHub");
        });
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.DocExpansion(DocExpansion.None);
            c.DefaultModelsExpandDepth(-1);
        });
    }

    public class UtilitiesHub : Hub{
        public async Task NewCallReceived(CallContext newCall) {
            await Clients.All.SendAsync("NewCallReceived", newCall);
        }
    }
}