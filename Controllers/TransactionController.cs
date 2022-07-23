namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TransactionController : BaseApiController {
	private readonly ITransactionRepository _transactionRepository;

	public TransactionController(ITransactionRepository transactionRepository) {
		_transactionRepository = transactionRepository;
	}

	[HttpPost]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<TransactionReadDto>>> Create(TransactionCreateDto dto) {
		GenericResponse<TransactionReadDto?> i = await _transactionRepository.Create(dto);
		return Result(i);
	}

	[HttpGet]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<TransactionReadDto>>>> Read() {
		GenericResponse<IEnumerable<TransactionReadDto>> i = await _transactionRepository.Read();
		return Result(i);
	}

	[HttpGet("Mine")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<TransactionReadDto>>>> ReadMine() {
		GenericResponse<IEnumerable<TransactionReadDto>> i = await _transactionRepository.ReadMine();
		return Result(i);
	}
}