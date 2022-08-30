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
	public async Task<ActionResult<GenericResponse<TransactionEntity>>> Create(TransactionCreateDto dto) {
		GenericResponse<TransactionEntity?> i = await _transactionRepository.Create(dto);
		return Result(i);
	}

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<TransactionEntity>>>> Read() {
		GenericResponse<IEnumerable<TransactionEntity>> i = await _transactionRepository.Read();
		return Result(i);
	}

	[HttpGet("Mine")]
	public async Task<ActionResult<GenericResponse<IEnumerable<TransactionEntity>>>> ReadMine() {
		GenericResponse<IEnumerable<TransactionEntity>> i = await _transactionRepository.ReadMine();
		return Result(i);
	}
}