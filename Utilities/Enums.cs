namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
	public static IEnumerable<CategoryReadDto> GetValues<T>() {
		return (from int itemType in Enum.GetValues(typeof(T))
			select new CategoryReadDto {Title = Enum.GetName(typeof(T), itemType), SecondaryId = itemType}).ToList();
	}
}

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

public enum OtpResult {
	Ok = 1,
	Incorrect = 2,
	TimeOut = 3
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

public enum OrderStatuses {
	Pending = 0,
	Canceled = 1,
	Paid = 2
}

public enum LocationType {
	Planet = 0,
	Continent = 1,
	Country = 2,
	City = 3,
	Region = 4
}

public enum ApprovalStatus {
	Pending = 0,
	Approved = 1,
	Rejected = 2
}

public enum PayType
{
	Online,
	PayAtHome,
}

public enum SendType
{
	Pishtaz,
	Custome,
	Tipax
}