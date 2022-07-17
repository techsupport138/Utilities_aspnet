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
			.Include(x => x.CreatorUser).ThenInclude(x => x.Media)
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
			CreatorUserId = model.CreatorUserId,
			Visited = false
		};
		_context.Set<NotificationEntity>().Add(notification);
		_context.SaveChanges();
		if (model.Media != null)
		{
			FileTypes type = FileTypes.Image;
			if (model.Media.EndsWith("svg")) type = FileTypes.Svg;

			_context.Set<MediaEntity>().Add(new MediaEntity
			{
				NotificationId = notification.Id,
				CreatedAt = DateTime.Now,
				FileType = type,
				FileName = model.Media,
				Link = model.Media
			});
			_context.SaveChanges();
		}

		return new GenericResponse();
	}
}