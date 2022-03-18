using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Responses;

public class ApiResponse<T> : ApiResponse
{
    public T? Result { get; set; }
    public ApiResponse(T result, UtilitiesStatusCodes status = UtilitiesStatusCodes.Success, string message = "") : base(UtilitiesStatusCodes.Success, "")
    {
        this.Result = result;
        this.Status = status;
        this.Message = message;
    }
}

public class ApiResponse
{
    public UtilitiesStatusCodes Status { get; set; } = UtilitiesStatusCodes.Success;
    public string Message { get; set; } = "";
    public ApiResponse(UtilitiesStatusCodes status, string message)
    {
        Status = status;
        Message = message;
    }
}