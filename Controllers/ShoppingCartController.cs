using Utilities_aspnet.ShoppingCart;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : BaseApiController
{
    private readonly IShoppingCartRepository _repository;

    public ShoppingCartController(IShoppingCartRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<ShoppingCartReadDto>>> Read()
    {
        var result = await _repository.Read(Guid.Parse(User?.Identity?.Name!));

        return Result(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenericResponse<ShoppingCartReadDto>>> ReadById(Guid id)
    {
        var result = await _repository.ReadById(id);

        return Result(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<GenericResponse<ShoppingCartReadDto>>> Create(ShoppingCartCreateDto dto)
    {
        dto.UserId = Guid.Parse(User?.Identity?.Name!);

        var result = await _repository.Create(dto);

        return Result(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    public async Task<ActionResult<GenericResponse<ShoppingCartReadDto>>> Update(ShoppingCartUpdateDto dto)
    {
        var result = await _repository.Update(dto);

        return Result(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id:guid}/{itemId:guid}")]
    public async Task<ActionResult<GenericResponse<ShoppingCartReadDto>>> Delete(Guid id, Guid itemId)
    {
        var result = await _repository.Delete(id, itemId);

        return Result(result);
    }
}