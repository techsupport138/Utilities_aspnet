namespace Utilities_aspnet.ShoppingCart;

public interface IShoppingCartRepository
{
    Task<GenericResponse<ShoppingCartReadDto?>> Read(Guid userId);
    Task<GenericResponse<ShoppingCartReadDto?>> ReadById(Guid shoppingCartId);
    Task<GenericResponse<ShoppingCartReadDto?>> Create(ShoppingCartCreateDto dto);
    Task<GenericResponse<ShoppingCartReadDto?>> Update(ShoppingCartUpdateDto dto);
    Task<GenericResponse<ShoppingCartReadDto?>> Delete(Guid shoppingCartId, Guid shoppingCartItemId);
}

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public ShoppingCartRepository(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> Read(Guid userId)
    {
        ShoppingCartEntity? shoppingCarts = await _context.Set<ShoppingCartEntity>()
            .Include(x => x.User)
            .Include(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        ShoppingCartReadDto? result = _mapper.Map<ShoppingCartReadDto?>(shoppingCarts);

        return new GenericResponse<ShoppingCartReadDto?>(result);
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> ReadById(Guid shoppingCartId)
    {
        ShoppingCartEntity? shoppingCart = await _context.Set<ShoppingCartEntity>()
            .Include(x => x.User)
            .Include(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.Id == shoppingCartId);

        ShoppingCartReadDto? result = _mapper.Map<ShoppingCartReadDto?>(shoppingCart);

        return new GenericResponse<ShoppingCartReadDto?>(result);
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> Create(ShoppingCartCreateDto dto)
    {
        ShoppingCartEntity? shoppingCart = await _context.Set<ShoppingCartEntity>()
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId);

        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCartEntity()
            {
                UserId = dto.UserId
            };

            await _context.Set<ShoppingCartEntity>().AddAsync(shoppingCart);
        }

        dto.ShoppingCartItems.ForEach(async item =>
        {
            ShoppingCartItemEntity? shoppingCartItem = _mapper.Map<ShoppingCartItemEntity>(item);

            shoppingCartItem.ShoppingCartId = shoppingCart.Id;

            await _context.Set<ShoppingCartItemEntity>().AddAsync(shoppingCartItem);
        });

        await _context.SaveChangesAsync();

        return await ReadById(shoppingCart.Id);
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> Update(ShoppingCartUpdateDto dto)
    {
        ShoppingCartEntity? shoppingCart = await _context.Set<ShoppingCartEntity>()
            .Include(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.Id == dto.ShoppingCartId);

        if (shoppingCart == null)
            return new GenericResponse<ShoppingCartReadDto?>(null);

        ShoppingCartItemEntity? item = shoppingCart.ShoppingCartItems?.FirstOrDefault(x => x.Id == dto.ShoppingCartItemId);

        if(item == null)
            return await ReadById(shoppingCart.Id);

        item.Quantity = dto.Quantity;

        _context.Set<ShoppingCartEntity>().Update(shoppingCart);
        await _context.SaveChangesAsync();

        return await ReadById(shoppingCart.Id);
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> Delete(Guid shoppingCartId, Guid shoppingCartItemId)
    {
        ShoppingCartEntity? shoppingCart = await _context.Set<ShoppingCartEntity>()
            .Include(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.Id == shoppingCartId);

        if (shoppingCart == null)
            return new GenericResponse<ShoppingCartReadDto?>(null);

        ShoppingCartItemEntity? item = shoppingCart.ShoppingCartItems?.FirstOrDefault(x => x.Id == shoppingCartItemId);

        if(item == null)
            return await ReadById(shoppingCart.Id);

        shoppingCart.ShoppingCartItems?.RemoveAll(x => x.Id == shoppingCartItemId);

        await _context.SaveChangesAsync();

        return await ReadById(shoppingCart.Id);
    }
}