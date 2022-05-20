namespace Utilities_aspnet.Utilities.Enums;

public enum UtilitiesStatusCodes {
    Success = 200,
    BadRequest = 400,
    Forbidden = 403,
    NotFound = 404,
    Unhandled = 900,
    New = 499,
    WrongMobile = 601,
    WrongVerificationCode = 602,
}

public static class UtilitiesStatusCodesExtension {
    public static bool isSuccessful(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Success;
    public static bool isBadRequest(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.BadRequest;
    public static bool isForbidden(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Forbidden;
    public static bool isNotFound(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.NotFound;
    public static int value(this UtilitiesStatusCodes statusCode) => (int) statusCode;
}

public enum ContentUseCase {
    AboutUs = 0,
    Terms = 1,
    OnBoarding = 2,
    HomeSlider = 3,
    SplashScreen = 100,
    Learn = 101,
    Monthly = 102,
    Newsletters = 103,
}

public enum VisibilityType {
    Public = 0,
    Private = 1,
    UsersOnly = 2,
    Followers = 3
}

public enum DatabaseType {
    SqlServer = 0,
    MySql = 1
}

public enum FileTypes {
    Image = 0,
    Svg = 1,
    Gif = 2,
    Video = 3,
    Voice = 4,
    Pdf = 5,
    Txt = 6
}

public enum LocationType {
    Planet = 0,
    Continent = 1,
    Country = 2,
    City = 3,
    Region = 4,
}