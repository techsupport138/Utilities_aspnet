namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseApiController {
	private readonly ICategoryRepository _repository;

	public CategoryController(ICategoryRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<CategoryEntity>>> Create(CategoryEntity dto) => Result(await _repository.Create(dto));

	[HttpGet]
	public ActionResult<GenericResponse<IQueryable<CategoryEntity>>> Read() => Result(_repository.Read());

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<CategoryEntity>>> Update(CategoryEntity dto) => Result(await _repository.Update(dto));

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<IActionResult> Delete(Guid id) => Result(await _repository.Delete(id));
}