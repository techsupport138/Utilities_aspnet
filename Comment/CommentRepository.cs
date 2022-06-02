namespace Utilities_aspnet.Comment;

public interface ICommentRepository {
    Task<GenericResponse<CommentReadDto?>> Create(CommentCreateUpdateDto entity);
    Task<GenericResponse<CommentReadDto?>> Read(Guid id);
    Task<GenericResponse<CommentReadDto?>> Update(Guid id, CommentCreateUpdateDto entity);
    Task<GenericResponse> Delete(Guid id);
}

public class CommentRepository : ICommentRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<CommentReadDto?>> Read(Guid id) {
        CommentEntity? comment = await _context.Set<CommentEntity>()
            .AsNoTracking()
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

        if (entity.CompanyId.HasValue)
            comment.CompanyId = entity.CompanyId;

        if (entity.AdId.HasValue)
            comment.AdId = entity.AdId;

        if (entity.DailyPriceId.HasValue)
            comment.DailyPriceId = entity.DailyPriceId;

        if (entity.ProjectId.HasValue)
            comment.ProjectId = entity.ProjectId;

        if (entity.TutorialId.HasValue)
            comment.TutorialId = entity.TutorialId;

        if (entity.EventId.HasValue)
            comment.EventId = entity.EventId;

        if (entity.TenderId.HasValue)
            comment.TenderId = entity.TenderId;

        if (entity.ServiceId.HasValue)
            comment.ServiceId = entity.ServiceId;

        if (entity.MagazineId.HasValue)
            comment.MagazineId = entity.MagazineId;

        _context.Set<CommentEntity>().Update(comment);
        await _context.SaveChangesAsync();

        return await Read(comment.Id);
        ;
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