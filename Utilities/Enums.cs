namespace Utilities_aspnet.Utilities;

public static class EnumExtension
{
    public static IEnumerable<IdTitleDto> GetValues<T>()
    {
        return (from int itemType in Enum.GetValues(typeof(T))
                select new IdTitleDto { Title = Enum.GetName(typeof(T), itemType), Id = itemType }).ToList();
    }
}

public static class UtilitiesStatusCodesExtension
{
    public static int Value(this UtilitiesStatusCodes statusCode) => (int)statusCode;
}

public enum UtilitiesStatusCodes
{
	Success = 200,
	BadRequest = 400,
	Forbidden = 403,
	NotFound = 404,
	Unhandled = 900,
	WrongVerificationCode = 601,
	MaximumLimitReached = 602,
	UserAlreadyExist = 603,
	UserSuspended = 604,
	UserNotFound = 605,
	MultipleSeller = 607
}

public enum OtpResult
{
    Ok = 1,
    Incorrect = 2,
    TimeOut = 3
}

public enum DatabaseType
{
    SqlServer = 0,
    MySql = 1
}

public enum OrderStatuses
{
    Pending = 100,
    Canceled = 101,
    Paid = 102,
    Accept = 103,
    Reject = 104,
    InProgress = 105,
    InProcess = 106,
    Shipping = 107,//not used yet
    Refund = 108,//not used yet
    RefundComplete = 109,//not used yet
    Complete = 110

}

public enum PayType
{
    Online,
    PayAtHome,
    Cash = 101,
    Stripe = 102,
    Coin = 103,//not used yet
    Paypal = 104,//not used yet
    Visa = 105//not used yet
}

public enum SendType
{
    Pishtaz,
    Custome,
    Tipax
}

public enum FormFieldType
{
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

public enum TransactionStatus
{
    Pending = 100,
    Fail = 101,
    Success = 102
}

public enum ProductStatus
{
    Released = 1,
    Expired = 2,
    InQueue = 3,
    Deleted = 4
}

public enum Currency
{
    Rial = 100,
    Dolor = 101,
    Lira = 102,
    Euro = 103,
    Btc = 200
}

public enum SeenStatus
{
    UnSeen = 100,
    Seen = 101,
    SeenDetail = 102,
    Ignored = 103,
    Deleted = 104
}

public enum ChatStatus
{
    Open = 100,
    Closes = 101,
    WaitingForHost = 102,
    Answered = 103,
    Deleted = 104
}

public enum Priority
{
    VeryHigh = 100,
    High = 101,
    Normal = 102,
    Low = 103
}

public enum OrderType
{
    Sale = 100,
    Purchase = 101,
    All = 102
}

public enum Reaction
{
    None = 100,
    Like = 101,
    DissLike = 102,
    Funny = 103,
    Awful = 104
}

public enum AgeCategory
{
    None = 100,
    Kids = 101,
    Tennager = 102,
    Young = 103,
    Adult = 104
}

public enum OrderReportType
{
    OrderDate = 100,
    OrderDateProductUseCase = 101,
    OrderProductUseCase = 102,
    OrderState = 103,
    OrderStuse = 104,
    All = 105
}
public enum ReportType
{
    Insight = 100,
    TopKeyword = 101,
    PercentUsecase = 102,
    All = 105
}
public enum ReferenceIdType
{
    Product = 100,
    Category = 101,
    User = 102
}