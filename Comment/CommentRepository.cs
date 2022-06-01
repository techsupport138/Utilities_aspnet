namespace Utilities_aspnet.Comment;

public interface ICommentRepository {
    Task<GenericResponse> Create(CommentCreateUpdateDto entity);
    Task<GenericResponse<CommentEntity?>> Read(Guid id);
    Task<GenericResponse> Update(Guid id, CommentCreateUpdateDto entity);
    Task<GenericResponse> Delete(Guid id);
}

public class CommentRepository : ICommentRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public CommentRepository(DbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<CommentEntity?>> Read(Guid id) {
        CommentEntity? comment = await _context.Set<CommentEntity>()
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Comment)
            .Include(x => x.Product)
            .Include(x => x.Project)
            .Include(x => x.Tutorial)
            .Include(x => x.Event)
            .Include(x => x.Ad)
            .Include(x => x.Tender)
            .Include(x => x.Company)
            .Include(x => x.Service)
            .Include(x => x.MagazineId)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        return new GenericResponse<CommentEntity?>(comment);
    }

    public async Task<GenericResponse> Create(CommentCreateUpdateDto entity) {
        CommentEntity? comment = _mapper.Map<CommentEntity>(entity);

        await _context.AddAsync(comment);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }

    public async Task<GenericResponse> Update(Guid id, CommentCreateUpdateDto entity) {
        CommentEntity? comment = await _context.Set<CommentEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (comment == null)
            return new GenericResponse(UtilitiesStatusCodes.NotFound, "Comment notfound");

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

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
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