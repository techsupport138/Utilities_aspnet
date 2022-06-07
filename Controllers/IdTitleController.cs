namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdTitleController : BaseApiController {
    private readonly IIdTitleRepository<BrandEntity> _brandRepository;
    private readonly IIdTitleRepository<CategoryEntity> _categoryRepository;
    private readonly IIdTitleRepository<ReferenceEntity> _referenceRepository;
    private readonly IIdTitleRepository<SpecialityEntity> _specialityRepository;

    public IdTitleController(
        IIdTitleRepository<CategoryEntity> categoryRepository,
        IIdTitleRepository<BrandEntity> brandRepository,
        IIdTitleRepository<ReferenceEntity> referenceRepository,
        IIdTitleRepository<SpecialityEntity> specialityRepository) {
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _referenceRepository = referenceRepository;
        _specialityRepository = specialityRepository;
    }

    [HttpPost("{type}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> Create(string type, IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.Create(dto);
                break;
            case "brand":
                i = await _brandRepository.Create(dto);
                break;
            case "refernce":
                i = await _referenceRepository.Create(dto);
                break;
            case "speciality":
                i = await _specialityRepository.Create(dto);
                break;
        }

        return Result(i);
    }

    [HttpGet("{type}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> Read(string type) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.Read();
                break;
            case "brand":
                i = await _brandRepository.Read();
                break;
            case "refernce":
                i = await _referenceRepository.Read();
                break;
            case "speciality":
                i = await _specialityRepository.Read();
                break;
        }

        return Result(i);
    }

    [HttpGet("{type}/{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadById(string type, Guid id) {
        GenericResponse<IdTitleReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.ReadById(id);
                break;
            case "brand":
                i = await _brandRepository.ReadById(id);
                break;
            case "refernce":
                i = await _referenceRepository.ReadById(id);
                break;
            case "speciality":
                i = await _specialityRepository.ReadById(id);
                break;
        }

        return Result(i);
    }

    [HttpGet("{type}/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadByUseCase(
        string type,
        IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.ReadByUseCase(useCase);
                break;
            case "brand":
                i = await _brandRepository.ReadByUseCase(useCase);
                break;
            case "refernce":
                i = await _referenceRepository.ReadByUseCase(useCase);
                break;
            case "speciality":
                i = await _specialityRepository.ReadByUseCase(useCase);
                break;
        }

        return Result(i);
    }

    [HttpPut("{type}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(string type, IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.Update(dto);
                break;
            case "brand":
                i = await _brandRepository.Update(dto);
                break;
            case "refernce":
                i = await _referenceRepository.Update(dto);
                break;
            case "speciality":
                i = await _specialityRepository.Update(dto);
                break;
        }

        return Result(i);
    }

    [HttpDelete("{type}/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(string type, Guid id) {
        GenericResponse i = new(UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.Delete(id);
                break;
            case "brand":
                i = await _brandRepository.Delete(id);
                break;
            case "refernce":
                i = await _referenceRepository.Delete(id);
                break;
            case "speciality":
                i = await _specialityRepository.Delete(id);
                break;
        }

        return Result(i);
    }

    [HttpPost("Category")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpGet("Category/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadCategoryByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _categoryRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Category")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateCategory(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _categoryRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Category/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCategory(Guid id) {
        await _categoryRepository.Delete(id);
        return Ok();
    }

    [HttpPost("Brand")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpGet("Brand/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadBrandByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _brandRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Brand")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateBrand(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _brandRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Brand/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteBrand(Guid id) {
        await _brandRepository.Delete(id);
        return Ok();
    }

    [HttpPost("Reference")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpGet("Reference/{useCase}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> ReadReferenceByUseCase(IdTitleUseCase useCase) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = await _referenceRepository.ReadByUseCase(useCase);
        return Result(i);
    }

    [HttpPut("Reference")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> UpdateReference(IdTitleCreateUpdateDto dto) {
        GenericResponse<IdTitleReadDto> i = await _referenceRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("Reference/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteReference(Guid id) {
        await _referenceRepository.Delete(id);
        return Ok();
    }
}