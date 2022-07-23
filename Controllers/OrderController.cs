namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : BaseApiController {
	private readonly IOrderRepository _orderRepository;

	public OrderController(IOrderRepository orderRepository) {
		_orderRepository = orderRepository;
	}

	[HttpPost]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<OrderReadDto?>>> CreateUpdate(OrderCreateUpdateDto dto) {
		GenericResponse<OrderReadDto?> i = await _orderRepository.CreateUpdate(dto);
		return Result(i);
	}

	[HttpGet]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<OrderReadDto>>>> Read() {
		GenericResponse<IEnumerable<OrderReadDto>> i = await _orderRepository.Read();
		return Result(i);
	}

	[HttpGet("{id:guid}")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<OrderReadDto?>>> ReadById(Guid id) {
		GenericResponse<OrderReadDto?> i = await _orderRepository.ReadById(id);
		return Result(i);
	}

	[HttpGet("Mine")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<OrderReadDto>>>> ReadMine() {
		GenericResponse<IEnumerable<OrderReadDto>> i = await _orderRepository.ReadMine();
		return Result(i);
	}
}