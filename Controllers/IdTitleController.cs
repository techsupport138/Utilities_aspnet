namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]/{type}")]
public class IdTitleController : BaseApiController {
    private readonly IIdTitleRepository<BrandEntity> _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IIdTitleRepository<ReferenceEntity> _referenceRepository;
    private readonly IIdTitleRepository<SpecialityEntity> _specialityRepository;
    
    public IdTitleController(
        ICategoryRepository categoryRepository,
        IIdTitleRepository<BrandEntity> brandRepository,
        IIdTitleRepository<ReferenceEntity> referenceRepository,
        IIdTitleRepository<SpecialityEntity> specialityRepository) {
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _referenceRepository = referenceRepository;
        _specialityRepository = specialityRepository;
    }

    [HttpPost]
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
            case "reference":
                i = await _referenceRepository.Create(dto);
                break;
            case "speciality":
                i = await _specialityRepository.Create(dto);
                break;
        }

        return Result(i);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<IdTitleReadDto>>>> Read(string type) {
        GenericResponse<IEnumerable<IdTitleReadDto>> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.Read();
                break;
            case "categoryV2":
                i = await _categoryRepository.ReadV2();
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<IdTitleReadDto>>> ReadById(string type, Guid id) {
        GenericResponse<IdTitleReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "category":
                i = await _categoryRepository.ReadById(id);
                break;
            case "categoryV2":
                i = await _categoryRepository.ReadByIdV2(id);
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

    [HttpGet("{useCase}")]
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

    [HttpPut]
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

    [HttpDelete("{id:guid}")]
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
}