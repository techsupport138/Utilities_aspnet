using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Utilities_aspnet.Utilities.Core;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseApiController : ControllerBase { }