namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class NotificationController : BaseApiController {
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(INotificationRepository notificationRepository) {
        _notificationRepository = notificationRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<NotificationDto>>>> GetNotifications()
    {
        GenericResponse<IEnumerable<NotificationDto>> i = await _notificationRepository.GetNotifications();
        return Result(i);
    }


    [HttpPost]
    public async Task<ActionResult<GenericResponse>> PostNotification(CreateNotificationDto model)
    {
        GenericResponse i = await _notificationRepository.CreateNotification(model);
        return Result(i);
    }
}