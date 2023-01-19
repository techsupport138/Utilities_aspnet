namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GlobalSearchController : BaseApiController {
	private readonly IGlobalSearchRepository _repository;

	public GlobalSearchController(IGlobalSearchRepository repository) {
		_repository = repository;
	}

	[AllowAnonymous]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[HttpPost]
	public async Task<ActionResult<GenericResponse<GlobalSearchDto>>> Create(GlobalSearchParams filter) {
		GenericResponse<GlobalSearchDto> i = await _repository.Filter(filter, User.Identity?.Name);
		return Result(i);
	}
}