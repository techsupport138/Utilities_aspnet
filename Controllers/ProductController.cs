using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

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

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Read()
		=> Result(await _productRepository.Read(new FilterProductDto()));

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

	#region Benchmark

	[HttpPost("Benchmark")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> BenchmarkCreate(ProductCreateUpdateDto dto)
		=> Result(await _productRepository.Create(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("Benchmark")]
	public IActionResult BenchmarkRead() {
		Summary? i = BenchmarkRunner.Run<ProductRepository>();
		return Result(new GenericResponse<string>(i.AllRuntimes));
	}

	[HttpGet("BenchmarkMine")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> BenchmarkReadMine()
		=> Result(await _productRepository.ReadMine());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("BenchmarkFilter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> BenchmarkFilter(FilterProductDto? dto)
		=> Result(await _productRepository.Read(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("Benchmark/{id:guid}")]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> BenchmarkReadById(Guid id)
		=> Result(await _productRepository.ReadById(id));

	[HttpPut("Benchmark")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> BenchmarkUpdate(ProductCreateUpdateDto dto)
		=> Result(await _productRepository.Update(dto));

	[HttpDelete("Benchmark/{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> BenchmarkDelete(Guid id) => Result(await _productRepository.Delete(id));

	#endregion
}