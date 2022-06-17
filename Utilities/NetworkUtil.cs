namespace Utilities_aspnet.Utilities;

public class NetworkUtil {
    private static IHttpContextAccessor _httpContextAccessor;

    private static string? _ServerAddress;

    public static string ServerAddress {
        get {
            if (_ServerAddress == null) {
                HttpRequest request = _httpContextAccessor.HttpContext.Request;
                string scheme = request.Scheme;
                string host = request.Host.ToUriComponent();
                string pathBase = request.PathBase.ToUriComponent();

                _ServerAddress = $"{scheme}://{host}{pathBase}";
            }

            return _ServerAddress;
        }
    }

    public static void Configure(IHttpContextAccessor? httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }
}