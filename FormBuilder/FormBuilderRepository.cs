using Utilities_aspnet.FormBuilder;

namespace Utilities_aspnet.Product;

public interface IFormBuilderRepository<T>
{

}

public class FormBuilderRepository<T> : IFormBuilderRepository<T>
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FormBuilderRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }


	public async Task<GenericResponse<FormBuilderEntity>> UpdateFormBuilder(KVVMs model)
	{
		foreach (var item in model.KVVM)
		{
			try
			{
				var up = await _dbContext.Set<FormBuilderEntity>().FirstOrDefaultAsync(x => 
				(x.ProductId == model.ProductId || x.ProjectId == model.ProjectId || x.AdId == model.AdId || x.CompanyId == model.CompanyId || x.UserId == model.UserId ||
				x.EventId == model.EventId || x.MagazineId == model.MagazineId || x.ServiceId == model.ServiceId || x.TenderId == model.TenderId || x.TutorialId == model.TutorialId
				)&& x.FormBuilderFieldId == item.Key);
				if (up != null)
				{
					up.Value = item.Value;
					await _dbContext.SaveChangesAsync();
				}
				else
				{
					_dbContext.Set<FormBuilderEntity>().Add(new FormBuilderEntity()
					{
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
						FormBuilderFieldId = item.Key,
						Value = item.Value
					});
				}
				await _dbContext.SaveChangesAsync();
			}
			catch { }
		}
		return new GenericResponse<FormBuilderEntity>(new FormBuilderEntity());
	}

}