namespace Utilities_aspnet.Repositories;

public interface ICommentRepository {
	Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto entity);
	Task<GenericResponse<CommentReadDto?>> Read(Guid id);
	Task<GenericResponse<IEnumerable<CommentReadDto>?>> ReadByProductId(Guid id);
	Task<GenericResponse<CommentReadDto?>> Update(Guid id, CommentCreateUpdateDto entity);
	Task<GenericResponse> Delete(Guid id);
}

public class CommentRepository : ICommentRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public CommentRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<IEnumerable<CommentReadDto>?>> ReadByProductId(Guid id) {
		IEnumerable<CommentEntity> ? comment = await _context.Set<CommentEntity>()
			.AsNoTracking().Include(x=>x.User)!.ThenInclude(x => x.Media)
			.Include(x=>x.Children)!.ThenInclude(x=>x.User)!.ThenInclude(x => x.Media)
			.Include(x => x.Children)!.ThenInclude(x=>x.Children)
			.Where(x => x.ProductId == id && x.ParentId == null).OrderBy(x=>x.CreatedAt).ToListAsync();

		IEnumerable<CommentReadDto>? result = _mapper.Map<IEnumerable<CommentReadDto>?>(comment);

		return new GenericResponse<IEnumerable<CommentReadDto>?> (result);
	}
	
	public async Task<GenericResponse<CommentReadDto?>> Read(Guid id) {
		CommentEntity? comment = await _context.Set<CommentEntity>()
			.AsNoTracking().Include(x=>x.User)!.ThenInclude(x => x.Media)
			.Include(x=>x.Children)!.ThenInclude(x=>x.User)!.ThenInclude(x => x.Media)
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();

		CommentReadDto? result = _mapper.Map<CommentReadDto>(comment);

		return new GenericResponse<CommentReadDto?>(result);
	}

	public async Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto entity) {
		CommentEntity? comment = _mapper.Map<CommentEntity>(entity);
		comment.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!;

		await _context.AddAsync(comment);
		await _context.SaveChangesAsync();

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