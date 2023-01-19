namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : BaseApiController {
	private readonly IDiscountRepository _repository;

	public DiscountController(IDiscountRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> Create(DiscountEntity dto) => Result(await _repository.Create(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IEnumerable<DiscountEntity>>> Filter(DiscountFilterDto dto) => Result(_repository.Filter(dto));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> Update(DiscountEntity dto) => Result(await _repository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id) => Result(await _repository.Delete(id));

	[HttpGet("{code}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<DiscountEntity>>> ReadDiscountCode(string code) => Result(await _repository.ReadDiscountCode(code));
}