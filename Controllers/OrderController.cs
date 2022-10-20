namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : BaseApiController {
	private readonly IOrderRepository _orderRepository;

	public OrderController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

	[HttpPost]
	public async Task<ActionResult<GenericResponse<OrderReadDto?>>> Create(OrderCreateUpdateDto dto) => Result(await _orderRepository.Create(dto));

	[HttpPut]
	public async Task<ActionResult<GenericResponse<OrderReadDto?>>> Update(OrderCreateUpdateDto dto) => Result(await _orderRepository.Update(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IEnumerable<OrderReadDto>>> Filter(OrderFilterDto dto) => Result(_orderRepository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> ReadById(Guid id) => Result(await _orderRepository.ReadById(id));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> Delete(Guid id) => Result(await _orderRepository.Delete(id));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("CreateOrderDetailToOrder")]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto)
		=> Result(await _orderRepository.CreateOrderDetailToOrder(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpDelete("DeleteOrderDetail")]
	public async Task<ActionResult<GenericResponse<OrderReadDto>>> DeleteOrderDetail(Guid id) =>
		Result(await _orderRepository.DeleteOrderDetail(id));
}