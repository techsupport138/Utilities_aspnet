namespace Utilities_aspnet.Form;

public interface IFormRepository {
    Task<GenericResponse<List<FormFieldDto>>> ReadFormFields(Guid categoryId);
    Task<GenericResponse<List<FormFieldDto>?>> CreateFormFields(FormFieldDto dto);
    Task<GenericResponse<List<FormFieldDto>>> UpdateFormBuilder(FormCreateDto model);
}

public class FormRepository : IFormRepository {
    private readonly DbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public FormRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<List<FormFieldDto>>> UpdateFormBuilder(FormCreateDto model) {
        foreach (IdTitleReadDto item in model.KVVM)
            try {
                FormEntity? up = await _dbContext.Set<FormEntity>().FirstOrDefaultAsync(x =>
                    (x.ProductId == model.ProductId || x.ProjectId == model.ProjectId || x.AdId == model.AdId ||
                     x.CompanyId == model.CompanyId || x.UserId == model.UserId ||
                     x.EventId == model.EventId || x.MagazineId == model.MagazineId || x.ServiceId == model.ServiceId ||
                     x.TenderId == model.TenderId || x.TutorialId == model.TutorialId
                    ) && x.FormFieldId == item.Id);
                if (up != null) {
                    up.Title = item.Title;
                    await _dbContext.SaveChangesAsync();
                }
                else {
                    _dbContext.Set<FormEntity>().Add(new FormEntity {
                        ProductId = model.ProductId,
                        AdId = model.AdId,
                        CompanyId = model.CompanyId,
                        UserId = model.UserId,
                        EventId = model.EventId,
                        ProjectId = model.ProjectId,
                        MagazineId = model.MagazineId,
                        TutorialId = model.TutorialId,
                        TenderId = model.TenderId,
                        ServiceId = model.ServiceId,
                        FormFieldId = item.Id,
                        Title = item.Title
                    });
                }

                await _dbContext.SaveChangesAsync();
            }
            catch {
                // ignored
            }

        List<FormEntity> entity = await _dbContext.Set<FormEntity>().Where(x =>
            x.ProductId == model.ProductId || x.ProjectId == model.ProjectId || x.AdId == model.AdId ||
            x.CompanyId == model.CompanyId || x.UserId == model.UserId ||
            x.EventId == model.EventId || x.MagazineId == model.MagazineId || x.ServiceId == model.ServiceId ||
            x.TenderId == model.TenderId || x.TutorialId == model.TutorialId).ToListAsync();

        return new GenericResponse<List<FormFieldDto>>(_mapper.Map<List<FormFieldDto>>(entity));
    }

    public async Task<GenericResponse<List<FormFieldDto>?>> CreateFormFields(FormFieldDto dto) {
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
            ? new GenericResponse<List<FormFieldDto>?>(ReadFormFields((Guid) categoryId).Result.Result)
            : new GenericResponse<List<FormFieldDto>?>(null);
    }

    public async Task<GenericResponse<List<FormFieldDto>>> ReadFormFields(Guid categoryId) {
        List<FormFieldEntity> model =
            await _dbContext.Set<FormFieldEntity>().Where(x => x.CategoryId == categoryId).ToListAsync();
        return new GenericResponse<List<FormFieldDto>>(_mapper.Map<List<FormFieldDto>>(model));
    }
}