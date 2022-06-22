namespace Utilities_aspnet.Utilities;

public class Server {
	private static IHttpContextAccessor? _httpContextAccessor;

	private static string? _serverAddress;
	private static string? _userId;

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
	
	public static string? UserId {
		get {
			_userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
			return _userId;
		}
	}

	public static void Configure(IHttpContextAccessor? httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
}