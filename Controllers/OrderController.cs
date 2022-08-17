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
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> Create(OrderCreateUpdateDto dto)  
		=> Result(await _orderRepository.Create(dto));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> Update(OrderCreateUpdateDto dto)
	 => Result(await _orderRepository.Update(dto));

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<OrderReadDto>>>> Read() {
		GenericResponse<IEnumerable<OrderReadDto>> i = await _orderRepository.Read();
		return Result(i);
	}


	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> ReadById(Guid id) {
		GenericResponse<OrderReadDto> i = await _orderRepository.ReadById(id);
		return Result(i);
	}

    [HttpGet("Mine")]
    public async Task<ActionResult<GenericResponse<IEnumerable<OrderReadDto>>>> ReadMine()
    {
        GenericResponse<IEnumerable<OrderReadDto>> i = await _orderRepository.ReadMine();
        return Result(i);
    }
}