using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Utilities_aspnet.User.Entities;

namespace Utilities_aspnet.Utilities;

public static class IdentityExtensions
{
    public static void AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<UserEntity, IdentityRole>(
                options => { options.SignIn.RequireConfirmedAccount = false; }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
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

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
        });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => false;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = new PathString("/error/403");
            options.Cookie.Name = "Cookie.medgram_aspnet";
            options.Cookie.HttpOnly = false;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(604800);
            options.LoginPath = new PathString("/Account/Login");
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });
    }
    
    public static void UseSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void SeedUser<T>(this IHost host, string role, string username, string email, string password)
        where T : IdentityDbContext
    {
        try
        {
            host.Services.CreateScope();
            var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();
            var adminRole = new IdentityRole(role);

            if (!context.Roles.Any()) roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

            if (context.Users.Any(i => i.UserName == username)) return;
            var adminUser = new UserEntity
            {
                UserName = username,
                Email = email,
            };
            userManager.CreateAsync(adminUser, password).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}