using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Utilities_aspnet.Utilities;

public static class SwaggerExtensions
{
    public static void AddUtilitiesSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "SinaMN75", Version = "v1.0.0"});
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization using the Bearer. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            };
            c.AddSecurityDefinition("Bearer", securitySchema);
            c.SchemaFilter<SchemaFilter>();
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {{securitySchema, new[] {"Bearer"}}});
        });
    }
    
    public static void UseUtilitiesSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleApi v1"));
    }

    private class SchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null) return;
            foreach (var (_, value) in schema.Properties)
                if (value.Default != null && value.Example == null)
                    value.Example = value.Default;
        }
    }
}