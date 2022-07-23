namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopProductController : BaseApiController {
	private readonly ITopProductRepository _topProductRepository;

	public TopProductController(ITopProductRepository topProductRepository) => _topProductRepository = topProductRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<TopProductReadDto?>>> Create(TopProductCreateDto dto)
		=> Result(await _topProductRepository.Create(dto));

	[HttpGet]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<TopProductReadDto>?>>> Read()
		=> Result(await _topProductRepository.Read());

	[HttpGet("TopProduct")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<TopProductReadDto?>>> ReadTopProduct()
		=> Result(await _topProductRepository.ReadTopProduct());
}