namespace Utilities_aspnet.Utilities;

public class AppSettings {
	public SmsPanelSettings? SmsPanelSettings { get; set; }
	public Pushe? Pushe { get; set; }
	public SeoSetting? SeoSetting { get; set; }
}

public enum Sender {
	SmsIr,
	FarazSms
}

public class SmsPanelSettings {
	public Sender? Sender { get; set; }
	public string? LineNumber { get; set; }
	public string? SmsApiKey { get; set; }
	public string? SmsSecret { get; set; }
	public int? OtpId { get; set; }
}

public class Pushe {
	public string? AccessToken { get; set; }
	public string? Applications { get; set; }
}

public class SeoSetting {
	public string? GoogleAnalyticsCode { get; set; }
	public string? AlexaCode { get; set; }
}