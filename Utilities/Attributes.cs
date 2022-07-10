namespace Utilities_aspnet.Utilities;

public class ResponseTimeMiddleware {
	// Name of the Response Header, Custom Headers starts with "X-"  
	private const string ResponseHeaderResponseTime = "X-Response-Time-ms";

	// Handle to the next Middleware in the pipeline  
	private readonly RequestDelegate _next;

	public ResponseTimeMiddleware(RequestDelegate next) {
		_next = next;
	}

	public Task InvokeAsync(HttpContext context) {
		// Start the Timer using Stopwatch  
		Stopwatch watch = new Stopwatch();
		watch.Start();
		context.Response.OnStarting(() => {
			// Stop the timer information and calculate the time   
			watch.Stop();
			long responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
			// Add the Response time information in the Response headers.   
			context.Response.Headers[ResponseHeaderResponseTime] = responseTimeForCompleteRequest.ToString();
			return Task.CompletedTask;
		});
		// Call the next delegate/middleware in the pipeline   
		return _next(context);
	}
}