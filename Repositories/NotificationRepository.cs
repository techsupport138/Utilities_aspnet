namespace Utilities_aspnet.Repositories;

public interface INotificationRepository {
	Task<GenericResponse> Create(NotificationCreateUpdateDto model);
	GenericResponse<IQueryable<NotificationEntity>> Read();
}

public class NotificationRepository : INotificationRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public NotificationRepository(DbContext context, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_httpContextAccessor = httpContextAccessor;
	}

	public GenericResponse<IQueryable<NotificationEntity>> Read() {
		IQueryable<NotificationEntity> model = _context.Set<NotificationEntity>()
			.Include(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x.Categories)
			.Where(x => (x.UserId == null || x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name) && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt)
			.AsNoTracking()
			.Take(100);

		return new GenericResponse<IQueryable<NotificationEntity>>(model);
	}

	public async Task<GenericResponse> Create(NotificationCreateUpdateDto model) {
		NotificationEntity notification = new() {
			UseCase = model.UseCase,
			Link = model.Link,
			Message = model.Message,
			Title = model.Title,
			UserId = model.UserId,
			CreatorUserId = model.CreatorUserId,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
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