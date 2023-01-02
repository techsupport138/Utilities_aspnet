namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
	public static IEnumerable<IdTitleDto> GetValues<T>() {
		return (from int itemType in Enum.GetValues(typeof(T))
			select new IdTitleDto {Title = Enum.GetName(typeof(T), itemType), Id = itemType}).ToList();
	}
}

public static class UtilitiesStatusCodesExtension {
	public static int Value(this UtilitiesStatusCodes statusCode) => (int) statusCode;
}

public enum UtilitiesStatusCodes {
	Success = 200,
	BadRequest = 400,
	Forbidden = 403,
	NotFound = 404,
	Unhandled = 900,
	WrongVerificationCode = 601,
	MaximumLimitReached = 602,
	UserAlreadyExist = 603,
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
	Pending = 100,
	Canceled = 101,
	Paid = 102
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
	Date,
	Time,
	DateTime
}

public enum TransactionStatus {
	Pending = 100,
	Fail = 101,
	Success = 102
}

public enum ProductStatus {
	Released = 1,
	Expired = 2,
	InQueue = 3,
	Deleted = 4
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
	Deleted = 104
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