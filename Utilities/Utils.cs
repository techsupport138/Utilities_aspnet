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
    public static void SetupUtilities<T>(this IServiceCollection services, string connectionStrings,
        DatabaseType databaseType = DatabaseType.SqlServer) where T : DbContext {
        services.AddUtilitiesServices<T>(connectionStrings, databaseType);
        services.AddUtilitiesSwagger();
        services.AddUtilitiesIdentity();
    }

    private static void AddUtilitiesServices<T>(this IServiceCollection services, string connectionStrings, DatabaseType databaseType)
        where T : DbContext {
        services.AddCors(c => c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        services.AddScoped<DbContext, T>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<T>(options => {
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
        services.AddControllersWithViews()
            .AddNewtonsoftJson(i => i.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        services.AddRazorPages();
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(options => {
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
        app.UseWebSockets();
        app.Use(async (context, next) => {
            if (context.WebSockets.IsWebSocketRequest) {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine(webSocket.State);
                Console.WriteLine("DONE");
            }
            else
                await next();
        });
    }

    private static void AddUtilitiesSwagger(this IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}