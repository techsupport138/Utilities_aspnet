namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : BaseApiController {
	private readonly IContentRepository _contentRepository;

	public ContentController(IContentRepository contentRepository) {
		_contentRepository = contentRepository;
	}

	[HttpGet]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<ContentReadDto>>>> Read() {
		GenericResponse<IEnumerable<ContentReadDto>> i = await _contentRepository.Read();
		return Result(i);
	}

	[HttpGet("{id:guid}")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<ContentReadDto>>> ReadById(Guid id) {
		GenericResponse<ContentReadDto> i = await _contentRepository.ReadById(id);
		return Result(i);
	}

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<ContentReadDto>>> Create(ContentCreateUpdateDto dto) {
		GenericResponse<ContentReadDto> i = await _contentRepository.Create(dto);
		return Result(i);
	}

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<ContentReadDto>>> Update(ContentCreateUpdateDto dto) {
		GenericResponse<ContentReadDto> i = await _contentRepository.Update(dto);
		return Result(i);
	}

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<IActionResult> Delete(Guid id) {
		await _contentRepository.Delete(id);
		return Ok();
	}
}