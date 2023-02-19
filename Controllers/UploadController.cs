namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : BaseApiController
{
    private readonly IFileManagerRepository _repository;
    public UploadController(IFileManagerRepository repository) => _repository = repository;

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement]
    [RequestSizeLimit(50_000_000)]
    public async Task<ActionResult<GenericResponse<ResponseSaveFileDto>>> UploadProduct(RequestSaveFileDto dto, CancellationToken ct)
    {
        var saveFile = await _repository.SaveFile(dto);
        if (saveFile == null || !saveFile.Result.Success) return BadRequest();

        var resultSaveInDb = await _repository.Create(dto, ct);
        return Result(resultSaveInDb);
    }
}