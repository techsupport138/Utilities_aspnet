namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdTitleController : BaseApiController {
    private readonly IIdTitleRepository<BrandEntity> _brandRepository;
    private readonly IIdTitleRepository<CategoryEntity> _categoryRepository;
    private readonly IIdTitleRepository<ReferenceEntity> _referenceRepository;

    public IdTitleController(
        IIdTitleRepository<CategoryEntity> categoryRepository,
        IIdTitleRepository<BrandEntity> brandRepository,
        IIdTitleRepository<ReferenceEntity> referenceRepository) {
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _referenceRepository = referenceRepository;
    }

    [HttpPost("Category")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> CreateCategory(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.Create(dto);
        return Result(i);
    }

    [HttpGet("Category")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadCategory() {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _categoryRepository.Read();
        return Result(i);
    }

    [HttpGet("Category/{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadCategoryById(Guid id) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.ReadById(id);
        return Result(i);
    }

    [HttpGet("Category/ReadByUseCase/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadCategoryByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _categoryRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Category/{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateCategory(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Category/{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id) {
        await _categoryRepository.Delete(id);
        return Ok();
    }

    [HttpPost("Brand")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> CreateBrand(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _brandRepository.Create(dto);
        return Result(i);
    }

    [HttpGet("Brand")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadBrand() {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _brandRepository.Read();
        return Result(i);
    }

    [HttpGet("Brand/{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadBrandById(Guid id) {
        GenericResponse<IdTitleReadDto> i = await _brandRepository.ReadById(id);
        return Result(i);
    }
    
    [HttpGet("Brand/ReadByUseCase/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadBrandByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _brandRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Brand/{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateBrand(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _brandRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Brand/{id:guid}")]
    public async Task<IActionResult> DeleteBrand(Guid id) {
        await _brandRepository.Delete(id);
        return Ok();
    }

    [HttpPost("Reference")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> CreateReference(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _referenceRepository.Create(dto);
        return Result(i);
    }

    [HttpGet("Reference")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadReference() {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _referenceRepository.Read();
        return Result(i);
    }

    [HttpGet("Reference/{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadReferenceById(Guid id) {
        GenericResponse<IdTitleReadDto> i = await _referenceRepository.ReadById(id);
        return Result(i);
    }
    
    [HttpGet("Reference/ReadByUseCase/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadReferenceByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _referenceRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Reference/{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateReference(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _referenceRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Reference/{id:guid}")]
    public async Task<IActionResult> DeleteReference(Guid id) {
        await _referenceRepository.Delete(id);
        return Ok();
    }
}