namespace Utilities_aspnet.Repositories;

public interface ICommentRepository {
	Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto dto);
	Task<GenericResponse<CommentReadDto?>> ToggleLikeComment(Guid commentId);
	Task<GenericResponse<CommentReadDto?>> Read(Guid id);
	Task<GenericResponse<IEnumerable<CommentReadDto>?>> ReadByProductId(Guid id);
	GenericResponse<IQueryable<CommentReadDto>?> Filter(CommentFilterDto dto);
	Task<GenericResponse<CommentReadDto?>> Update(Guid id, CommentCreateUpdateDto entity);
	Task<GenericResponse> Delete(Guid id);
}

public class CommentRepository : ICommentRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;
	private readonly INotificationRepository _notificationRepository;

	public CommentRepository(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		INotificationRepository notificationRepository) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_notificationRepository = notificationRepository;
	}

	public async Task<GenericResponse<IEnumerable<CommentReadDto>?>> ReadByProductId(Guid id) {
		IEnumerable<CommentEntity> comment = await _context.Set<CommentEntity>()
			.Include(x => x.User).ThenInclude(x => x!.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.Children)
			.Include(x => x.Children)!.ThenInclude(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media)
			.Where(x => x.ProductId == id && x.ParentId == null && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt).AsNoTracking().ToListAsync();

		return new GenericResponse<IEnumerable<CommentReadDto>?>(_mapper.Map<IEnumerable<CommentReadDto>?>(comment));
	}

	public GenericResponse<IQueryable<CommentReadDto>?> Filter(CommentFilterDto dto) {
		IQueryable<CommentEntity> e = _context.Set<CommentEntity>().Where(x => x.DeletedAt == null);

		if (dto.ProductId.HasValue) e = e.Where(x => x.ProductId == dto.ProductId);
		if (dto.UserId.IsNotNullOrEmpty()) e = e.Where(x => x.UserId == dto.UserId);

		e = e.OrderByDescending(x => x.CreatedAt)
			.Include(x => x.User).ThenInclude(x => x!.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media).OrderByDescending(x => x.CreatedAt)
			.AsNoTracking();

		if (dto.ShowProducts.IsTrue()) e = e.Include(x => x.Product);

		return new GenericResponse<IQueryable<CommentReadDto>?>(_mapper.Map<IQueryable<CommentReadDto>>(e));
	}

	public async Task<GenericResponse<CommentReadDto?>> Read(Guid id) {
		CommentEntity? comment = await _context.Set<CommentEntity>()
			.Include(x => x.User).ThenInclude(x => x!.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media).OrderByDescending(x => x.CreatedAt)
			.Where(x => x.Id == id && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt)
			.AsNoTracking()
			.FirstOrDefaultAsync();

		return new GenericResponse<CommentReadDto?>(_mapper.Map<CommentReadDto>(comment));
	}

	public async Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto dto) {
		string userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;

		CommentEntity comment = new() {
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
			Comment = dto.Comment,
			ProductId = dto.ProductId,
			Score = dto.Score,
			ParentId = dto.ParentId,
			UserId = userId,
		};
		_context.Add(comment);
		await _context.SaveChangesAsync();

		try {
			ProductEntity? product = _context.Set<ProductEntity>()
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

	public async Task<GenericResponse<CommentReadDto?>> ToggleLikeComment(Guid commentId) {
		string userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
		CommentEntity? comment = await _context.Set<CommentEntity>().FirstOrDefaultAsync(x => x.Id == commentId);
		LikeCommentEntity? oldLikeComment = await _context.Set<LikeCommentEntity>()
			.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
		comment.Score ??= 0;
		if (oldLikeComment != null) {
			comment.Score -= 1;
			_context.Set<LikeCommentEntity>().Remove(oldLikeComment);
			await _context.SaveChangesAsync();
		}
		else {
			comment.Score += 1;
			await _context.AddAsync(new LikeCommentEntity {UserId = userId, CommentId = commentId});
			await _context.SaveChangesAsync();
			UserEntity? commectUser = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == comment.UserId);
			if (commectUser != null) {
				commectUser.Point += 1;
				await _context.SaveChangesAsync();
			}
		}

		return await Read(comment.Id);
	}

	public async Task<GenericResponse<CommentReadDto?>> Update(Guid id, CommentCreateUpdateDto entity) {
		CommentEntity? comment = await _context.Set<CommentEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

		if (comment == null) return new GenericResponse<CommentReadDto?>(null);
		if (!string.IsNullOrEmpty(entity.Comment)) comment.Comment = entity.Comment;
		if (entity.Score.HasValue) comment.Score = entity.Score;
		if (entity.ProductId.HasValue) comment.ProductId = entity.ProductId;

		comment.UpdatedAt = DateTime.Now;
		_context.Set<CommentEntity>().Update(comment);
		await _context.SaveChangesAsync();

		return await Read(comment.Id);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		CommentEntity? comment = await _context.Set<CommentEntity>().AsNoTracking().Include(p => p.Children).FirstOrDefaultAsync(x => x.Id == id);
		if (comment == null) return new GenericResponse(UtilitiesStatusCodes.NotFound, "Comment notfound");
		_context.Set<CommentEntity>().Remove(comment);
		await _context.SaveChangesAsync();
		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}