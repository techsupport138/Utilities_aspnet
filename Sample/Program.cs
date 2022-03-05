using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.Utilities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddUtilitiesServices<DbContext>(connectionStrings: "");

var app = builder.Build();

app.UseUtilitiesServices(app.Environment);

app.Run();