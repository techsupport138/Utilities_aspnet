namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : BaseApiController {
	private readonly IDiscountRepository _discountRepository;

	public DiscountController(IDiscountRepository discountRepository) => _discountRepository = discountRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<DiscountReadDto>>> Create(DiscountCreateUpdateDto dto) => Result(await _discountRepository.Create(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<DiscountReadDto>>>> Filter(DiscountFilterDto dto) => Result(await _discountRepository.Read(dto));


	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<DiscountReadDto>>> Update(DiscountCreateUpdateDto dto) => Result(await _discountRepository.Update(dto));


	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> Delete(Guid id) => Result(await _discountRepository.Delete(id));


	[HttpGet("{code}")]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> ReadDiscountCode(string code)
	{
		GenericResponse<DiscountEntity> i = await _discountRepository.ReadDiscountCode(code);
		return Result(i);
	}
}