namespace Utilities_aspnet.Repositories;

public interface INotificationRepository {
	Task<GenericResponse> Create(NotificationCreateUpdateDto model);
	GenericResponse<IQueryable<NotificationEntity>> Read();
	Task<GenericResponse> UpdateSeenStatus(IEnumerable<Guid> ids, SeenStatus seenStatus);
}

public class NotificationRepository : INotificationRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public NotificationRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
	}

	public GenericResponse<IQueryable<NotificationEntity>> Read() {
		IQueryable<NotificationEntity> i = _dbContext.Set<NotificationEntity>()
			.Include(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x!.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x!.Categories)
			.Where(x => (x.UserId == null || x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name) && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt)
			.AsNoTracking()
			.Take(100);

		return new GenericResponse<IQueryable<NotificationEntity>>(i);
	}

	public async Task<GenericResponse> UpdateSeenStatus(IEnumerable<Guid> ids, SeenStatus seenStatus) {
		IQueryable<NotificationEntity> i = _dbContext.Set<NotificationEntity>()
			.Include(x => x.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x!.Media)
			.Include(x => x.CreatorUser).ThenInclude(x => x!.Categories)
			.Where(x => (x.UserId == null || x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name) && x.DeletedAt == null)
			.Where(x => ids.Contains(x.Id))
			.OrderByDescending(x => x.CreatedAt);

		foreach (NotificationEntity e in i) {
			e.SeenStatus = seenStatus;
			_dbContext.Update(e);
		}
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
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
		await _dbContext.Set<NotificationEntity>().AddAsync(notification);
		await _dbContext.SaveChangesAsync();
		if (model.Media != null) {
			await _dbContext.Set<MediaEntity>().AddAsync(new MediaEntity {
				NotificationId = notification.Id,
				CreatedAt = DateTime.Now,
				FileName = model.Media
			});
			await _dbContext.SaveChangesAsync();
		}

		return new GenericResponse();
	}
}