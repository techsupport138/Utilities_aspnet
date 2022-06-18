namespace Utilities_aspnet.Repositories;

public interface ISmsSender {
	long SendSms(string mobileNumber, string message);
}

public class SmsSender : ISmsSender {
	private readonly IConfiguration _config;
	private readonly DbContext _context;
	private readonly AppSettings _setting;

	public SmsSender(IConfiguration config, AppSettings setting, DbContext context) {
		_config = config;
		_setting = setting;
		_context = context;
	}

	public long SendSms(string mobileNumber, string message) {
		AppSettings appSettings = new();
		_config.GetSection("AppSettings").Bind(appSettings);
		SMSPanelSettings smsSetting = appSettings.SMSPanelSettings;
		switch (smsSetting.Sender) {
			case Sender.SMS_Ir:
				// string? token = new Token()
				//     .GetToken(smsSetting.SmsApiKey, smsSetting.SmsSecret);
				// MessageSendObject? messageSendObject = new() {
				//     Messages = new IEnumerable<string> {message}.ToArray(),
				//     MobileNumbers = new IEnumerable<string> {mobileNumber}.ToArray(),
				//     LineNumber = smsSetting.LineNumber,
				//     SendDateTime = null,
				//     CanContinueInCaseOfError = true
				// };
				//
				// MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);
				//var _message = new Message()
				//{
				//    MessageTypeId = 1,
				//    MessageTo = num,
				//    MessageBody = message,
				//    DeliverId = messageSendResponseObject.Ids[0].ID,
				//    MessageFrom = _setting.SMSPanelSettings.LineNumber.ToString(),
				//    DeliverMessage = messageSendResponseObject.Message,
				//    IsSuccessful = messageSendResponseObject.IsSuccessful
				//};
				//_context.Messages.Add(_message);
				//_context.SaveChanges();
				// if (messageSendResponseObject.IsSuccessful)
				// return messageSendResponseObject.Ids[0].ID;
				// else
				// return -1;
				break;
			case Sender.FarazSMS:

				#region FarazSMS

				RestClient client = new("http://188.0.240.110/api/select");
				RestRequest request = new(); //  new(Method.Post);
				request.Method = Method.Post;

				request.AddHeader("cache-control", "no-cache");
				request.AddHeader("Content-Type", "application/json");
				request.AddHeader("Authorization", "AccessKey U4-OM_COTYg_NBkwWBQtYeUUv1ODRKDrXEYtmtDfyRY=");

				request.AddParameter("undefined",
				                     "{\"op\" : \"pattern\"" + ",\"user\" : \"09130269500\"" + ",\"pass\":  \"C1System\"" +
				                     ",\"fromNum\" : " +
				                     "03000505".TrimStart(new[] {'0'}) + "" + ",\"toNum\": " +
				                     mobileNumber.TrimStart(new[] {'0'}) + "" +
				                     ",\"patternCode\": \"whrqg2ad1r\"" + ",\"inputData\" : [{\"verification-code\":" + message +
				                     "}]}",
				                     ParameterType.RequestBody);

				client.ExecutePostAsync(request);
				return 0;

				#endregion

				break;
		}

		return 0;
	}
}