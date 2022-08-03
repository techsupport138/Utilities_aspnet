namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseApiController {
	private readonly ICategoryRepository _categoryRepository;

	public CategoryController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<CategoryReadDto>>> Create(CategoryCreateUpdateDto dto)
		=> Result(await _categoryRepository.Create(dto));

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<CategoryReadDto>>>> Read()
		=> Result(await _categoryRepository.Read());

	[HttpGet("Parent")]
	public async Task<ActionResult<GenericResponse<IEnumerable<CategoryReadDto>>>> ReadParent()
		=> Result(await _categoryRepository.Read());

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(CategoryCreateUpdateDto dto)
		=> Result(await _categoryRepository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> Delete(Guid id) => Result(await _categoryRepository.Delete(id));
}