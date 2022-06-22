namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class BlockController : BaseApiController {
	private readonly IBlockRepository _repository;

	public BlockController(IBlockRepository repository) => _repository = repository;

	[HttpGet]
	public async Task<ActionResult<GenericResponse<BlockReadDto>>> Read() {
		GenericResponse<BlockReadDto> result = await _repository.Read();
		return Result(result);
	}
	
	[HttpGet("ReadMine")]
	public async Task<ActionResult<GenericResponse<BlockReadDto>>> ReadMine() {
		GenericResponse<BlockReadDto> result = await _repository.ReadMine();
		return Result(result);
	}

	[HttpPost]
	public async Task<ActionResult<GenericResponse>> Create(BlockCreateDto dto) {
		GenericResponse result = await _repository.ToggleBlock(dto);
		return Result(result);
	}

	
}