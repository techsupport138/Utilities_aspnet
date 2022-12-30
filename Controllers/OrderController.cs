namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
public class OrderController : BaseApiController {
	private readonly IOrderRepository _orderRepository;

	public OrderController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

	[HttpPost]
	public async Task<ActionResult<GenericResponse<OrderEntity?>>> Create(OrderCreateUpdateDto dto) => Result(await _orderRepository.Create(dto));

	[HttpPut]
	public async Task<ActionResult<GenericResponse<OrderEntity?>>> Update(OrderCreateUpdateDto dto) => Result(await _orderRepository.Update(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IEnumerable<OrderEntity>>> Filter(OrderFilterDto dto) => Result(_orderRepository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<OrderEntity>>> ReadById(Guid id) => Result(await _orderRepository.ReadById(id));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<GenericResponse<OrderEntity>>> Delete(Guid id) => Result(await _orderRepository.Delete(id));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("CreateOrderDetailToOrder")]
	public async Task<ActionResult<GenericResponse<OrderEntity>>> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto)
		=> Result(await _orderRepository.CreateOrderDetailToOrder(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpDelete("DeleteOrderDetail")]
	public async Task<ActionResult<GenericResponse<OrderEntity>>> DeleteOrderDetail(Guid id) =>
		Result(await _orderRepository.DeleteOrderDetail(id));
}