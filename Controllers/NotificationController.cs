namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseApiController {
	private readonly INotificationRepository _repository;

	public NotificationController(INotificationRepository repository) => _repository = repository;

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpGet]
	public ActionResult<GenericResponse<IQueryable<NotificationEntity>>> Read() => Result(_repository.Read());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(NotificationCreateUpdateDto model) => Result(await _repository.Create(model));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[HttpPost("UpdateSeenStatus")]
	public async Task<ActionResult<GenericResponse>> UpdateSeenStatus(IEnumerable<Guid> ids, SeenStatus seenStatus)
		=> Result(await _repository.UpdateSeenStatus(ids, seenStatus));
}