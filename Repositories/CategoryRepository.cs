namespace Utilities_aspnet.Repositories;

public interface ICategoryRepository {
	public Task<GenericResponse<CategoryReadDto>> Create(CategoryCreateUpdateDto dto);
	public Task<GenericResponse<IEnumerable<CategoryReadDto>>> ReadChildren();
	public Task<GenericResponse<IEnumerable<CategoryReadDto>>> ReadParent();
	public Task<GenericResponse<CategoryReadDto>> Update(CategoryCreateUpdateDto dto);
	public Task<GenericResponse<CategoryReadDto>> ReadById(Guid id);
	public Task<GenericResponse> Delete(Guid id);
}

public class CategoryRepository : ICategoryRepository {
	private readonly DbContext _dbContext;
	private readonly IMapper _mapper;

	public CategoryRepository(DbContext context, IMapper mapper) {
		_dbContext = context;
		_mapper = mapper;
	}

	public async Task<GenericResponse<CategoryReadDto>> Create(CategoryCreateUpdateDto dto) {
		CategoryEntity entity = _mapper.Map<CategoryEntity>(dto);

		EntityEntry<CategoryEntity> i = await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse<CategoryReadDto>(_mapper.Map<CategoryReadDto>(i.Entity));
	}

	public async Task<GenericResponse<IEnumerable<CategoryReadDto>>> ReadChildren() {
		IEnumerable<CategoryEntity> i = await _dbContext.Set<CategoryEntity>()
			.Include(i => i.Media)
			.Include(i => i.Children).ThenInclude(i => i.Media).Where(x => x.ParentId == null)
			.AsNoTracking()
			.ToListAsync();
		return new GenericResponse<IEnumerable<CategoryReadDto>>(_mapper.Map<IEnumerable<CategoryReadDto>>(i));
	}

	public async Task<GenericResponse<IEnumerable<CategoryReadDto>>> ReadParent() {
		IEnumerable<CategoryEntity> i = await _dbContext.Set<CategoryEntity>()
			.Include(i => i.Media)
			.Include(i => i.Parent).ThenInclude(i => i.Media)
			.AsNoTracking()
			.ToListAsync();
		return new GenericResponse<IEnumerable<CategoryReadDto>>(_mapper.Map<IEnumerable<CategoryReadDto>>(i));
	}

	public async Task<GenericResponse<CategoryReadDto>> ReadById(Guid id) {
		CategoryEntity? i = await _dbContext.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == id);
		return new GenericResponse<CategoryReadDto>(_mapper.Map<CategoryReadDto>(i));
	}

	public async Task<GenericResponse> Delete(Guid id) {
		CategoryEntity? i = await _dbContext.Set<CategoryEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		_dbContext.Remove(i);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse<CategoryReadDto>> Update(CategoryCreateUpdateDto dto) {
		CategoryEntity? entity = await _dbContext.Set<CategoryEntity>().FirstOrDefaultAsync(item => item.Id == dto.Id);

		if (entity == null) return new GenericResponse<CategoryReadDto>(null, UtilitiesStatusCodes.NotFound);
		entity.Title = dto.Title ?? entity.Title;
		entity.TitleTr1 = dto.TitleTr1 ?? entity.TitleTr1;
		entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
		entity.Color = dto.Color ?? entity.Color;
		entity.Link = dto.Link ?? entity.Link;
		entity.UpdatedAt = DateTime.Now;
		entity.UseCase = dto.UseCase ?? entity.UseCase;
		entity.Type = dto.Type ?? entity.Type;
		entity.ParentId = dto.ParentId ?? entity.ParentId;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse<CategoryReadDto>(_mapper.Map<CategoryReadDto>(entity));
	}
}