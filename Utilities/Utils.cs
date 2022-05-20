using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension {
    public static void SetupUtilities<T>(
        this WebApplicationBuilder builder,
        string connectionStrings,
        DatabaseType databaseType = DatabaseType.SqlServer,
        string? redisConnectionString = null) where T : DbContext {
        builder.AddUtilitiesServices<T>(connectionStrings, databaseType);

        if (redisConnectionString != null) builder.AddRedis(redisConnectionString);

        builder.AddUtilitiesSwagger();
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
        });

        builder.Logging.AddEntityFramework<T>();
        builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromSeconds(604800); });
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

        builder.Services.AddTransient<AppSettings>();
        builder.Services.AddTransient<ISmsSender, SmsSender>();
        builder.Services.AddTransient<IOtpService, OtpService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
        builder.Services.AddTransient<IMediaRepository, MediaRepository>();
        builder.Services.AddTransient<IUploadRepository, UploadRepository>();
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<IEnumRepository, EnumRepository>();
        builder.Services.AddTransient<IIdTitleRepository<CategoryEntity>, IdTitleRepository<CategoryEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<TagEntity>, IdTitleRepository<TagEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<ColorEntity>, IdTitleRepository<ColorEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<FavoriteEntity>, IdTitleRepository<FavoriteEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<ContactInfoItemEntity>, IdTitleRepository<ContactInfoItemEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<SpecialityEntity>, IdTitleRepository<SpecialityEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<BrandEntity>, IdTitleRepository<BrandEntity>>();
        builder.Services.AddTransient<IIdTitleRepository<ReferenceEntity>, IdTitleRepository<ReferenceEntity>>();
        builder.Services.AddTransient<IProductRepository<ProductEntity>, ProductRepository<ProductEntity>>();
        builder.Services.AddTransient<IProductRepository<ProjectEntity>, ProductRepository<ProjectEntity>>();
        builder.Services.AddTransient<IProductRepository<EventEntity>, ProductRepository<EventEntity>>();
        builder.Services.AddTransient<IProductRepository<TutorialEntity>, ProductRepository<TutorialEntity>>();
        builder.Services.AddTransient<IProductRepository<AdEntity>, ProductRepository<AdEntity>>();
        builder.Services.AddTransient<IProductRepository<CompanyEntity>, ProductRepository<CompanyEntity>>();
        builder.Services.AddTransient<IProductRepository<MagazineEntity>, ProductRepository<MagazineEntity>>();
        builder.Services.AddTransient<IProductRepository<TenderEntity>, ProductRepository<TenderEntity>>();
        builder.Services.AddTransient<IProductRepository<ServiceEntity>, ProductRepository<ServiceEntity>>();

        //https://blog.elmah.io/generate-a-pdf-from-asp-net-core-for-free/
        //https://github.com/keyone2693/ImageResizer.AspNetCore
        //http://imageresizer.aspnetcore.keyone2693.ir/
        builder.Services.AddImageResizer();
    }

    private static void AddUtilitiesSwagger(this WebApplicationBuilder builder) {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => {
            c.UseInlineDefinitionsForEnums();
            c.DocumentFilter<SwaggerFilters>();
            c.SchemaFilter<SchemaFilter>();
            c.EnableAnnotations();
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
        // app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => {
            endpoints.MapAreaControllerRoute("Dashboard", "Dashboard",
                "/Dashboard/{controller=MyDashboard}/{action=Index}/{id?}",
                new {area = "Dashboard", controller = "MyDashboard", action = "Index"});
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
        app.UseSession();
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.DocExpansion(DocExpansion.None);
            c.DefaultModelsExpandDepth(-1);
        });
    }

    private class SchemaFilter : ISchemaFilter {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
            if (schema.Properties == null) return;

            foreach ((string _, OpenApiSchema? value) in schema.Properties)
                if (value.Default != null && value.Example == null)
                    value.Example = value.Default;
        }

        private string ToCamelCase(string name) {
            return char.ToLowerInvariant(name[0]) + name[1..];
        }
    }

    public class SwaggerFilters : IDocumentFilter {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) {
            swaggerDoc.Paths.Remove("/DNTCaptchaImage/Refresh");
            swaggerDoc.Paths.Remove("/DNTCaptchaImage/Show");
            IEnumerable<ApiDescription>? z = context.ApiDescriptions;
        }
    }
}