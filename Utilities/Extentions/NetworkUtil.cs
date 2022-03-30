using Microsoft.AspNetCore.Http;

namespace Utilities_aspnet.Extensions
{
    public class NetworkUtil
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor? httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static string? _ServerAddress;
        public static string ServerAddress
        {
            get
            {
                if (_ServerAddress == null)
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    var scheme = request.Scheme;
                    var host = request.Host.ToUriComponent();
                    var pathBase = request.PathBase.ToUriComponent();

                    _ServerAddress = $"{scheme}://{host}{pathBase}";
                }

                return _ServerAddress;
            }
        }
    }
}