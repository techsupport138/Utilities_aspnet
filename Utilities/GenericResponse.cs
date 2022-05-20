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
        Ids = ids;
        Id = id;
    }

    public UtilitiesStatusCodes Status { get; set; }
    public string Message { get; set; }
    public List<Guid>? Ids { get; set; }
    public Guid? Id { get; set; }
}