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
    public List<Guid>? Ids { get; set; }
    public Guid? Id { get; set; }

    public GenericResponse(UtilitiesStatusCodes status = UtilitiesStatusCodes.Success, string message = "", List<Guid>? ids = null,
        Guid? id = null) {
        Status = status;
        Message = message;
        Ids = ids;
        Id = id;
    }
}