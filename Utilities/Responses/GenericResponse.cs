using Microsoft.AspNetCore.Mvc;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Responses;

public class GenericResponse<T> : GenericResponse {
    public T? Result { get; set; }

    public GenericResponse(T result, UtilitiesStatusCodes status = UtilitiesStatusCodes.Success, string message = "") {
        Result = result;
        Status = status;
        Message = message;
    }
}

public class GenericResponse {
    public UtilitiesStatusCodes Status { get; set; }
    public string Message { get; set; }

    public GenericResponse(UtilitiesStatusCodes status = UtilitiesStatusCodes.Success, string message = "") {
        Status = status;
        Message = message;
    }
}


public class Response<T> : ControllerBase
{
    public IActionResult ResponseSending(GenericResponse<T> response)
    {
        switch ((UtilitiesStatusCodes)response.Status)
        {
            case UtilitiesStatusCodes.Success:
                return Ok(response);
            case UtilitiesStatusCodes.BadRequest:
                return BadRequest(response);
            case UtilitiesStatusCodes.NotFound:
                return NotFound(response);
            case UtilitiesStatusCodes.Forbidden:
                return Forbid();
            default:
                return BadRequest(response);
        }
    }
}

public class Response : ControllerBase
{
    public IActionResult ResponseSending(GenericResponse response)
    {
        switch ((UtilitiesStatusCodes)response.Status)
        {
            case UtilitiesStatusCodes.Success:
                return Ok(response);
            case UtilitiesStatusCodes.BadRequest:
                return BadRequest(response);
            case UtilitiesStatusCodes.NotFound:
                return NotFound(response);
            case UtilitiesStatusCodes.Forbidden:
                return Forbid();
            default:
                return BadRequest(response);
        }
    }
}