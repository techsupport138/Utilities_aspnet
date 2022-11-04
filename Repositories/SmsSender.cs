namespace Utilities_aspnet.Repositories;

public interface ISmsSender {
	void SendSms(string mobileNumber, string message);
}

public class SmsSender : ISmsSender {
	private readonly IConfiguration _config;

	public SmsSender(IConfiguration config) => _config = config;

	public void SendSms(string mobileNumber, string message) {
		AppSettings appSettings = new();
		_config.GetSection("AppSettings").Bind(appSettings);
		SmsPanelSettings smsSetting = appSettings.SmsPanelSettings;

		if (mobileNumber.Contains("+98")) {
			mobileNumber = mobileNumber.TrimStart(new[] {'+'});
			mobileNumber = mobileNumber.TrimStart(new[] {'9'});
			mobileNumber = mobileNumber.TrimStart(new[] {'8'});
		}
		else mobileNumber = mobileNumber.TrimStart(new[] {'0'});

		RestClient client = new("http://188.0.240.110/api/select");
		RestRequest request = new RestRequest(Method.POST);
		request.AddHeader("cache-control", "no-cache");
		request.AddHeader("Content-Type", "application/json");
		request.AddHeader("Authorization", "AccessKey " + smsSetting.SmsApiKey);
		request.AddParameter("undefined",
		                     "{\"op\" : \"pattern\"" + ",\"user\" : \"" + smsSetting.UserName + "\"" + ",\"pass\": \"" + smsSetting.SmsSecret + "\"" +
		                     ",\"fromNum\" : " + "03000505".TrimStart(new[] {'0'}) + "" + ",\"toNum\": " + mobileNumber + "" + ",\"patternCode\": \" " +
		                     smsSetting.PatternCode + "\"" + ",\"inputData\" : [{\"verification-code\":" + message + "}]}", ParameterType.RequestBody);

		client.Execute(request);
	}
}