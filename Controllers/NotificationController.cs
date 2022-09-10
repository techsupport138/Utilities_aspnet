namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseApiController {
	private readonly INotificationRepository _notificationRepository;

	public NotificationController(INotificationRepository notificationRepository) {
		_notificationRepository = notificationRepository;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<GenericResponse<IQueryable<NotificationEntity>>>> Read() => Result(await _notificationRepository.GetNotifications());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(NotificationCreateUpdateDto model) {
		GenericResponse i = await _notificationRepository.CreateNotification(model);
		return Result(i);
	}
}