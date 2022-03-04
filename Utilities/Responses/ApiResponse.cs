using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Responses;

public class ApiResponse<T>
{
    public StatusCodes Status { get; set; } = StatusCodes.Unhandled;
    public string Message { get; set; } = "";
    public T? Result { get; set; }
}