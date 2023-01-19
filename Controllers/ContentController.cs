namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : BaseApiController {
	private readonly IContentRepository _repository;

	public ContentController(IContentRepository repository) => _repository = repository;

	[HttpGet]
	public ActionResult<GenericResponse<IQueryable<ContentEntity>>> Read() => Result(_repository.Read());

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ContentEntity>>> Create(ContentEntity dto) => Result(await _repository.Create(dto));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ContentEntity>>> Update(ContentEntity dto) => Result(await _repository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id) => Result(await _repository.Delete(id));
}