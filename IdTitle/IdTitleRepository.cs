namespace Utilities_aspnet.IdTitle;

public interface IIdTitleRepository<T> where T : BaseIdTitleEntity {
    public Task<GenericResponse<IdTitleReadDto>> Create(IdTitleCreateUpdateDto dto);
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> Read();
    public Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id);
    public Task<GenericResponse<IdTitleReadDto>> Update(IdTitleCreateUpdateDto dto);
    public Task<GenericResponse> Delete(Guid id);
}

public class IdTitleRepository<T> : IIdTitleRepository<T> where T : BaseIdTitleEntity {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public IdTitleRepository(DbContext context, IMapper mapper) {
        _dbContext = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<IdTitleReadDto>> Create(IdTitleCreateUpdateDto dto) {
        T entity = _mapper.Map<T>(dto);

        EntityEntry<T>? i = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i.Entity));
    }


    public async Task<GenericResponse<IEnumerable<IdTitleReadDto>>> Read() {
        IEnumerable<T> i = await _dbContext.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }

    public async Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i));
    }

    public Task<GenericResponse> Delete(Guid id) {
        throw new NotImplementedException();
    }

    public Task<GenericResponse<IdTitleReadDto>> Update(IdTitleCreateUpdateDto dto) {
        throw new NotImplementedException();
    }
}