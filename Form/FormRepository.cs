namespace Utilities_aspnet.FormBuilder;

public interface IFormRepository<T> { }

public class FormRepository<T> : IFormRepository<T> {
    private readonly DbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public FormRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<FormEntity>> UpdateFormBuilder(KVVMs model) {
        foreach (KVVM item in model.KVVM)
            try {
                FormEntity? up = await _dbContext.Set<FormEntity>().FirstOrDefaultAsync(x =>
                    (x.ProductId == model.ProductId || x.ProjectId == model.ProjectId || x.AdId == model.AdId ||
                     x.CompanyId == model.CompanyId || x.UserId == model.UserId ||
                     x.EventId == model.EventId || x.MagazineId == model.MagazineId || x.ServiceId == model.ServiceId ||
                     x.TenderId == model.TenderId || x.TutorialId == model.TutorialId
                    ) && x.FormFieldId == item.Key);
                if (up != null) {
                    up.Value = item.Value;
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
                        FormFieldId = item.Key,
                        Value = item.Value
                    });
                }

                await _dbContext.SaveChangesAsync();
            }
            catch {
                // ignored
            }

        return new GenericResponse<FormEntity>(new FormEntity());
    }
}