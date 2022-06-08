namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseApiController {
    private readonly IProductRepository<ProductEntity> _productRepository;

    public ProductController(IProductRepository<ProductEntity> productRepository) {
        _productRepository = productRepository;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Create(ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = await _productRepository.Create(dto);
        return Result(i);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Read() {
        GenericResponse<IEnumerable<ProductReadDto>> i = await _productRepository.Read(null);
        return Result(i);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [HttpPost("Filter")]
    public async Task<ActionResult<GenericResponse<IEnumerable<ProductReadDto>>>> Filter(FilterProductDto? dto) {
        GenericResponse<IEnumerable<ProductReadDto>> i = await _productRepository.Read(dto);
        return Result(i);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> ReadById(Guid id) {
        GenericResponse<ProductReadDto> i = await _productRepository.ReadById(id);
        return Result(i);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ProductReadDto>>> Update(ProductCreateUpdateDto dto) {
        GenericResponse<ProductReadDto> i = await _productRepository.Update(dto);
        return Result(i);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id) {
        await _productRepository.Delete(id);
        return Ok();
    }
}