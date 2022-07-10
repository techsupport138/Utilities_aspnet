namespace Utilities_aspnet.Utilities;

public class BenchmarkAttribute : ActionFilterAttribute {
	public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
		base.OnActionExecuting(actionContext);
		actionContext.Request.Properties[actionContext.ActionDescriptor.ActionName] = Stopwatch.StartNew();
	}

	public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
		base.OnActionExecuted(actionExecutedContext);
		Stopwatch stopwatch =
			(Stopwatch) actionExecutedContext.Request.Properties[actionExecutedContext.ActionContext.ActionDescriptor.ActionName];

		if (actionExecutedContext.Response is {Content: { }}) {
			actionExecutedContext.Response.Content.Headers.TryAddWithoutValidation(
				"Execution-Time", stopwatch.ElapsedMilliseconds.ToString());
		}
	}
}