namespace Utilities_aspnet.Utilities;

[ApiController]
public abstract class BaseApiController : ControllerBase {
	[NonAction]
	public ObjectResult Result(GenericResponse response) {
		return new ObjectResult(response) {
			StatusCode = response.Status.Value()
		};
	}
}