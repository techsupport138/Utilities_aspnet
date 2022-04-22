﻿using ImageResizer.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using Utilities_aspnet.Base.Data;
using Utilities_aspnet.Statistic.Data;
using Utilities_aspnet.User.Data;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Enums;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using System.Reflection;
using Utilities_aspnet.Utilities.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.OpenApi.Models;

namespace Utilities_aspnet.Utilities;

public static class StartupExtension
{

    public static void SetupUtilities<T>(this WebApplicationBuilder builder, string connectionStrings,
        DatabaseType databaseType = DatabaseType.SqlServer, string? redisConnectionString = null) where T : DbContext
    {
        builder.AddUtilitiesServices<T>(connectionStrings, databaseType);

        
        
        if (redisConnectionString != null) builder.AddRedis(redisConnectionString);

        builder.Services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(connectionStrings)
                    .EnableSensitiveDataLogging(false)
            );
        

        builder.AddUtilitiesIdentity();
        builder.AddUtilitiesSwagger();

        builder.Services.Configure<FormOptions>(x =>
        {
            //x.MultipartBodyLengthLimit = 209715200;
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
            x.MultipartHeadersLengthLimit = int.MaxValue;
        });
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = int.MaxValue; // or your desired value
        });
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = System.TimeSpan.FromSeconds(604800);
        });


    }

    private static void AddUtilitiesServices<T>(this WebApplicationBuilder builder, string connectionStrings, DatabaseType databaseType)
        where T : DbContext
    {
        builder.Services.AddCors(c =>
        c.AddPolicy("AllowOrigin", option =>
        option.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));
        builder.Services.AddScoped<DbContext, T>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //builder.Services.AddScoped<SignInManager<UserEntity>, SignInManager<UserEntity>>();

        builder.Services.AddDbContext<T>(options =>
        {
            switch (databaseType)
            {
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
        //builder.Services.AddSingleton<IFileProvider>(_ => new PhysicalFileProvider(_env.WebRootPath ?? _env.ContentRootPath));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


        builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(i => i.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

        builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        });

        builder.Logging.AddEntityFramework<T>();
        builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromSeconds(604800); });
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddMemoryCache();


        //todo:همه ریپوزیتوری های مورد استفاده اینجا رجیستر شود
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



        //https://blog.elmah.io/generate-a-pdf-from-asp-net-core-for-free/
        //builder.Services.AddWkhtmltopdf("wkhtmltopdf");

        //https://github.com/keyone2693/ImageResizer.AspNetCore
        //http://imageresizer.aspnetcore.keyone2693.ir/
        builder.Services.AddImageResizer();



    }

    private static void AddUtilitiesSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory);
<<<<<<< HEAD
            c.IncludeXmlComments(xmlPath + xmlFile);
=======
            c.IncludeXmlComments(xmlPath + "/Phopx.xml");
>>>>>>> 43adfb26d5009afcd6e20fc03f387bd3729c8108
            c.IncludeXmlComments(xmlPath + "/Utilities_aspnet.xml");
            c.UseInlineDefinitionsForEnums();
            c.DocumentFilter<SwaggerFilters>();
            c.SchemaFilter<SchemaFilter>();
            c.EnableAnnotations();
            c.OrderActionsBy(c => c.RelativePath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.OperationFilter<AddSwaggerService>();
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
                }
              });
        });
    }

    private static void AddRedis(this WebApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
    }

    public static void UseUtilitiesServices(this WebApplication app)
    {
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDeveloperExceptionPage();
        app.UseUtilitiesSwagger();

        //app.UseHttpsRedirection();
        RewriteOptions options = new RewriteOptions();
        //.AddRedirectToHttpsPermanent();
        //.AddRedirectToWwwPermanent();


        app.UseImageResizer();

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapAreaControllerRoute("Dashboard", "Dashboard",
                "/Dashboard/{controller=MyDashboard}/{action=Index}/{id?}",
                new { area = "Dashboard", controller = "MyDashboard", action = "Index" });
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
        app.UseSession();
    }

    private static void UseUtilitiesSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            c.DefaultModelsExpandDepth(-1);

        });
    }
}