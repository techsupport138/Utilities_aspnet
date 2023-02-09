namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductV2Controller : BaseApiController {
	private readonly IProductRepository _repository;

	public ProductV2Controller(IProductRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> Create(ProductCreateUpdateDto dto, CancellationToken ct)
		=> Result(await _repository.Create(dto, ct));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IQueryable<ProductEntity>>> Filter(ProductFilterDto dto) => Result(_repository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> ReadById(Guid id, CancellationToken ct)
		=> Result(await _repository.ReadById(id, ct));

    [HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> Update(ProductCreateUpdateDto dto, CancellationToken ct)
		=> Result(await _repository.Update(dto, ct));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id, CancellationToken ct) => Result(await _repository.Delete(id, ct));
}