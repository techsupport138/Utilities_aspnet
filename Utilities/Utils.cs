using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension {
    static StartupExtension() { }

    public static void SetupUtilities<T>(this IServiceCollection services, string connectionStrings) where T : DbContext {
        services.AddUtilitiesServices<T>(connectionStrings);
        services.AddUtilitiesSwagger();
        services.AddUtilitiesIdentity();
    }

    private static void AddUtilitiesServices<T>(this IServiceCollection services, string connectionStrings) where T : DbContext {
        services.AddScoped<DbContext, T>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<T>(options => options.UseSqlServer(connectionStrings).EnableSensitiveDataLogging());
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
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