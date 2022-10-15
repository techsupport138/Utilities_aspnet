namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
	public static IEnumerable<IdTitleDto> GetValues<T>() {
		return (from int itemType in Enum.GetValues(typeof(T))
			select new IdTitleDto {Title = Enum.GetName(typeof(T), itemType), Id = itemType}).ToList();
	}
}

public static class UtilitiesStatusCodesExtension {
	public static bool IsSuccessful(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Success;
	public static bool IsBadRequest(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.BadRequest;
	public static bool IsForbidden(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.Forbidden;
	public static bool IsNotFound(this UtilitiesStatusCodes statusCode) => statusCode == UtilitiesStatusCodes.NotFound;
	public static int Value(this UtilitiesStatusCodes statusCode) => (int) statusCode;
}

public enum UtilitiesStatusCodes {
	Success = 200,
	BadRequest = 400,
	Forbidden = 403,
	NotFound = 404,
	Unhandled = 900,
	WrongVerificationCode = 602
}

public enum OtpResult {
	Ok = 1,
	Incorrect = 2,
	TimeOut = 3
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
	PayAtHome
}

public enum SendType {
	Pishtaz,
	Custome,
	Tipax
}

public enum FormFieldType {
	SingleLineText,
	MultiLineText,
	MultiSelect,
	SingleSelect,
	Bool,
	Number,
	File,
	Image,
	CarPlack,
	PhoneNumber,
	Password,
}

public enum TransactionStatus {
	Fail = -1,
	Pending = 0,
	Success = 100
}

public enum ProductStatus {
	Released,
	Expired,
	InQueue,
	Deleted
}

public enum Sender {
	SmsIr,
	Faraz
}

public enum Currency {
	Rial = 100,
	Dolor = 101,
	Lira = 102,
	Euro = 103,
	Btc = 200
}

public enum SeenStatus {
	UnSeen = 100,
	Seen = 101,
	SeenDetail = 102,
	Ignored = 103,
	Deleted = 104,
}

public enum ChatStatus {
	Open = 100,
	Closes = 101,
	WaitingForHost = 102,
	Answered = 103,
	Deleted = 104
}

public enum Priority {
	VeryHigh = 100,
	High = 101,
	Normal = 102,
	Low = 103
}