using Microsoft.AspNetCore.Mvc;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Base;

[ApiController]
public class BaseApiController : ControllerBase {
    [NonAction]
    public ObjectResult Result(GenericResponse response) => new(response) {
        StatusCode = response.Status.value()
    };
}