using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Responses;

public class ApiResponse<T>
{
    public UtilitiesStatusCodes Status { get; set; } = UtilitiesStatusCodes.Unhandled;
    public string Message { get; set; } = "";
    public T? Result { get; set; }
    public ApiResponse(UtilitiesStatusCodes status, T result, string message)
    {
        this.Result = result;
        this.Status = status;
        this.Message = message;
    }
}

public class ApiResponse
{
    public UtilitiesStatusCodes Status { get; set; } = UtilitiesStatusCodes.Unhandled;
    public string Message { get; set; } = "";
    public ApiResponse(UtilitiesStatusCodes status, string message)
    {
        this.Status = status;
        this.Message = message;
    }
}