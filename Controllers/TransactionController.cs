namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
public class TransactionController : BaseApiController {
	private readonly ITransactionRepository _transactionRepository;

	public TransactionController(ITransactionRepository transactionRepository) => _transactionRepository = transactionRepository;

	[HttpPost]
	public async Task<ActionResult<GenericResponse<TransactionEntity>>> Create(TransactionEntity dto) => Result(await _transactionRepository.Create(dto));

	[HttpGet]
	public ActionResult<GenericResponse<IQueryable<TransactionEntity>>> Read() => Result(_transactionRepository.Read());

	[HttpGet("Mine")]
	public ActionResult<GenericResponse<IQueryable<TransactionEntity>>> ReadMine() => Result(_transactionRepository.ReadMine());
}