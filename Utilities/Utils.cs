using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension {
    public static void SetupUtilities<T>(this IServiceCollection services, string connectionStrings) where T : DbContext {
        services.AddUtilitiesServices<T>(connectionStrings);
        services.AddUtilitiesSwagger();
        services.AddUtilitiesIdentity();
    }

    private static void AddUtilitiesServices<T>(this IServiceCollection services, string connectionStrings) where T : DbContext {
        services.AddScoped<DbContext, T>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<T>(options => options.UseSqlServer(connectionStrings).EnableSensitiveDataLogging());
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
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
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