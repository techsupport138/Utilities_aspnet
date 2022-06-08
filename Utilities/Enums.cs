namespace Utilities_aspnet.Utilities;

public enum UtilitiesStatusCodes {
    Success = 200,
    BadRequest = 400,
    Forbidden = 403,
    NotFound = 404,
    Unhandled = 900,
    New = 499,
    WrongMobile = 601,
    WrongVerificationCode = 602
}

public static class UtilitiesStatusCodesExtension {
    public static bool IsSuccessful(this UtilitiesStatusCodes statusCode) {
        return statusCode == UtilitiesStatusCodes.Success;
    }

    public static bool IsBadRequest(this UtilitiesStatusCodes statusCode) {
        return statusCode == UtilitiesStatusCodes.BadRequest;
    }

    public static bool IsForbidden(this UtilitiesStatusCodes statusCode) {
        return statusCode == UtilitiesStatusCodes.Forbidden;
    }

    public static bool IsNotFound(this UtilitiesStatusCodes statusCode) {
        return statusCode == UtilitiesStatusCodes.NotFound;
    }

    public static int Value(this UtilitiesStatusCodes statusCode) {
        return (int) statusCode;
    }
}

public enum ContentUseCase {
    AboutUs = 0,
    Terms = 1,
    OnBoarding = 2,
    HomeSlider = 3,
    SplashScreen = 100,
    Learn = 101,
    Monthly = 102,
    Newsletters = 103
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
    Txt = 6,
    Link = 7
}

public enum LocationType {
    Planet = 0,
    Continent = 1,
    Country = 2,
    City = 3,
    Region = 4
}

public enum ApprovalStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}