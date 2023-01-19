namespace Utilities_aspnet.Repositories;

public interface IFollowBookmarkRepository {
	GenericResponse<IQueryable<UserEntity>> GetFollowers(string id);
	GenericResponse<IQueryable<UserEntity>> GetFollowing(string id);
	Task<GenericResponse> ToggleFollow(string sourceUserId, FollowCreateDto dto);
	Task<GenericResponse> RemoveFollowings(string targetUserId, FollowCreateDto dto);
	GenericResponse<IQueryable<BookmarkEntity>?> ReadBookmarks();
	Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto);
}

public class FollowBookmarkRepository : IFollowBookmarkRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly INotificationRepository _notificationRepository;
	
	public FollowBookmarkRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor, INotificationRepository notificationRepository) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
		_notificationRepository = notificationRepository;
	}

	public async Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto) {
		BookmarkEntity? oldBookmark = _dbContext.Set<BookmarkEntity>()
			.FirstOrDefault(x => (x.ProductId != null && x.ProductId == dto.ProductId ||
			                      x.CategoryId != null && x.CategoryId == dto.CategoryId) &&
			                     x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!);
		if (oldBookmark == null) {
			BookmarkEntity bookmark = new() {UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!};

			if (dto.ProductId.HasValue) bookmark.ProductId = dto.ProductId;
			bookmark.FolderName = dto.FolderName;

			await _dbContext.Set<BookmarkEntity>().AddAsync(bookmark);
			await _dbContext.SaveChangesAsync();
		}
		else {
			_dbContext.Set<BookmarkEntity>().Remove(oldBookmark);
			await _dbContext.SaveChangesAsync();
		}

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

	public GenericResponse<IQueryable<BookmarkEntity>?> ReadBookmarks() {
		IQueryable<BookmarkEntity> bookmark = _dbContext.Set<BookmarkEntity>()
			.Where(x => x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)
			.Include(x => x.Product).ThenInclude(x => x.Media)
			.Include(x => x.Product).ThenInclude(i => i.Votes)
			.Include(x => x.Product).ThenInclude(i => i.User).ThenInclude(x => x.Media)
			.Include(x => x.Product).ThenInclude(i => i.Bookmarks)
			.Include(x => x.Product).ThenInclude(i => i.Forms)!.ThenInclude(x => x.FormField)
			.Include(x => x.Product).ThenInclude(i => i.Categories)
			.Include(x => x.Product).ThenInclude(i => i.Comments.Where(x => x.ParentId == null)).ThenInclude(x => x.Children)
			.Include(x => x.Product).ThenInclude(i => i.Reports)
			.Include(x => x.Product).ThenInclude(i => i.Teams)!.ThenInclude(x => x.User)!.ThenInclude(x => x.Media);
		return new GenericResponse<IQueryable<BookmarkEntity>?>(bookmark);
	}

	public GenericResponse<IQueryable<UserEntity>> GetFollowers(string id) {
		IQueryable<UserEntity?> followers = _dbContext.Set<FollowEntity>()
			.Where(x => x.FollowsUserId == id)
			.Include(x => x.FollowerUser).ThenInclude(x => x.Media)
			.Include(x => x.FollowerUser).ThenInclude(x => x.Categories).ThenInclude(x => x.Media)
			.AsNoTracking().Select(x => x.FollowerUser);

		return new GenericResponse<IQueryable<UserEntity>>(followers);
	}

	public GenericResponse<IQueryable<UserEntity>> GetFollowing(string id) {
		IQueryable<UserEntity?> followings = _dbContext.Set<FollowEntity>()
			.Where(x => x.FollowerUserId == id)
			.Include(x => x.FollowsUser).ThenInclude(x => x.Media)
			.Include(x => x.FollowsUser).ThenInclude(x => x.Categories).ThenInclude(x => x.Media)
			.AsNoTracking()
			.Select(x => x.FollowsUser);

		return new GenericResponse<IQueryable<UserEntity>>(followings);
	}

	public async Task<GenericResponse> ToggleFollow(string sourceUserId, FollowCreateDto parameters) {
		UserEntity? myUser = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == sourceUserId);

		FollowEntity? follow = await _dbContext.Set<FollowEntity>()
			.FirstOrDefaultAsync(x => x.FollowerUserId == sourceUserId && x.FollowsUserId == parameters.UserId);

		if (follow != null) {
			_dbContext.Set<FollowEntity>().Remove(follow);
			await _dbContext.SaveChangesAsync();
		}
		else {
			follow = new FollowEntity {
				FollowerUserId = sourceUserId,
				FollowsUserId = parameters.UserId
			};

			await _dbContext.Set<FollowEntity>().AddAsync(follow);
			await _dbContext.SaveChangesAsync();

			UserEntity? followsUser = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == parameters.UserId);

			if (followsUser != null) {
				followsUser.Point += 1;
				await _dbContext.SaveChangesAsync();
			}

			try {
				await _notificationRepository.Create(new NotificationCreateUpdateDto {
					UserId = parameters.UserId,
					Message = "You are being followed by " + myUser?.UserName,
					Title = "Follow",
					UseCase = "Follow",
					CreatorUserId = sourceUserId
				});
			}
			catch { }
		}

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

	public async Task<GenericResponse> RemoveFollowings(string userId, FollowCreateDto parameters) {
		FollowEntity? following = await _dbContext.Set<FollowEntity>()
			.Where(x => x.FollowerUserId == parameters.UserId && x.FollowsUserId == userId).FirstOrDefaultAsync();
		if (following != null) {
			_dbContext.Set<FollowEntity>().Remove(following);
			await _dbContext.SaveChangesAsync();
		}

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}