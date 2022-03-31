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

public class Response<T> : ControllerBase {
    public IActionResult ResponseSending(GenericResponse<T> response) {
        return response.Status switch {
            UtilitiesStatusCodes.Success => Ok(response),
            UtilitiesStatusCodes.BadRequest => BadRequest(response),
            UtilitiesStatusCodes.NotFound => NotFound(response),
            UtilitiesStatusCodes.Forbidden => Forbid(),
            _ => BadRequest(response)
        };
    }
}

public class Response : ControllerBase {
    public IActionResult ResponseSending(GenericResponse response) {
        return response.Status switch {
            UtilitiesStatusCodes.Success => Ok(response),
            UtilitiesStatusCodes.BadRequest => BadRequest(response),
            UtilitiesStatusCodes.NotFound => NotFound(response),
            UtilitiesStatusCodes.Forbidden => Forbid(),
            _ => BadRequest(response)
        };
    }
}