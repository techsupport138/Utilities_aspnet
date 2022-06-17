namespace Utilities_aspnet.Content;

public interface IContentRepository<T> where T : BaseContentEntity {
    Task<GenericResponse<ContentReadDto>> Create(ContentCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ContentReadDto>>> Read();
    Task<GenericResponse<ContentReadDto>> ReadById(Guid id);
    Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository<T> : IContentRepository<T> where T : BaseContentEntity {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContentRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<ContentReadDto>> Create(ContentCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);
        entity.UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        EntityEntry<T> i = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ContentReadDto>>> Read() {
        IEnumerable<T> i = await _context.Set<T>().Include(x => x.Media).AsNoTracking().ToListAsync();
        return new GenericResponse<IEnumerable<ContentReadDto>>(_mapper.Map<IEnumerable<ContentReadDto>>(i));
    }

    public async Task<GenericResponse<ContentReadDto>> ReadById(Guid id) {
        T? i = await _context.Set<T>().AsNoTracking().Include(x => x.Media).FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i));
    }

    public async Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T? entity = await _context.Set<T>()
            .AsNoTracking()
            .Where(x => x.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            return new GenericResponse<ContentReadDto>(new ContentReadDto());

        entity.UseCase = dto.UseCase;
        entity.ApprovalStatus = dto.ApprovalStatus;
        entity.Title ??= dto.Title;
        entity.SubTitle ??= dto.SubTitle;
        entity.Description ??= dto.Description;
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(entity));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        GenericResponse<ContentReadDto> i = await ReadById(id);
        _context.Set<T>().Remove(_mapper.Map<T>(i.Result));
        await _context.SaveChangesAsync();
        return new GenericResponse();
    }
}