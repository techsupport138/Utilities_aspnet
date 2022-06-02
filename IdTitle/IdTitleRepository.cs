namespace Utilities_aspnet.IdTitle;

public interface IIdTitleRepository<T> where T : BaseIdTitleEntity {
    public Task<GenericResponse<IdTitleReadDto>> Create(IdTitleCreateUpdateDto dto);
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> Read();
    public Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id);
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadByUseCase(IdTitleUseCase useCase);
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
        IEnumerable<T> i = await _dbContext.Set<T>()
            .Include(i => i.Media).AsNoTracking()
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }

    public async Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>()
            .Include(i => i.Media).AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i));
    }

    public async Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadByUseCase(IdTitleUseCase useCase) {
        IEnumerable<T> i = await _dbContext.Set<T>()
            .Include(i => i.Media)
            .Where(i => i.UseCase == useCase).AsNoTracking()
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        _dbContext.Remove(i);
        await _dbContext.SaveChangesAsync();


        return new GenericResponse();
    }

    public async Task<GenericResponse<IdTitleReadDto>> Update(IdTitleCreateUpdateDto dto) {
        T? entity = await _dbContext.Set<T>().FirstOrDefaultAsync(item => item.Id == dto.Id);

        if (entity == null) return new GenericResponse<IdTitleReadDto>(null, UtilitiesStatusCodes.NotFound);
        entity.Title = dto.Title ?? entity.Title;
        entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
        entity.Color = dto.Color ?? entity.Color;
        entity.Link = dto.Link ?? entity.Link;
        entity.UpdatedAt = DateTime.Now;
        entity.UseCase = dto.UseCase ?? entity.UseCase;
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(entity));
    }
}