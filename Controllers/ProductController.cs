namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseApiController {
    private readonly IProductRepository<ProductEntity> _productRepository;
    private readonly IProductRepository<AdEntity> _adRepository;
    private readonly IProductRepository<DailyPriceEntity> _dailyPriceRepository;
    private readonly IProductRepository<CompanyEntity> _companyRepository;
    private readonly IProductRepository<MagazineEntity> _magazineRepository;
    private readonly IProductRepository<ProjectEntity> _projectRepository;
    private readonly IProductRepository<ServiceEntity> _serviceRepository;
    private readonly IProductRepository<TenderEntity> _tenderRepository;
    private readonly IProductRepository<TutorialEntity> _tutorialRepository;
    
    public ProductController(
        IProductRepository<ProductEntity> productRepository,
        IProductRepository<AdEntity> adRepository,
        IProductRepository<DailyPriceEntity> dailyPriceRepository,
        IProductRepository<CompanyEntity> companyRepository,
        IProductRepository<MagazineEntity> magazineRepository,
        IProductRepository<ProjectEntity> projectRepository,
        IProductRepository<ServiceEntity> serviceRepository,
        IProductRepository<TenderEntity> tenderRepository,
        IProductRepository<TutorialEntity> tutorialRepository) {
        _productRepository = productRepository;
        _adRepository = adRepository;
        _dailyPriceRepository = dailyPriceRepository;
        _companyRepository = companyRepository;
        _magazineRepository = magazineRepository;
        _projectRepository = projectRepository;
        _serviceRepository = serviceRepository;
        _tenderRepository = tenderRepository;
        _tutorialRepository = tutorialRepository;
    }

    [HttpPost("{type}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Create(string type, ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.Create(dto);
                break;
            case "ad":
                i = await _adRepository.Create(dto);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.Create(dto);
                break;
            case "magazine":
                i = await _magazineRepository.Create(dto);
                break;
            case "project":
                i = await _projectRepository.Create(dto);
                break;
            case "service":
                i = await _serviceRepository.Create(dto);
                break;
            case "tender":
                i = await _tenderRepository.Create(dto);
                break;
            case "tutorial":
                i = await _tutorialRepository.Create(dto);
                break;
            case "company":
                i = await _companyRepository.Create(dto);
                break;
        }

        return Result(i);
    }

    [HttpGet("{type}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Read(string type) {
        GenericResponse<IEnumerable<ProductReadDto>> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.Read(null);
                break;
            case "ad":
                i = await _adRepository.Read(null);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.Read(null);
                break;
            case "magazine":
                i = await _magazineRepository.Read(null);
                break;
            case "project":
                i = await _projectRepository.Read(null);
                break;
            case "service":
                i = await _serviceRepository.Read(null);
                break;
            case "tender":
                i = await _tenderRepository.Read(null);
                break;
            case "tutorial":
                i = await _tutorialRepository.Read(null);
                break;
        }

        return Result(i);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [HttpPost("{type}/Filter")]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Filter(string type, FilterProductDto? dto) {
        GenericResponse<IEnumerable<ProductReadDto>> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.Read(dto);
                break;
            case "ad":
                i = await _adRepository.Read(dto);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.Read(dto);
                break;
            case "magazine":
                i = await _magazineRepository.Read(dto);
                break;
            case "project":
                i = await _projectRepository.Read(dto);
                break;
            case "service":
                i = await _serviceRepository.Read(dto);
                break;
            case "tender":
                i = await _tenderRepository.Read(dto);
                break;
            case "tutorial":
                i = await _tutorialRepository.Read(dto);
                break;
        }

        return Result(i);
    }

    [HttpGet("{type}/{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> ReadById(string type, Guid id) {
        GenericResponse<ProductReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.ReadById(id);
                break;
            case "ad":
                i = await _adRepository.ReadById(id);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.ReadById(id);
                break;
            case "magazine":
                i = await _magazineRepository.ReadById(id);
                break;
            case "project":
                i = await _projectRepository.ReadById(id);
                break;
            case "service":
                i = await _serviceRepository.ReadById(id);
                break;
            case "tender":
                i = await _tenderRepository.ReadById(id);
                break;
            case "tutorial":
                i = await _tutorialRepository.ReadById(id);
                break;
        }

        return Result(i);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(string type, ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = new(null, UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.Update(dto);
                break;
            case "ad":
                i = await _adRepository.Update(dto);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.Update(dto);
                break;
            case "magazine":
                i = await _magazineRepository.Update(dto);
                break;
            case "project":
                i = await _projectRepository.Update(dto);
                break;
            case "service":
                i = await _serviceRepository.Update(dto);
                break;
            case "tender":
                i = await _tenderRepository.Update(dto);
                break;
            case "tutorial":
                i = await _tutorialRepository.Update(dto);
                break;
        }

        return Result(i);
    }

    [HttpDelete("{type}/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(string type, Guid id) {
        GenericResponse i = new(UtilitiesStatusCodes.BadRequest, "Type Not Found");
        switch (type) {
            case "product":
                i = await _productRepository.Delete(id);
                break;
            case "ad":
                i = await _adRepository.Delete(id);
                break;
            case "dailyPrice":
                i = await _dailyPriceRepository.Delete(id);
                break;
            case "magazine":
                i = await _magazineRepository.Delete(id);
                break;
            case "project":
                i = await _projectRepository.Delete(id);
                break;
            case "service":
                i = await _serviceRepository.Delete(id);
                break;
            case "tender":
                i = await _tenderRepository.Delete(id);
                break;
            case "tutorial":
                i = await _tutorialRepository.Delete(id);
                break;
        }

        return Result(i);
    }
}