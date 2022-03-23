using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
                case DatabaseType.MongoDb:
                    builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoDb"));
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
    }

    public static void UseUtilitiesServices(this WebApplication app) {
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (app.Environment.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseUtilitiesSwagger();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseRouting();
    }

    private static void AddUtilitiesSwagger(this WebApplicationBuilder builder) {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}