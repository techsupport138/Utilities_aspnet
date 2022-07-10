namespace Utilities_aspnet.Utilities;

public class ResponseTimeMiddleware {
	private const string ResponseHeaderResponseTime = "X-Response-Time-ms";
	private readonly RequestDelegate _next;
	public ResponseTimeMiddleware(RequestDelegate next) => _next = next;

	public Task InvokeAsync(HttpContext context) {
		Stopwatch watch = new();
		watch.Start();
		context.Response.OnStarting(() => {
			watch.Stop();
			long responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
			context.Response.Headers[ResponseHeaderResponseTime] = responseTimeForCompleteRequest.ToString();
			return Task.CompletedTask;
		});
		return _next(context);
	}
}