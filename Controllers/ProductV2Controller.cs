namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductV2Controller : BaseApiController {
	private readonly IProductRepositoryV2 _productRepository;

	public ProductV2Controller(IProductRepositoryV2 productRepository) => _productRepository = productRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> Create(ProductCreateUpdateDto dto, CancellationToken ct)
		=> Result(await _productRepository.Create(dto, ct));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public ActionResult<GenericResponse<IQueryable<ProductEntity>>> Filter(ProductFilterDto dto) => Result(_productRepository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> ReadById(Guid id, CancellationToken ct)
		=> Result(await _productRepository.ReadById(id, ct));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<ProductEntity>>> Update(ProductCreateUpdateDto dto, CancellationToken ct)
		=> Result(await _productRepository.Update(dto, ct));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id, CancellationToken ct) => Result(await _productRepository.Delete(id, ct));
}