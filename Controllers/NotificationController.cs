using Utilities_aspnet.Repositories;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationController : BaseApiController {
	private readonly INotificationRepository _notificationRepository;

	public NotificationController(INotificationRepository notificationRepository) {
		_notificationRepository = notificationRepository;
	}

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<NotificationDto>>>> Read() {
		GenericResponse<IEnumerable<NotificationDto>> i = await _notificationRepository.GetNotifications();
		return Result(i);
	}

	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(NotificationCreateUpdateDto model) {
		GenericResponse i = await _notificationRepository.CreateNotification(model);
		return Result(i);
	}
}