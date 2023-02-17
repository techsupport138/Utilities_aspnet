namespace Utilities_aspnet.Repositories;

public interface ICommentRepository {
	Task<GenericResponse<CommentEntity?>> Create(CommentCreateUpdateDto dto);
	Task<GenericResponse<CommentEntity?>> ToggleLikeComment(Guid commentId);
	Task<GenericResponse<CommentEntity?>> Read(Guid id);
	GenericResponse<IQueryable<CommentEntity>?> ReadByProductId(Guid id);
	GenericResponse<IQueryable<CommentEntity>?> Filter(CommentFilterDto dto);
	Task<GenericResponse<CommentEntity?>> Update(Guid id, CommentCreateUpdateDto dto);
	Task<GenericResponse> Delete(Guid id);
}

public class CommentRepository : ICommentRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly INotificationRepository _notificationRepository;
	
	public CommentRepository(
		DbContext dbContext,
		IHttpContextAccessor httpContextAccessor,
		INotificationRepository notificationRepository) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
		_notificationRepository = notificationRepository;
	}

	public GenericResponse<IQueryable<CommentEntity>?> ReadByProductId(Guid id) {
		IQueryable<CommentEntity> comment = _dbContext.Set<CommentEntity>()
			.Include(x => x.User).ThenInclude(x => x!.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.Children)
			.Include(x => x.Children)!.ThenInclude(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media)
			.Where(x => x.ProductId == id && x.ParentId == null && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt).AsNoTracking();
		return new GenericResponse<IQueryable<CommentEntity>?>(comment);
	}

	public GenericResponse<IQueryable<CommentEntity>?> Filter(CommentFilterDto dto) {
		if (!dto.ShowDeleted)
		{
			IQueryable<CommentEntity> q = _dbContext.Set<CommentEntity>().Where(x => x.DeletedAt == null);

			if (dto.ProductId.HasValue) q = q.Where(x => x.ProductId == dto.ProductId);
			if (dto.Status.HasValue) q = q.Where(x => x.Status == dto.Status);
			if (dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);

			q = q.OrderByDescending(x => x.CreatedAt)
				.Include(x => x.User).ThenInclude(x => x!.Media)
				.Include(x => x.Media)
				.Include(x => x.LikeComments)
				.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media)
				.OrderByDescending(x => x.CreatedAt)
				.AsNoTracking();

			if (dto.ShowProducts.IsTrue()) q = q.Include(x => x.Product).ThenInclude(x => x.Media);

			return new GenericResponse<IQueryable<CommentEntity>?>(q);
		}
		else
		{
			IQueryable<CommentEntity> q = _dbContext.Set<CommentEntity>();

			if (dto.ProductId.HasValue) q = q.Where(x => x.ProductId == dto.ProductId);
			if (dto.Status.HasValue) q = q.Where(x => x.Status == dto.Status);
			if (dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);

			q = q.OrderByDescending(x => x.CreatedAt)
				.Include(x => x.User).ThenInclude(x => x!.Media)
				.Include(x => x.Media)
				.Include(x => x.LikeComments)
				.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media)
				.OrderByDescending(x => x.CreatedAt)
				.AsNoTracking();

			if (dto.ShowProducts.IsTrue()) q = q.Include(x => x.Product).ThenInclude(x => x.Media);

			return new GenericResponse<IQueryable<CommentEntity>?>(q);
		}
	}

	public async Task<GenericResponse<CommentEntity?>> Read(Guid id) {
		CommentEntity? comment = await _dbContext.Set<CommentEntity>()
			.Include(x => x.User).ThenInclude(x => x!.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media)
			.Where(x => x.Id == id && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt)
			.AsNoTracking()
			.FirstOrDefaultAsync();

		return new GenericResponse<CommentEntity?>(comment);
	}

	public async Task<GenericResponse<CommentEntity?>> Create(CommentCreateUpdateDto dto) {
		string userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;

		CommentEntity comment = new() {
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
			Comment = dto.Comment,
			ProductId = dto.ProductId,
			Score = dto.Score,
			ParentId = dto.ParentId,
			UserId = userId,
			Status = dto.Status
		};
		await _dbContext.AddAsync(comment);
		await _dbContext.SaveChangesAsync();

		try {
			ProductEntity? product = _dbContext.Set<ProductEntity>()
				.Include(x => x.Media)
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == comment.ProductId);

			if (product != null && product.UserId != userId) {
				string? linkMedia = product.Media?.OrderBy(x => x.CreatedAt).Select(x => x.FileName).FirstOrDefault();

				await _notificationRepository.Create(new NotificationCreateUpdateDto {
					UserId = product.UserId,
					Message = dto.Comment ?? "",
					Title = "Comment",
					UseCase = "Comment",
					CreatorUserId = comment.UserId,
					Media = linkMedia,
					Link = product.Id.ToString()
				});
			}
		}
		catch { }

		return await Read(comment.Id);
	}

	public async Task<GenericResponse<CommentEntity?>> ToggleLikeComment(Guid commentId) {
		string userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
		CommentEntity? comment = await _dbContext.Set<CommentEntity>().FirstOrDefaultAsync(x => x.Id == commentId);
		LikeCommentEntity? oldLikeComment = await _dbContext.Set<LikeCommentEntity>()
			.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
		comment.Score ??= 0;
		if (oldLikeComment != null) {
			comment.Score -= 1;
			_dbContext.Set<LikeCommentEntity>().Remove(oldLikeComment);
			await _dbContext.SaveChangesAsync();
		}
		else {
			comment.Score += 1;
			await _dbContext.AddAsync(new LikeCommentEntity {UserId = userId, CommentId = commentId});
			await _dbContext.SaveChangesAsync();
			UserEntity? commectUser = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == comment.UserId);
			if (commectUser != null) {
				commectUser.Point += 1;
				await _dbContext.SaveChangesAsync();
			}
		}

		return await Read(comment.Id);
	}

	public async Task<GenericResponse<CommentEntity?>> Update(Guid id, CommentCreateUpdateDto dto) {
		CommentEntity? comment = await _dbContext.Set<CommentEntity>().FirstOrDefaultAsync(x => x.Id == id);

		if (comment == null) return new GenericResponse<CommentEntity?>(null);
		if (!string.IsNullOrEmpty(dto.Comment)) comment.Comment = dto.Comment;
		if (dto.Score.HasValue) comment.Score = dto.Score;
		if (dto.ProductId.HasValue) comment.ProductId = dto.ProductId;
		if (dto.Status.HasValue) comment.Status = dto.Status;

		comment.UpdatedAt = DateTime.Now;
		_dbContext.Set<CommentEntity>().Update(comment);
		await _dbContext.SaveChangesAsync();

		return await Read(comment.Id);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		CommentEntity? comment = await _dbContext.Set<CommentEntity>().Include(p => p.Children).FirstOrDefaultAsync(x => x.Id == id);
		if (comment == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		comment.DeletedAt = DateTime.Now;
		_dbContext.Update(comment);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}
}