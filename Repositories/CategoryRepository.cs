namespace Utilities_aspnet.Repositories;

public interface ICategoryRepository {
	public Task<GenericResponse<CategoryEntity>> Create(CategoryEntity dto);
	public GenericResponse<IQueryable<CategoryEntity>> Read();
	public Task<GenericResponse<CategoryEntity?>> Update(CategoryEntity dto);
	public Task<GenericResponse> Delete(Guid id);
}

public class CategoryRepository : ICategoryRepository {
	private readonly DbContext _dbContext;

	public CategoryRepository(DbContext context) => _dbContext = context;

	public async Task<GenericResponse<CategoryEntity>> Create(CategoryEntity entity) {
		EntityEntry<CategoryEntity> i = await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse<CategoryEntity>(i.Entity);
	}

	public GenericResponse<IQueryable<CategoryEntity>> Read() {
		IQueryable<CategoryEntity> i = _dbContext.Set<CategoryEntity>()
			.Include(i => i.Media)
			.Include(i => i.Children)!.ThenInclude(i => i.Media).Where(x => x.ParentId == null)
			.AsNoTracking();
		return new GenericResponse<IQueryable<CategoryEntity>>(i);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		CategoryEntity? i = await _dbContext.Set<CategoryEntity>()
			.Include(x => x.Products)
			.Include(x => x.Users)
			.Include(x => x.Media)
			.Include(x => x.Children)
			.FirstOrDefaultAsync(i => i.Id == id);
		if (i != null) {
			_dbContext.Remove(i);
			await _dbContext.SaveChangesAsync();
		}
		else
			return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");

		return new GenericResponse();
	}

	public async Task<GenericResponse<CategoryEntity?>> Update(CategoryEntity dto) {
		CategoryEntity? entity = await _dbContext.Set<CategoryEntity>().FindAsync(dto.Id);

		if (entity == null) return new GenericResponse<CategoryEntity?>(null, UtilitiesStatusCodes.NotFound);
		entity.Title = dto.Title ?? entity.Title;
		entity.TitleTr1 = dto.TitleTr1 ?? entity.TitleTr1;
		entity.TitleTr2 = dto.TitleTr2 ?? entity.TitleTr2;
		entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
		entity.Color = dto.Color ?? entity.Color;
		entity.Link = dto.Link ?? entity.Link;
		entity.UpdatedAt = DateTime.Now;
		entity.UseCase = dto.UseCase ?? entity.UseCase;
		entity.Type = dto.Type ?? entity.Type;
		entity.ParentId = dto.ParentId ?? entity.ParentId;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse<CategoryEntity?>(entity);
	}
}