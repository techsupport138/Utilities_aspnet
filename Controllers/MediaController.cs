namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : BaseApiController {
	private readonly IUploadRepository _repository;

	public MediaController(IUploadRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<MediaEntity>>> Upload([FromForm] UploadDto dto) => Result(await _repository.Upload(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> Delete(Guid id) => Result(await _repository.Delete(id));
}