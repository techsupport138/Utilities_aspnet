﻿namespace Utilities_aspnet.Repositories;

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
		ContentEntity? e = await _context.Set<ContentEntity>().AsNoTracking().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();
		if (e == null) return new GenericResponse<ContentEntity>(new ContentEntity());
		e.UseCase = dto.UseCase;
		e.Title = dto.Title;
		e.SubTitle = dto.SubTitle;
		e.Description = dto.Description;
		_context.Update(e);
		await _context.SaveChangesAsync();
		return new GenericResponse<ContentEntity>(e);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		ContentEntity? e = await _context.Set<ContentEntity>().FirstOrDefaultAsync(i => i.Id == id);
		if (e == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		_context.Set<ContentEntity>().Remove(e);
		await _context.SaveChangesAsync();
		return new GenericResponse();
	}
}