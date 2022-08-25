namespace Utilities_aspnet.Repositories;

public interface IContentRepository {
	Task<GenericResponse<ContentEntity>> Create(ContentEntity dto);
	GenericResponse<IQueryable<ContentEntity>> Read();
	Task<GenericResponse<ContentEntity>> ReadById(Guid id);
	Task<GenericResponse<ContentEntity>> Update(ContentEntity dto);
	Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository : IContentRepository {
	private readonly DbContext _context;

	public ContentRepository(DbContext context) => _context = context;

	public async Task<GenericResponse<ContentEntity>> Create(ContentEntity dto) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		EntityEntry<ContentEntity> i = await _context.Set<ContentEntity>().AddAsync(dto);
		await _context.SaveChangesAsync();
		return new GenericResponse<ContentEntity>(i.Entity);
	}

	public GenericResponse<IQueryable<ContentEntity>> Read() {
		IQueryable<ContentEntity> i = _context.Set<ContentEntity>()
			.Select(x => new ContentEntity {
				Id = x.Id,
				Description = x.Description,
				Title = x.Title,
				SubTitle = x.SubTitle,
				UseCase = x.UseCase,
				Media = x.Media!.Select(y => new MediaEntity {
					Id = y.Id,
					Link = y.Link,
					Title = y.Title,
					Size = y.Size,
					FileName = y.FileName,
					Visibility = y.Visibility,
					UseCase = y.UseCase,
				})
			}).AsNoTracking();
		return new GenericResponse<IQueryable<ContentEntity>>(i);
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