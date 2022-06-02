namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : BaseApiController {
    private readonly IReportRepository _repository;

    public ReportController(IReportRepository repository) {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse<IEnumerable<ReportReadDto>>>> Read(ReportFilterDto parameters) {
        var result = await _repository.Read(parameters);

        return Result(result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ReportReadDto?>>> Create(ReportCreateDto parameters) {
        var result = await _repository.Create(parameters);

        return Result(result);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Delete(Guid id) {
        var result = await _repository.Delete(id);

        return Result(result);
    }
}