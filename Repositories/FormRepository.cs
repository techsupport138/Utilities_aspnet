namespace Utilities_aspnet.Repositories;

public interface IFormRepository {
	GenericResponse<IQueryable<FormFieldEntity>> ReadFormFields(Guid categoryId);
	Task<GenericResponse<IQueryable<FormFieldEntity>?>> CreateFormFields(FormFieldEntity dto);
	Task<GenericResponse<IQueryable<FormEntity>>> UpdateForm(FormCreateDto model);
	Task<GenericResponse<IQueryable<FormFieldEntity>?>> UpdateFormFields(FormFieldEntity dto);
	Task<GenericResponse> DeleteFormField(Guid id);
	Task<GenericResponse> DeleteFormBuilder(Guid id);
}

public class FormRepository : IFormRepository {
	private readonly DbContext _dbContext;

	public FormRepository(DbContext dbContext) => _dbContext = dbContext;

	public async Task<GenericResponse<IQueryable<FormEntity>>> UpdateForm(FormCreateDto model) {
		foreach (FormTitleDto item in model.Form!)
			try {
				FormEntity? up = await _dbContext.Set<FormEntity>()
					.FirstOrDefaultAsync(x => (x.ProductId == model.ProductId &&
						                     model.ProductId != null || x.UserId == model.UserId &&
						                     model.UserId != null || x.OrderDetailId == model.OrderDetailId &&
						                     model.OrderDetailId != null) && x.FormFieldId == item.Id);
				if (up != null) {
					up.Title = item.Title ?? "";
					await _dbContext.SaveChangesAsync();
				}
				else {
					_dbContext.Set<FormEntity>().Add(new FormEntity {
						ProductId = model.ProductId,
						UserId = model.UserId,
						OrderDetailId = model.OrderDetailId,
						FormFieldId = item.Id,
						Title = item.Title ?? "",
					});
				}

				await _dbContext.SaveChangesAsync();
			}
			catch { }

		IQueryable<FormEntity> entity = _dbContext.Set<FormEntity>()
			.Where(x => x.ProductId == model.ProductId && model.ProductId != null
			            || x.UserId == model.UserId && model.UserId != null
			            || x.OrderDetailId == model.OrderDetailId && model.OrderDetailId != null)
			.AsNoTracking();
		return new GenericResponse<IQueryable<FormEntity>>(entity);
	}

	public async Task<GenericResponse<IQueryable<FormFieldEntity>?>> CreateFormFields(FormFieldEntity dto) {
		Guid? categoryId = dto.CategoryId;
		try {
			await _dbContext.Set<FormFieldEntity>().AddAsync(dto);
			await _dbContext.SaveChangesAsync();
		}
		catch { }
		return categoryId != null
			? new GenericResponse<IQueryable<FormFieldEntity>?>(ReadFormFields((Guid) categoryId).Result)
			: new GenericResponse<IQueryable<FormFieldEntity>?>(null);
	}

	public async Task<GenericResponse<IQueryable<FormFieldEntity>?>> UpdateFormFields(FormFieldEntity dto) {
		Guid? categoryId = dto.CategoryId;
		FormFieldEntity? entity = await _dbContext.Set<FormFieldEntity>().FirstOrDefaultAsync(x => x.Id == dto.Id);
		if (entity == null) return new GenericResponse<IQueryable<FormFieldEntity>?>(null, UtilitiesStatusCodes.NotFound);
		try {
			entity.Label = dto.Label;
			entity.OptionList = dto.OptionList;
			entity.CategoryId = categoryId;
			entity.UseCase = dto.UseCase;
			entity.UseCase2 = dto.UseCase2;
			entity.IsRequired = dto.IsRequired;
			entity.Type = dto.Type;
			entity.UpdatedAt = DateTime.Now;
			await _dbContext.SaveChangesAsync();
		}
		catch { }
		return categoryId != null
			? new GenericResponse<IQueryable<FormFieldEntity>?>(ReadFormFields((Guid) categoryId).Result)
			: new GenericResponse<IQueryable<FormFieldEntity>?>(null);
	}

	public GenericResponse<IQueryable<FormFieldEntity>> ReadFormFields(Guid categoryId) => new(_dbContext.Set<FormFieldEntity>()
		                                                                                           .Where(x => x.DeletedAt == null)
		                                                                                           .Where(x => x.CategoryId == categoryId)
		                                                                                           .Where(x => x.ParentId == null)
		                                                                                           .Include(i => i.Children.Where(x => x.DeletedAt == null))
		                                                                                           .AsNoTracking());

	public async Task<GenericResponse> DeleteFormField(Guid id) {
		FormFieldEntity? entity = await _dbContext.Set<FormFieldEntity>()
			.Include(x => x.Forms)
			.FirstOrDefaultAsync(i => i.Id == id);
		if (entity == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		entity.DeletedAt = DateTime.Now;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse> DeleteFormBuilder(Guid id) {
		FormEntity? entity = await _dbContext.Set<FormEntity>()
			.Include(x => x.Product)
			.Include(x => x.User)
			.FirstOrDefaultAsync(i => i.Id == id);
		if (entity == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		entity.DeletedAt = DateTime.Now;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}
}