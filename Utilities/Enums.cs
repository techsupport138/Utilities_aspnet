namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
	public static IEnumerable<CategoryReadDto> GetValues<T>() {
		return (from int itemType in Enum.GetValues(typeof(T)) select new CategoryReadDto {Title = Enum.GetName(typeof(T), itemType), SecondaryId = itemType})
			.ToList();
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
	public static bool IsSuccessful(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Success;

	public static bool IsBadRequest(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.BadRequest;

	public static bool IsForbidden(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Forbidden;

	public static bool IsNotFound(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.NotFound;

	public static int Value(this UtilitiesStatusCodes statusCode) => (int) statusCode;
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

public enum PayType {
	Online,
	PayAtHome,
}

public enum SendType {
	Pishtaz,
	Custome,
	Tipax
}