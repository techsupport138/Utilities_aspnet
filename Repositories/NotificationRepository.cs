namespace Utilities_aspnet.Repositories;

public interface INotificationRepository {
	Task<GenericResponse<IQueryable<NotificationEntity>>> GetNotifications();
	Task<GenericResponse> CreateNotification(NotificationCreateUpdateDto model);
}

public class NotificationRepository : INotificationRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public NotificationRepository(DbContext context, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<IQueryable<NotificationEntity>>> GetNotifications() {
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		IQueryable<NotificationEntity> model = _context.Set<NotificationEntity>()
			.Include(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x.Categories)
			.Where(x => (x.UserId == null || x.UserId == userId) && x.DeletedAt == null).OrderByDescending(x => x.CreatedAt).AsNoTracking().Take(100);

		if (userId != null) {
			foreach (NotificationEntity item in model)
				item.IsFollowing = await _context.Set<FollowEntity>().AnyAsync(x => x.FollowsUserId == item.UserId && x.FollowerUserId == userId);
		}

		return new GenericResponse<IQueryable<NotificationEntity>>(model);
	}

	public async Task<GenericResponse> CreateNotification(NotificationCreateUpdateDto model) {
		NotificationEntity notification = new() {
			UseCase = model.UseCase,
			Link = model.Link,
			Message = model.Message,
			Title = model.Title,
			UserId = model.UserId,
			CreatorUserId = model.CreatorUserId,
			Visited = false
		};
		_context.Set<NotificationEntity>().Add(notification);
		_context.SaveChanges();
		if (model.Media != null) {
			_context.Set<MediaEntity>().Add(new MediaEntity {
				NotificationId = notification.Id,
				CreatedAt = DateTime.Now,
				FileName = model.Media
			});
			_context.SaveChanges();
		}

		return new GenericResponse();
	}
}