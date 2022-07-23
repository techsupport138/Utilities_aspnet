namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseApiController {
	private readonly ICategoryRepository _categoryRepository;

	public CategoryController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<CategoryReadDto>>> Create(CategoryCreateUpdateDto dto)
		=> Result(await _categoryRepository.Create(dto));

	[HttpGet]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<CategoryReadDto>>>> Read()
		=> Result(await _categoryRepository.Read());

	[HttpGet("Parent")]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<CategoryReadDto>>>> ReadParent()
		=> Result(await _categoryRepository.Read());

	[HttpPut]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(CategoryCreateUpdateDto dto)
		=> Result(await _categoryRepository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<IActionResult> Delete(Guid id) => Result(await _categoryRepository.Delete(id));
}