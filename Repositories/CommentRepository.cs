namespace Utilities_aspnet.Repositories;

public interface ICommentRepository {
	Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto entity);
	Task<GenericResponse<CommentReadDto?>> ToggleLikeComment(Guid commentId);
	Task<GenericResponse<CommentReadDto?>> Read(Guid id);
	Task<GenericResponse<IEnumerable<CommentReadDto>?>> ReadByProductId(Guid id);
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
			.AsNoTracking().Include(x => x.User)!.ThenInclude(x => x.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User)!.ThenInclude(x => x.Media)
			.Include(x => x.Children)!.ThenInclude(x => x.Children)
			.Include(x => x.Children)!.ThenInclude(x => x.LikeComments)
			.Where(x => x.ProductId == id && x.ParentId == null).OrderByDescending(x => x.CreatedAt).ToListAsync();

		IEnumerable<CommentReadDto>? result = _mapper.Map<IEnumerable<CommentReadDto>?>(comment);

		return new GenericResponse<IEnumerable<CommentReadDto>?>(result);
	}

	public async Task<GenericResponse<CommentReadDto?>> Read(Guid id) {
		CommentEntity? comment = await _context.Set<CommentEntity>()
			.AsNoTracking().Include(x => x.User)!.ThenInclude(x => x.Media)
			.Include(x => x.Media)
			.Include(x => x.LikeComments)
			.Include(x => x.Children)!.ThenInclude(x => x.User)!.ThenInclude(x => x.Media)
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();

		CommentReadDto? result = _mapper.Map<CommentReadDto>(comment);

		return new GenericResponse<CommentReadDto?>(result);
	}

	public async Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto entity) {
		//CommentEntity? comment = _mapper.Map<CommentEntity>(entity);
		CommentEntity? comment = new CommentEntity {
			CreatedAt = DateTime.Now,
			Comment = entity.Comment,
			ProductId = entity.ProductId,
			Score = entity.Score,
			ParentId = entity.ParentId
		};

		comment.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;

		_context.Add(comment);
		_context.SaveChanges();

		try {
			ProductEntity? product = _context.Set<ProductEntity>().Include(x => x.Media)
				.FirstOrDefault(x => x.Id == comment.ProductId);
			string? linkMedia = product?.Media?.OrderBy(x => x.CreatedAt).Select(x => x.FileName)?.FirstOrDefault();
			_notificationRepository.CreateNotification(new NotificationCreateUpdateDto {
				UserId = product.UserId,
				Message = "Comment",
				Title = "Comment",
				UseCase = "Comment",
				CreatorUserId = comment.UserId,
				Media = linkMedia,
				Link = product.Id.ToString()
			});
		}
		catch { }

		return await Read(comment.Id);
	}

	public async Task<GenericResponse<CommentReadDto?>> ToggleLikeComment(Guid commentId) {
		string userId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
		CommentEntity? comment = await _context.Set<CommentEntity>().FirstOrDefaultAsync(x => x.Id == commentId);
		LikeCommentEntity? oldLikeComment = await _context.Set<LikeCommentEntity>()
			.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
		if (comment.Score == null) {
			comment.Score = 0;
		}
		if (oldLikeComment != null) {
			comment.Score = comment.Score - 1;
			_context.Set<LikeCommentEntity>().Remove(oldLikeComment);
			await _context.SaveChangesAsync();
		}
		else {
			comment.Score = comment.Score + 1;
			await _context.AddAsync(new LikeCommentEntity {UserId = userId, CommentId = commentId});
			await _context.SaveChangesAsync();
			UserEntity? commectUser = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == comment.UserId);
			if (commectUser != null) {
				commectUser.Point = commectUser.Point + 1;
				_context.SaveChanges();
			}
		}

		return await Read(comment.Id);
	}

	public async Task<GenericResponse<CommentReadDto?>> Update(Guid id, CommentCreateUpdateDto entity) {
		CommentEntity? comment = await _context.Set<CommentEntity>()
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (comment == null)
			return new GenericResponse<CommentReadDto?>(null);

		if (!string.IsNullOrEmpty(entity.Comment))
			comment.Comment = entity.Comment;

		if (entity.Score.HasValue)
			comment.Score = entity.Score;

		if (entity.ProductId.HasValue)
			comment.ProductId = entity.ProductId;

		_context.Set<CommentEntity>().Update(comment);
		await _context.SaveChangesAsync();

		return await Read(comment.Id);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		CommentEntity? comment = await _context.Set<CommentEntity>()
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (comment == null)
			if (comment == null)
				return new GenericResponse(UtilitiesStatusCodes.NotFound, "Comment notfound");

		_context.Set<CommentEntity>().Remove(comment);
		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}