namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : BaseApiController
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse<OrderReadDto?>>> Create(OrderCreateUpdateDto dto)
    {
        GenericResponse<OrderReadDto?> i = await _orderRepository.Create(dto);
        return Result(i);
    }

    [HttpPut]
    public async Task<ActionResult<GenericResponse<OrderReadDto?>>> Update(OrderCreateUpdateDto dto)
    {
        GenericResponse<OrderReadDto?> i = await _orderRepository.Update(dto);
        return Result(i);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [HttpPost("Filter")]
    public ActionResult<GenericResponse<IEnumerable<OrderReadDto>>> Filter(OrderFilterDto dto)
    {
        return Result(_orderRepository.Filter(dto));
    }
   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<OrderReadDto>>> ReadById(Guid id)
    {
        GenericResponse<OrderReadDto> i = await _orderRepository.ReadById(id);
        return Result(i);
    }


}