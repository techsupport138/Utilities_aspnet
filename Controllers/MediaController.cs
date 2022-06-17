namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : BaseApiController {
    private readonly IUploadRepository _uploadRepository;

    public MediaController(IUploadRepository uploadRepository) {
        _uploadRepository = uploadRepository;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<MediaDto>>> Upload([FromForm] UploadDto dto) {
        GenericResponse i = await _uploadRepository.Upload(dto);
        return Result(i);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Delete(Guid id) {
        GenericResponse i = await _uploadRepository.Delete(id);
        return Result(i);
    }
}