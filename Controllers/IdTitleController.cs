namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdTitleController : BaseApiController {
    private readonly ICategoryRepository _categoryRepository;

    public IdTitleController(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> Create(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.Create(dto);
        return Result(i);
    }
    
    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> Read() {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _categoryRepository.Read();
        return Result(i);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadById(Guid id) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.ReadById(id);
        i = await _categoryRepository.ReadById(id);
        return Result(i);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.Update(dto);
        return Result(i);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id) {
        GenericResponse i = await _categoryRepository.Delete(id);
        return Result(i);
    }
}