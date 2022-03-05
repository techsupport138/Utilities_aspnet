using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Responses;

public class ApiResponse<T>
{
    public UtilitiesStatusCodes Status { get; set; } = UtilitiesStatusCodes.Unhandled;
    public string Message { get; set; } = "";
    public T? Result { get; set; }
}

public class ApiResponse
{
    public UtilitiesStatusCodes Status { get; set; } = UtilitiesStatusCodes.Unhandled;
    public string Message { get; set; } = "";
}