using Microsoft.AspNetCore.Http;

namespace Utilities_aspnet.Utilities;

public class Server {
    private static IHttpContextAccessor? _httpContextAccessor;

    public static void Configure(IHttpContextAccessor? httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    private static string? _serverAddress;

    public static string ServerAddress {
        get {
            if (_serverAddress != null) return _serverAddress;
            HttpRequest? request = _httpContextAccessor?.HttpContext?.Request;
            string? scheme = request?.Scheme;
            string? host = request?.Host.ToUriComponent();
            string? pathBase = request?.PathBase.ToUriComponent();
            _serverAddress = $"{scheme}://{host}{pathBase}";
            return _serverAddress;
        }
    }
}