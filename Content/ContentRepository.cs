namespace Utilities_aspnet.Content;

public interface IContentRepository {
    Task<GenericResponse<ContentReadDto>> Create(ContentCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ContentReadDto>>> Read();
    Task<GenericResponse<ContentReadDto>> ReadById(Guid id);
    Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository : IContentRepository {
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
        ContentEntity entity = _mapper.Map<ContentEntity>(dto);
        entity.UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        EntityEntry<ContentEntity> i = await _context.Set<ContentEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ContentReadDto>>> Read() {
        IEnumerable<ContentEntity> i = await _context.Set<ContentEntity>().Include(x => x.Media).AsNoTracking().ToListAsync();
        return new GenericResponse<IEnumerable<ContentReadDto>>(_mapper.Map<IEnumerable<ContentReadDto>>(i));
    }

    public async Task<GenericResponse<ContentReadDto>> ReadById(Guid id) {
        ContentEntity? i = await _context.Set<ContentEntity>().AsNoTracking().Include(x => x.Media)
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i));
    }

    public async Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        ContentEntity? entity = await _context.Set<ContentEntity>()
            .AsNoTracking()
            .Where(x => x.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            return new GenericResponse<ContentReadDto>(new ContentReadDto());

        entity.UseCase = dto.UseCase;
        entity.Title = dto.Title;
        entity.SubTitle = dto.SubTitle;
        entity.Description = dto.Description;
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(entity));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        GenericResponse<ContentReadDto> i = await ReadById(id);
        _context.Set<ContentEntity>().Remove(_mapper.Map<ContentEntity>(i.Result));
        await _context.SaveChangesAsync();
        return new GenericResponse();
    }
}