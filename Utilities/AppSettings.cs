namespace Utilities_aspnet.Utilities;

public class AppSettings {
	public SmsPanelSettings? SmsPanelSettings { get; set; }
	public Pushe? Pushe { get; set; }
}

public class SmsPanelSettings {
	public string? UserName { get; set; }
	public string? LineNumber { get; set; }
	public string? SmsApiKey { get; set; }
	public string? SmsSecret { get; set; }
	public int? OtpId { get; set; }
	public string? PatternCode { get; set; }
}

public class Pushe {
	public string? AccessToken { get; set; }
	public string? Applications { get; set; }
}