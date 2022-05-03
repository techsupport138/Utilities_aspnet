namespace Utilities_aspnet.Utilities.Enums;

public enum UtilitiesStatusCodes {
    Success = 200,
    BadRequest = 400,
    Forbidden = 403,
    NotFound = 404,
    Unhandled = 900,
    New = 499
}

public static class UtilitiesStatusCodesExtension {
    public static bool isSuccessful(this UtilitiesStatusCodes statusCode) => statusCode >= UtilitiesStatusCodes.Success;
    public static bool isBadRequest(this UtilitiesStatusCodes statusCode) => statusCode >= UtilitiesStatusCodes.BadRequest;
    public static bool isForbidden(this UtilitiesStatusCodes statusCode) => statusCode >= UtilitiesStatusCodes.Forbidden;
    public static bool isNotFound(this UtilitiesStatusCodes statusCode) => statusCode >= UtilitiesStatusCodes.NotFound;
}