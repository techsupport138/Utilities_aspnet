namespace Utilities_aspnet.Repositories;

public interface IContentRepository {
	Task<GenericResponse<ContentEntity>> Create(ContentEntity dto);
	Task<GenericResponse<IEnumerable<ContentEntity>>> Read();
	Task<GenericResponse<ContentEntity>> ReadById(Guid id);
	Task<GenericResponse<ContentEntity>> Update(ContentEntity dto);
	Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository : IContentRepository {
	private readonly DbContext _context;

	public ContentRepository(DbContext context) {
		_context = context;
	}

	public async Task<GenericResponse<ContentEntity>> Create(ContentEntity dto) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		EntityEntry<ContentEntity> i = await _context.Set<ContentEntity>().AddAsync(dto);
		await _context.SaveChangesAsync();
		return new GenericResponse<ContentEntity>(i.Entity);
	}

	public async Task<GenericResponse<IEnumerable<ContentEntity>>> Read() {
		IEnumerable<ContentEntity> i = await _context.Set<ContentEntity>().Include(x => x.Media).AsNoTracking().ToListAsync();
		return new GenericResponse<IEnumerable<ContentEntity>>(i);
	}

	public async Task<GenericResponse<ContentEntity>> ReadById(Guid id) {
		ContentEntity? i = await _context.Set<ContentEntity>().AsNoTracking().Include(x => x.Media).FirstOrDefaultAsync(i => i.Id == id);
		return new GenericResponse<ContentEntity>(i);
	}

	public async Task<GenericResponse<ContentEntity>> Update(ContentEntity dto) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		ContentEntity? entity = await _context.Set<ContentEntity>().AsNoTracking().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

		if (entity == null)
			return new GenericResponse<ContentEntity>(new ContentEntity());

		entity.UseCase = dto.UseCase;
		entity.Title = dto.Title;
		entity.SubTitle = dto.SubTitle;
		entity.Description = dto.Description;
		_context.Update(entity);
		await _context.SaveChangesAsync();

		return new GenericResponse<ContentEntity>(entity);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		GenericResponse<ContentEntity> i = await ReadById(id);
		_context.Set<ContentEntity>().Remove(i.Result);
		await _context.SaveChangesAsync();
		return new GenericResponse();
	}
}