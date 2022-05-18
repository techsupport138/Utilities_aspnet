

namespace Utilities_aspnet.Utilities.Filters
{
    public class AddSwaggerService: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

            if (!isAuthorized && allowAnonymous)
            {
                return;
            }
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Security = new List<OpenApiSecurityRequirement>();


            var security = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        }
                    }, new List<string>()
                }
            };
            //add security in here
            operation.Security.Add(security);
        }
    }
}
