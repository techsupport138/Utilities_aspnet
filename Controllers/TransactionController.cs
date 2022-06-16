using Utilities_aspnet.Transaction;

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
    public async Task<ActionResult<GenericResponse<TransactionReadDto>>> Create(TransactionCreateDto dto) {
        GenericResponse<TransactionReadDto?> i = await _transactionRepository.Create(dto);
        return Result(i);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<List<TransactionReadDto>>>> Read() {
        GenericResponse<List<TransactionReadDto>> i = await _transactionRepository.Read();
        return Result(i);
    }
    [HttpGet("Mine")]
    public async Task<ActionResult<GenericResponse<List<TransactionReadDto>>>> ReadMine() {
        GenericResponse<List<TransactionReadDto>> i = await _transactionRepository.ReadMine();
        return Result(i);
    }

}