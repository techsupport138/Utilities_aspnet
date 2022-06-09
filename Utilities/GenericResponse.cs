namespace Utilities_aspnet.Utilities;

public class GenericResponse<T> : GenericResponse {
    public GenericResponse(T result, UtilitiesStatusCodes status = UtilitiesStatusCodes.Success, string message = "") {
        Result = result;
        Status = status;
        Message = message;
    }

    public T? Result { get; set; }
}

public class GenericResponse {
    public GenericResponse(
        UtilitiesStatusCodes status = UtilitiesStatusCodes.Success,
        string message = "",
        List<Guid>? ids = null,
        Guid? id = null) {
        Status = status;
        Message = message;
    }

    public UtilitiesStatusCodes Status { get; set; }
    public int? PageSize { get; set; }
    public int? PageCount { get; set; }
    public int? TotalCount { get; set; }
    public string Message { get; set; }
}