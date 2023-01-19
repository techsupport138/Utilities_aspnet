namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
[Route("api/[controller]")]
public class BlockController : BaseApiController {
	private readonly IUserRepository _repository;

	public BlockController(IUserRepository repository) => _repository = repository;

	[HttpGet("ReadMine")]
	public ActionResult<GenericResponse<IQueryable<UserEntity>>> ReadMine() => Result(_repository.ReadMyBlockList());

	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(string userId) => Result(await _repository.ToggleBlock(userId));
}