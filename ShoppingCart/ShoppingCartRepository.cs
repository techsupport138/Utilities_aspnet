using Utilities_aspnet.User;

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
        var shoppingCarts = _context.Set<ShoppingCartEntity>()
            .Include(x => x.User)
            .Include(x => x.ShoppingCartItems)!
            .Select(x => new ShoppingCartReadDto()
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                UserId = x.UserId,
                User = _mapper.Map<UserReadDto?>(x.User),
                ShoppingCartItems = x.ShoppingCartItems.ToList().Select(y => new ShoppingCartItemReadDto()
                {
                    Id = y.Id,
                    Quantity = y.Quantity, 
                    ShoppingCartId = y.ShoppingCartId,
                    ProductId = y.ProductId,
                    Product = _mapper.Map<ProductReadDto?>(y.Product),
                    ProjectId = y.ProjectId,
                    Project = _mapper.Map<ProductReadDto?>(y.Project),
                    DailyPriceId = y.DailyPriceId,
                    DailyPrice = _mapper.Map<ProductReadDto?>(y.DailyPrice),
                    TutorialId = y.TutorialId,
                    Tutorial = _mapper.Map<ProductReadDto?>(y.Tutorial),
                    EventId = y.EventId,
                    Event = _mapper.Map<ProductReadDto?>(y.Event),
                    AdId = y.AdId,
                    Ad = _mapper.Map<ProductReadDto?>(y.Ad),
                    CompanyId = y.CompanyId,
                    Company = _mapper.Map<ProductReadDto?>(y.Company),
                    TenderId = y.TenderId,
                    Tender = _mapper.Map<ProductReadDto?>(y.Tender),
                    ServiceId = y.ServiceId,
                    Service = _mapper.Map<ProductReadDto?>(y.Service),
                    MagazineId = y.MagazineId,
                    Magazine = _mapper.Map<ProductReadDto?>(y.Magazine),
                }).ToList()
            })
            .ToList()
            .FirstOrDefault(x => x.UserId == userId);

        return new GenericResponse<ShoppingCartReadDto?>(shoppingCarts);
    }

    public async Task<GenericResponse<ShoppingCartReadDto?>> ReadById(Guid shoppingCartId)
    {
        var shoppingCarts = _context.Set<ShoppingCartEntity>()
            .Include(x => x.User)
            .Include(x => x.ShoppingCartItems)!
            .Select(x => new ShoppingCartReadDto()
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                UserId = x.UserId,
                User = _mapper.Map<UserReadDto?>(x.User),
                ShoppingCartItems = x.ShoppingCartItems.ToList().Select(y => new ShoppingCartItemReadDto()
                {
                    Id = y.Id,
                    Quantity = y.Quantity,
                    ShoppingCartId = y.ShoppingCartId,
                    ProductId = y.ProductId,
                    Product = _mapper.Map<ProductReadDto?>(y.Product),
                    ProjectId = y.ProjectId,
                    Project = _mapper.Map<ProductReadDto?>(y.Project),
                    DailyPriceId = y.DailyPriceId,
                    DailyPrice = _mapper.Map<ProductReadDto?>(y.DailyPrice),
                    TutorialId = y.TutorialId,
                    Tutorial = _mapper.Map<ProductReadDto?>(y.Tutorial),
                    EventId = y.EventId,
                    Event = _mapper.Map<ProductReadDto?>(y.Event),
                    AdId = y.AdId,
                    Ad = _mapper.Map<ProductReadDto?>(y.Ad),
                    CompanyId = y.CompanyId,
                    Company = _mapper.Map<ProductReadDto?>(y.Company),
                    TenderId = y.TenderId,
                    Tender = _mapper.Map<ProductReadDto?>(y.Tender),
                    ServiceId = y.ServiceId,
                    Service = _mapper.Map<ProductReadDto?>(y.Service),
                    MagazineId = y.MagazineId,
                    Magazine = _mapper.Map<ProductReadDto?>(y.Magazine),
                }).ToList()
            })
            .ToList()
            .FirstOrDefault(x => x.Id == shoppingCartId);
        return new GenericResponse<ShoppingCartReadDto?>(shoppingCarts);
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

        if (item == null)
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

        if (item == null)
            return await ReadById(shoppingCart.Id);

        shoppingCart.ShoppingCartItems?.RemoveAll(x => x.Id == shoppingCartItemId);

        await _context.SaveChangesAsync();

        return await ReadById(shoppingCart.Id);
    }
}