namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GlobalSearchController : BaseApiController {
	private readonly IGlobalSearchRepository _globalSearchRepository;

	public GlobalSearchController(IGlobalSearchRepository globalSearchRepository) {
		_globalSearchRepository = globalSearchRepository;
	}

	[AllowAnonymous]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost]
	public async Task<ActionResult<GenericResponse<GlobalSearchDto>>> Create(GlobalSearchParams filter) {
		GenericResponse<GlobalSearchDto> i = await _globalSearchRepository.Filter(filter, User.Identity?.Name);
		return Result(i);
	}
}