namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class BlockController : BaseApiController {
	private readonly IBlockRepository _repository;

	public BlockController(IBlockRepository repository) => _repository = repository;

	[HttpGet("ReadMine")]
	public ActionResult<GenericResponse<IQueryable<UserEntity>>> ReadMine() => Result(_repository.ReadMine());

	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(string userId) => Result(await _repository.ToggleBlock(userId));
}