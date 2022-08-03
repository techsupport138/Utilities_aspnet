namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseApiController {
	private readonly IProductRepository _productRepository;

	public ProductController(IProductRepository productRepository) => _productRepository = productRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> Create(ProductCreateUpdateDto dto)
		=> Result(await _productRepository.Create(dto));

	[HttpGet("Mine")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> ReadMine()
		=> Result(await _productRepository.ReadMine());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Filter(FilterProductDto? dto)
		=> Result(await _productRepository.Read(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("FilterV2")]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> FilterV2(ProductFilterDto dto)
		=> Result(await _productRepository.ReadV2(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> ReadById(Guid id)
		=> Result(await _productRepository.ReadById(id));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(ProductCreateUpdateDto dto)
		=> Result(await _productRepository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> Delete(Guid id) => Result(await _productRepository.Delete(id));
}