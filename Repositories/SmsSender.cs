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
		SmsPanelSettings smsSetting = appSettings.SmsPanelSettings;

        if (mobileNumber.Contains("+98"))
        {
			mobileNumber = mobileNumber.TrimStart(new[] { '+' });
			mobileNumber = mobileNumber.TrimStart(new[] { '9' });
			mobileNumber = mobileNumber.TrimStart(new[] { '8' });

		}
        else
        {
			mobileNumber = mobileNumber.TrimStart(new[] { '0' });
		}


		switch (Sender.FarazSms) {
			case Sender.SmsIr:
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
			case Sender.FarazSms:

				#region FarazSMS

				RestClient client = new("http://188.0.240.110/api/select");
				RestRequest request = new RestRequest(Method.POST);
				//request.Method = Method.Post;

				request.AddHeader("cache-control", "no-cache");
				request.AddHeader("Content-Type", "application/json");
				request.AddHeader("Authorization", "AccessKey U4-OM_COTYg_NBkwWBQtYeUUv1ODRKDrXEYtmtDfyRY=");

				//request.AddParameter("undefined",
				//                     "{\"op\" : \"pattern\"" + ",\"user\" : \"Anborapp\"" + ",\"pass\":  \"Anbor:/3890\"" +
				//                     ",\"fromNum\" : " +
				//                     "03000505".TrimStart(new[] {'0'}) + "" + ",\"toNum\": " +
				//                     mobileNumber.TrimStart(new[] {'0'}) + "" +
				//                     ",\"patternCode\": \"atd5eng0d73h5wh\"" + ",\"inputData\" : [{\"verification-code\":" +
				//                     message +
				//                     "}]}",
				//                     ParameterType.RequestBody);
				
				request.AddParameter("undefined",
				                     "{\"op\" : \"pattern\"" + ",\"user\" : "+ smsSetting.SmsApiKey +"" + ",\"pass\":  "+ smsSetting.SmsSecret + "" +
				                     ",\"fromNum\" : " +
				                     "03000505".TrimStart(new[] {'0'}) + "" + ",\"toNum\": " +
				                     mobileNumber.TrimStart(new[] {'0'}) + "" +
				                     ",\"patternCode\": "+ smsSetting.PatternCode + "" + ",\"inputData\" : [{\"verification-code\":" +
				                     message +
				                     "}]}",
				                     ParameterType.RequestBody);

				//client.ExecutePostAsync(request);
				IRestResponse response = client.Execute(request);

				return 0;

				#endregion

				break;
		}

		return 0;
	}
}