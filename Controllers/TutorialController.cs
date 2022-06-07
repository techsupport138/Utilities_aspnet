namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TutorialController : BaseApiController {
    private readonly IProductRepository<TutorialEntity> _repository;

    public TutorialController(IProductRepository<TutorialEntity> productRepository) {
        _repository = productRepository;
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Create(ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = await _repository.Create(dto);
        return Result(i);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Read() {
        GenericResponse<IEnumerable<ProductReadDto>> i = await _repository.Read(null);
        return Result(i);
    }
    
    [HttpGet("Mine")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> ReadMine() {
        GenericResponse<IEnumerable<ProductReadDto>> i = await _repository.ReadMine();
        return Result(i);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> ReadById(Guid id) {
        GenericResponse<ProductReadDto> i = await _repository.ReadById(id);
        return Result(i);
    }

    [HttpPost("Filter")]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Filter(FilterProductDto? dto) {
        GenericResponse<IEnumerable<ProductReadDto>> i = await _repository.Read(dto);
        return Result(i);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = await _repository.Update(dto);
        return Result(i);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id) {
        await _repository.Delete(id);
        return Ok();
    }
}