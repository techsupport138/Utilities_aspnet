using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Utilities_aspnet.Utilities.Data
{
    public interface ISmsSender
    {
        bool SendVerificationCode(string? num, string password);
    }

    public class SmsSender : ISmsSender
    {
        private readonly IConfiguration _config;
        private readonly DbContext _context;

        public SmsSender(IConfiguration config, DbContext context)
        {
            _config = config;
            _context = context;
        }

        public bool SendVerificationCode(string? num, string verificationCode)
        {
            ///////////////////Faraz
            RestClient client = new RestClient("http://188.0.240.110/api/select");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "AccessKey U4-OM_COTYg_NBkwWBQtYeUUv1ODRKDrXEYtmtDfyRY=");

            request.AddParameter("undefined", "{\"op\" : \"pattern\"" +
                                              ",\"user\" : \"09130269500\"" +
                                              ",\"pass\":  \"C1System\"" +
                                              ",\"fromNum\" : " + "03000505".TrimStart(new Char[] {'0'}) + "" +
                                              ",\"toNum\": " + num.TrimStart(new Char[] {'0'}) + "" +
                                              ",\"patternCode\": \"whrqg2ad1r\"" +
                                              ",\"inputData\" : [{\"verification-code\":" + verificationCode + "}]}"
                , ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //////////////////Faraz
            return true;
        }
    }
}