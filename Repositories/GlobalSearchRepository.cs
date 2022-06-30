namespace Utilities_aspnet.Repositories;

public interface IGlobalSearchRepository {
	Task<GenericResponse> Filter();
}

public class GlobalSearchRepository : IGlobalSearchRepository
{
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public GlobalSearchRepository(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor)
	{
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	
	public async Task<GenericResponse> Filter() {



		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}