namespace Utilities_aspnet.IdTitle;

public interface ICategoryRepository {
    public Task<GenericResponse<IdTitleReadDto>> Create(IdTitleCreateUpdateDto dto);
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> Read();
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadV2();
    public Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id);
    public Task<GenericResponse<IdTitleReadDto>> ReadByIdV2(Guid id);
    public Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadByUseCase(IdTitleUseCase useCase);
    public Task<GenericResponse<IdTitleReadDto>> Update(IdTitleCreateUpdateDto dto);
    public Task<GenericResponse> Delete(Guid id);
}

public class CategoryRepository : ICategoryRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public CategoryRepository(DbContext context, IMapper mapper) {
        _dbContext = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<IdTitleReadDto>> Create(IdTitleCreateUpdateDto dto) {
        CategoryEntity entity = _mapper.Map<CategoryEntity>(dto);

        EntityEntry<CategoryEntity>? i = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i.Entity));
    }


    public async Task<GenericResponse<IEnumerable<IdTitleReadDto>>> Read() {
        IEnumerable<CategoryEntity> i = await _dbContext.Set<CategoryEntity>()
            .Include(i => i.Media)
            .Include(i => i.Parent).ThenInclude(i => i.Media)
            .AsNoTracking()
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }
    
    public async Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadV2() {
        IEnumerable<CategoryEntity> i = await _dbContext.Set<CategoryEntity>()
            .Include(i => i.Media)
            .Include(i => i.Children).ThenInclude(i=>i.Media).Where(x=>x.ParentId == null)
            .AsNoTracking()
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }

    public async Task<GenericResponse<IdTitleReadDto>> ReadById(Guid id) {
        CategoryEntity? i = await _dbContext.Set<CategoryEntity>()
            .Include(i => i.Media)
            .Include(i => i.Parent).ThenInclude(i => i.Media)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i));
    }
    
    public async Task<GenericResponse<IdTitleReadDto>> ReadByIdV2(Guid id) {
        CategoryEntity? i = await _dbContext.Set<CategoryEntity>()
            .Include(i => i.Media)
            .Include(i => i.Children).ThenInclude(i => i.Media)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(i));
    }

    public async Task<GenericResponse<IEnumerable<IdTitleReadDto>>> ReadByUseCase(IdTitleUseCase useCase) {
        IEnumerable<CategoryEntity> i = await _dbContext.Set<CategoryEntity>()
            .Include(i => i.Media)
            .Where(i => i.UseCase == useCase).AsNoTracking()
            .ToListAsync();
        return new GenericResponse<IEnumerable<IdTitleReadDto>>(_mapper.Map<IEnumerable<IdTitleReadDto>>(i));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        CategoryEntity? i = await _dbContext.Set<CategoryEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        _dbContext.Remove(i);
        await _dbContext.SaveChangesAsync();

        return new GenericResponse();
    }

    public async Task<GenericResponse<IdTitleReadDto>> Update(IdTitleCreateUpdateDto dto) {
        CategoryEntity? entity = await _dbContext.Set<CategoryEntity>().FirstOrDefaultAsync(item => item.Id == dto.Id);

        if (entity == null) return new GenericResponse<IdTitleReadDto>(null, UtilitiesStatusCodes.NotFound);
        entity.Title = dto.Title ?? entity.Title;
        entity.TitleTr1 = dto.TitleTr1 ?? entity.TitleTr1;
        entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
        entity.Color = dto.Color ?? entity.Color;
        entity.Link = dto.Link ?? entity.Link;
        entity.UpdatedAt = DateTime.Now;
        entity.UseCase = dto.UseCase ?? entity.UseCase;
        entity.ParentId = dto.ParentId ?? entity.ParentId;
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<IdTitleReadDto>(_mapper.Map<IdTitleReadDto>(entity));
    }
}