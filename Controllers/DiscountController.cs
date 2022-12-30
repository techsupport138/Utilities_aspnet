namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : BaseApiController {
	private readonly IDiscountRepository _discountRepository;

	public DiscountController(IDiscountRepository discountRepository) => _discountRepository = discountRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> Create(DiscountEntity dto) => Result(await _discountRepository.Create(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IEnumerable<DiscountEntity>>> Filter(DiscountFilterDto dto) => Result(_discountRepository.Filter(dto));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> Update(DiscountEntity dto) => Result(await _discountRepository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id) => Result(await _discountRepository.Delete(id));

	[HttpGet("{code}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> ReadDiscountCode(string code) => Result(await _discountRepository.ReadDiscountCode(code));
}