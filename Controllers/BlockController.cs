namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class BlockController : BaseApiController {
	private readonly IBlockRepository _repository;

	public BlockController(IBlockRepository repository) => _repository = repository;

	[HttpGet("ReadMine")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> ReadMine() => Result(await _repository.ReadMine());

	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(string userId) => Result(await _repository.ToggleBlock(userId));
}