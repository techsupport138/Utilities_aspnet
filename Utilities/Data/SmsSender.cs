using RestSharp;

namespace Utilities_aspnet.Utilities.Data {
    public interface ISmsSender {
        void SendSms(string mobileNumber, string message);
    }

    public class SmsSender : ISmsSender {
        public void SendSms(string mobileNumber, string message) {
            #region FarazSMS

            RestClient client = new("http://188.0.240.110/api/select");
            RestRequest request = new(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "AccessKey U4-OM_COTYg_NBkwWBQtYeUUv1ODRKDrXEYtmtDfyRY=");

            request.AddParameter("undefined",
                "{\"op\" : \"pattern\"" + ",\"user\" : \"09130269500\"" + ",\"pass\":  \"C1System\"" + ",\"fromNum\" : " +
                "03000505".TrimStart(new[] {'0'}) + "" + ",\"toNum\": " + mobileNumber.TrimStart(new[] {'0'}) + "" +
                ",\"patternCode\": \"whrqg2ad1r\"" + ",\"inputData\" : [{\"verification-code\":" + message + "}]}",
                ParameterType.RequestBody);

            client.Execute(request);

            #endregion
        }
    }
}