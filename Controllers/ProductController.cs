using Utilities_aspnet.Repositories;

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

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Read(string useCase)
		=> Result(await _productRepository.Read(null, useCase));

	[HttpGet("Mine")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> ReadMine(string useCase)
		=> Result(await _productRepository.ReadMine(useCase));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Filter(FilterProductDto? dto)
		=> Result(await _productRepository.Read(dto, dto.UseCase));

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