namespace Utilities_aspnet.Utilities;

public class AppSettings {
    public SMSPanelSettings SMSPanelSettings { get; set; }
    public pushe Pushe { get; set; }
    public seoSetting SeoSetting { get; set; }
}

public enum Sender {
    SMS_Ir,
    FarazSMS
}

public class SMSPanelSettings {
    public Sender Sender { get; set; }
    public string LineNumber { get; set; }
    public string SmsApiKey { get; set; }
    public string SmsSecret { get; set; }
    public int OTPId { get; set; }
}

public class pushe {
    public string AccessToken { get; set; }
    public string Applications { get; set; }
}

public class seoSetting {
    public string GoogleAnalyticsCode { get; set; }
    public string AlexaCode { get; set; }
}