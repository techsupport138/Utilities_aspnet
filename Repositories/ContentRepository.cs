namespace Utilities_aspnet.Repositories;

public interface IContentRepository {
	Task<GenericResponse<ContentEntity>> Create(ContentEntity dto);
	GenericResponse<IQueryable<ContentEntity>> Read();
	Task<GenericResponse<ContentEntity>> Update(ContentEntity dto);
	Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository : IContentRepository {
	private readonly DbContext _context;

	public ContentRepository(DbContext context) => _context = context;

	public async Task<GenericResponse<ContentEntity>> Create(ContentEntity dto) {
		EntityEntry<ContentEntity> i = await _context.Set<ContentEntity>().AddAsync(dto);
		await _context.SaveChangesAsync();
		return new GenericResponse<ContentEntity>(i.Entity);
	}

	public GenericResponse<IQueryable<ContentEntity>> Read() => new(_context.Set<ContentEntity>().Include(x => x.Media).AsNoTracking());

	public async Task<GenericResponse<ContentEntity>> Update(ContentEntity dto) {
		ContentEntity? entity = await _context.Set<ContentEntity>().AsNoTracking().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();
		if (entity == null) return new GenericResponse<ContentEntity>(new ContentEntity());
		entity.UseCase = dto.UseCase;
		entity.Title = dto.Title;
		entity.SubTitle = dto.SubTitle;
		entity.Description = dto.Description;
		_context.Update(entity);
		await _context.SaveChangesAsync();
		return new GenericResponse<ContentEntity>(entity);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		ContentEntity? i = await _context.Set<ContentEntity>().FirstOrDefaultAsync(i => i.Id == id);
		if (i == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		_context.Set<ContentEntity>().Remove(i);
		await _context.SaveChangesAsync();
		return new GenericResponse();
	}
}