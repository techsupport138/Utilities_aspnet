namespace Utilities_aspnet.Form;

public interface IFormRepository {
    Task<GenericResponse<IEnumerable<FormFieldDto>>> ReadFormFields(Guid categoryId);
    Task<GenericResponse<IEnumerable<FormFieldDto>?>> CreateFormFields(FormFieldDto dto);
    Task<GenericResponse<IEnumerable<FormFieldDto>>> UpdateFormBuilder(FormCreateDto model);
    Task<GenericResponse<IEnumerable<FormFieldDto>?>> UpdateFormFields(FormFieldDto dto);
    Task<GenericResponse> DeleteFormField(Guid id);
    Task<GenericResponse> DeleteFormBuilder(Guid id);
}

public class FormRepository : IFormRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public FormRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GenericResponse<IEnumerable<FormFieldDto>>> UpdateFormBuilder(FormCreateDto model) {
        foreach (CategoryCreateUpdateDto item in model.Form)
            try {
                FormEntity? up = await _dbContext.Set<FormEntity>().FirstOrDefaultAsync(x =>
                    (x.ProductId == model.ProductId && model.ProductId != null ||
                     x.UserId == model.UserId && model.UserId != null
                    ) && x.FormFieldId == item.Id);
                if (up != null) {
                    up.Title = item.Title;
                    await _dbContext.SaveChangesAsync();
                }
                else {
                    _dbContext.Set<FormEntity>().Add(new FormEntity {
                        ProductId = model.ProductId,
                        UserId = model.UserId,
                        FormFieldId = item.Id,
                        Title = item.Title
                    });
                }

                await _dbContext.SaveChangesAsync();
            }
            catch {
                // ignored
            }

        IEnumerable<FormEntity> entity = await _dbContext.Set<FormEntity>().Where(x =>
            x.ProductId == model.ProductId && model.ProductId != null ||
            x.UserId == model.UserId && model.UserId != null).ToListAsync();

        return new GenericResponse<IEnumerable<FormFieldDto>>(_mapper.Map<IEnumerable<FormFieldDto>>(entity));
    }

    public async Task<GenericResponse<IEnumerable<FormFieldDto>?>> CreateFormFields(FormFieldDto dto) {
        Guid? categoryId = dto.CategoryId;
        try {
            FormFieldEntity entity = _mapper.Map<FormFieldEntity>(dto);
            await _dbContext.Set<FormFieldEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch {
            // ignored
        }

        return categoryId != null
            ? new GenericResponse<IEnumerable<FormFieldDto>?>(ReadFormFields((Guid) categoryId).Result.Result)
            : new GenericResponse<IEnumerable<FormFieldDto>?>(null);
    }

    public async Task<GenericResponse<IEnumerable<FormFieldDto>?>> UpdateFormFields(FormFieldDto dto) {
        Guid? categoryId = dto.CategoryId;
        FormFieldEntity? entity = await _dbContext.Set<FormFieldEntity>().FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (entity == null) return new GenericResponse<IEnumerable<FormFieldDto>?>(null, UtilitiesStatusCodes.NotFound);

        try {
            entity.Label = dto.Label;
            entity.OptionList = dto.OptionList;
            entity.CategoryId = categoryId;
            entity.IsRequired = dto.IsRequired;
            entity.Type = dto.Type;
            entity.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }
        catch {
            // ignored
        }

        return categoryId != null
            ? new GenericResponse<IEnumerable<FormFieldDto>?>(ReadFormFields((Guid) categoryId).Result.Result)
            : new GenericResponse<IEnumerable<FormFieldDto>?>(null);
    }

    public async Task<GenericResponse<IEnumerable<FormFieldDto>>> ReadFormFields(Guid categoryId) {
        IEnumerable<FormFieldEntity> model =
            await _dbContext.Set<FormFieldEntity>().Where(x => x.CategoryId == categoryId).ToListAsync();
        return new GenericResponse<IEnumerable<FormFieldDto>>(_mapper.Map<IEnumerable<FormFieldDto>>(model));
    }

    public async Task<GenericResponse> DeleteFormField(Guid id) {
        FormFieldEntity? entity = await _dbContext.Set<FormFieldEntity>().Include(x => x.Forms).AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse();
    }

    public async Task<GenericResponse> DeleteFormBuilder(Guid id) {
        FormEntity? entity = await _dbContext.Set<FormEntity>().Include(x => x.Product)
            .Include(x => x.User).AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse();
    }
}