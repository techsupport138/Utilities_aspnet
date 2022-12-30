namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopProductController : BaseApiController {
	private readonly ITopProductRepository _topProductRepository;

	public TopProductController(ITopProductRepository topProductRepository) => _topProductRepository = topProductRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<TopProductEntity?>>> Create(TopProductCreateDto dto) => Result(await _topProductRepository.Create(dto));

	[HttpGet]
	public ActionResult<GenericResponse<IEnumerable<TopProductEntity>?>> Read() => Result(_topProductRepository.Read());

	[HttpGet("TopProduct")]
	public async Task<ActionResult<GenericResponse<TopProductEntity?>>> ReadTopProduct() => Result(await _topProductRepository.ReadTopProduct());
}