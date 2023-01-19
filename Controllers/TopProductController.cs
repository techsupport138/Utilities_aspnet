namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopProductController : BaseApiController {
	private readonly ITopProductRepository _repository;

	public TopProductController(ITopProductRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<TopProductEntity?>>> Create(TopProductCreateDto dto) => Result(await _repository.Create(dto));

	[HttpGet]
	public ActionResult<GenericResponse<IEnumerable<TopProductEntity>?>> Read() => Result(_repository.Read());

	[HttpGet("TopProduct")]
	public async Task<ActionResult<GenericResponse<TopProductEntity?>>> ReadTopProduct() => Result(await _repository.ReadTopProduct());
}