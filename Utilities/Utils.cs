using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension
{
    public static void SetupUtilities<T>(this IServiceCollection services, string connectionStrings) where T : DbContext
    {
        services.AddUtilitiesServices<T>(connectionStrings);
        services.AddUtilitiesSwagger();
        services.AddUtilitiesIdentity();
    }
    
    public static void AddUtilitiesServices<T>(this IServiceCollection services, string connectionStrings) where T : DbContext
    {
        services.AddScoped<DbContext, T>();
        services.AddDbContext<T>(options => options.UseSqlServer(connectionStrings).EnableSensitiveDataLogging());
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        services.AddMvc(option => option.EnableEndpointRouting = false)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            });
    }

    public static void UseUtilitiesServices(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseUtilitiesSwagger();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseRouting();
    }
}