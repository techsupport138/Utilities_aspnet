namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : BaseApiController {
	private readonly IContentRepository _contentRepository;

	public ContentController(IContentRepository contentRepository) => _contentRepository = contentRepository;

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<ContentEntity>>>> Read() => Result(await _contentRepository.Read());

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<ContentEntity>>> ReadById(Guid id) => Result(await _contentRepository.ReadById(id));

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ContentEntity>>> Create(ContentEntity dto) => Result(await _contentRepository.Create(dto));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ContentEntity>>> Update(ContentEntity dto) => Result(await _contentRepository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> Delete(Guid id) => Result(await _contentRepository.Delete(id));
}