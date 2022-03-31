using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Utilities_aspnet.Statistic.Data;
using Utilities_aspnet.User.Data;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension {
    public static void SetupUtilities<T>(this WebApplicationBuilder builder, string connectionStrings,
        DatabaseType databaseType = DatabaseType.SqlServer) where T : DbContext {
        builder.AddUtilitiesServices<T>(connectionStrings, databaseType);
        builder.AddUtilitiesSwagger();
        builder.AddUtilitiesIdentity();
        
    }

    private static void AddUtilitiesServices<T>(this WebApplicationBuilder builder, string connectionStrings, DatabaseType databaseType)
        where T : DbContext {
        builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        builder.Services.AddScoped<DbContext, T>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddDbContext<T>(options => {
            switch (databaseType) {
                case DatabaseType.SqlServer:
                    options.UseSqlServer(connectionStrings).EnableSensitiveDataLogging();
                    break;
                case DatabaseType.MySql:
                    options.UseMySql(connectionStrings, new MySqlServerVersion(new Version(8, 0, 28))).EnableSensitiveDataLogging();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
            }
        });
        builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(i => i.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        });

        builder.Logging.AddEntityFramework<T>();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(604800);
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        builder.Services.AddMemoryCache();
        
        builder.Services.AddTransient<ISmsSender, SmsSender>();
        builder.Services.AddTransient<IOtpService, OtpService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();

        ///todo: همه ریپوزیتوری ها اینجا رجیستر بشند
        builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
        
    }

    public static void UseUtilitiesServices(this WebApplication app) {
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (app.Environment.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseUtilitiesSwagger();
        }

        app.UseHttpsRedirection();
        RewriteOptions? options = new RewriteOptions()
            .AddRedirectToHttpsPermanent()
            .AddRedirectToWwwPermanent();
        app.UseRewriter(options);
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseRouting();
        app.UseEndpoints(endpoints=>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
        
    }

    private static void AddUtilitiesSwagger(this WebApplicationBuilder builder) {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
    }
}