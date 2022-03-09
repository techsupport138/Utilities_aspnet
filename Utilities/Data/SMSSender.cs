using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using RestSharp;
using Utilities_aspnet.Core;

namespace Utilities_aspnet.Utilities.Date
{
    public interface ISMSSender
    {
        bool SendVerificationCode(string num, string password);
    }
    public class SMSSender : ISMSSender
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public SMSSender(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        public bool SendVerificationCode(string num, string verificationCode)
        {

            ///////////////////فراز
            var client = new RestClient("http://188.0.240.110/api/select");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "AccessKey U4-OM_COTYg_NBkwWBQtYeUUv1ODRKDrXEYtmtDfyRY=");

            request.AddParameter("undefined", "{\"op\" : \"pattern\"" +
                ",\"user\" : \"09130269500\"" +
                ",\"pass\":  \"C1System\"" +
                ",\"fromNum\" : " + "03000505".TrimStart(new Char[] { '0' }) + "" +
                ",\"toNum\": " + num.TrimStart(new Char[] { '0' }) + "" +
                ",\"patternCode\": \"whrqg2ad1r\"" +
                ",\"inputData\" : [{\"verification-code\":" + verificationCode + "}]}"
                , ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //////////////////فراز
            return true;
        }

    }
}
