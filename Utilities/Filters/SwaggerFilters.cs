using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Filters
{
    public class SwaggerFilters: IDocumentFilter
    {
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			swaggerDoc.Paths.Remove("/DNTCaptchaImage/Refresh");
			swaggerDoc.Paths.Remove("/DNTCaptchaImage/Show");
			var z = context.ApiDescriptions;
		}
	}
}
