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
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<NotificationDto>>>> Read() {
		GenericResponse<IEnumerable<NotificationDto>> i = await _notificationRepository.GetNotifications();
		return Result(i);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse>> Create(NotificationCreateUpdateDto model) {
		GenericResponse i = await _notificationRepository.CreateNotification(model);
		return Result(i);
	}
}