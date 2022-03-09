using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.Utilities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddUtilitiesServices<DbContext>(connectionStrings: "");

WebApplication app = builder.Build();

app.UseUtilitiesServices(app.Environment);

app.Run();