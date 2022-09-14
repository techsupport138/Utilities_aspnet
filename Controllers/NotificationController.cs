namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseApiController {
	private readonly INotificationRepository _notificationRepository;

	public NotificationController(INotificationRepository notificationRepository) => _notificationRepository = notificationRepository;

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet]
	public ActionResult<GenericResponse<IQueryable<NotificationEntity>>> Read() => Result(_notificationRepository.Read());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(NotificationCreateUpdateDto model) => Result(await _notificationRepository.Create(model));
}