namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : BaseApiController {
    private readonly IUploadRepository _uploadRepository;

    public MediaController(IUploadRepository uploadRepository) {
        _uploadRepository = uploadRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult<GenericResponse<EnumDto>>> Upload([FromForm] UploadDto dto) {
        GenericResponse? i = await _uploadRepository.UploadMedia(dto);
        return Ok(i);
    }
}