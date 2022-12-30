namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : BaseApiController {
	private readonly IUploadRepository _uploadRepository;

	public MediaController(IUploadRepository uploadRepository) => _uploadRepository = uploadRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<MediaEntity>>> Upload([FromForm] UploadDto dto) => Result(await _uploadRepository.Upload(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> Delete(Guid id) => Result(await _uploadRepository.Delete(id));
}
