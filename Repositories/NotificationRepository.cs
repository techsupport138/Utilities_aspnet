namespace Utilities_aspnet.Repositories;

public interface INotificationRepository {
	Task<GenericResponse<IEnumerable<NotificationDto>>> GetNotifications();
	Task<GenericResponse> CreateNotification(NotificationCreateUpdateDto model);
}

public class NotificationRepository : INotificationRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public NotificationRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<IEnumerable<NotificationDto>>> GetNotifications() {
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		IEnumerable<NotificationEntity> model = await _context.Set<NotificationEntity>().Include(x => x.Media)
			.Where(x => (x.UserId == null || x.UserId == userId) && x.DeletedAt == null).OrderByDescending(x => x.CreatedAt)
			.ToListAsync();

		IEnumerable<NotificationDto> notificationDtos = _mapper.Map<IEnumerable<NotificationDto>>(model);

		return new GenericResponse<IEnumerable<NotificationDto>>(notificationDtos);
	}

	public async Task<GenericResponse> CreateNotification(NotificationCreateUpdateDto model) {
		NotificationEntity notification = new() {
			UseCase = model.UseCase,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
			Link = model.Link,
			Message = model.Message,
			Title = model.Title,
			UserId = model.UserId,
			Visited = false
		};
		await _context.Set<NotificationEntity>().AddAsync(notification);
		await _context.SaveChangesAsync();
		if (model.Media != null) {
			FileTypes type = FileTypes.Image;
			if (model.Media.EndsWith("svg")) type = FileTypes.Svg;

			await _context.Set<MediaEntity>().AddAsync(new MediaEntity {
				NotificationId = notification.Id, CreatedAt = DateTime.Now, FileType = type, FileName = model.Media, Link = model.Media
			});
			await _context.SaveChangesAsync();
		}

		return new GenericResponse();
	}
}