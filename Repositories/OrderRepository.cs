namespace Utilities_aspnet.Repositories;

public interface IOrderRepository
{
    Task<GenericResponse<IEnumerable<OrderReadDto>>> Read();
    Task<GenericResponse<OrderReadDto>> ReadById(Guid id);
    Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine();
    Task<GenericResponse<OrderReadDto?>> Create(OrderCreateUpdateDto dto);
    Task<GenericResponse<OrderReadDto?>> Update(OrderCreateUpdateDto dto);
    public Task<GenericResponse> Delete(Guid id);
}

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public OrderRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<OrderReadDto?>> Create(OrderCreateUpdateDto dto)
    {
        string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
        double totalPrice = 0;


        //create
        OrderEntity entityOrder = new()
        {
            Description = dto.Description,
            ReceivedDate = dto.ReceivedDate,
            UserId = userId,
            DiscountPercent = dto.DiscountPercent,
            DiscountCode = dto.DiscountCode,
            PayType = PayType.Online,
            SendPrice = 0,
            SendType = SendType.Pishtaz,
            Status = OrderStatuses.Pending,
            PayNumber = ""
        };

        await _dbContext.Set<OrderEntity>().AddAsync(entityOrder);
        await _dbContext.SaveChangesAsync();

        foreach (var item in dto.OrderDetails)
        {
            ProductEntity? productEntity = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
            if (productEntity != null && productEntity.Stock < item.Count)
            {
                await Delete(entityOrder.Id);
                throw new ArgumentException("failed request! this request is more than stock!");
            }

            OrderDetailEntity orderDetailEntity = new()
            {
                OrderId = entityOrder.Id,
                ProductId = item.ProductId,
                Price = productEntity?.Price ?? 0,
                SaleCount = item.Count,
            };
            await _dbContext.Set<OrderDetailEntity>().AddAsync(orderDetailEntity);
            await _dbContext.SaveChangesAsync();

            totalPrice += Convert.ToDouble(productEntity?.Price ?? 0);
        }


        entityOrder.TotalPrice = totalPrice;
        entityOrder.DiscountPrice = totalPrice * dto.DiscountPercent / 100;


        return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(entityOrder));

    }

    public async Task<GenericResponse<OrderReadDto?>> Update(OrderCreateUpdateDto dto)
    {
        string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
        OrderEntity? oldOrder = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.UserId == userId && x.Id == dto.Id);
        if (oldOrder == null)
            throw new ArgumentException("not found!");

        //edit order
        oldOrder.Description = dto.Description;
        oldOrder.ReceivedDate = dto.ReceivedDate;
        oldOrder.Status = dto.Status;
        oldOrder.TotalPrice = dto.TotalPrice;
        oldOrder.DiscountPrice = dto.DiscountPrice;
        oldOrder.PayType = dto.PayType;
        oldOrder.SendPrice = dto.SendPrice;
        oldOrder.SendType = dto.SendType;
        oldOrder.DiscountCode = dto.DiscountCode;
        oldOrder.DiscountPercent = dto.DiscountPercent;

        foreach (var item in dto.OrderDetails)
        {
            OrderDetailEntity? oldOrderDetail = await _dbContext.Set<OrderDetailEntity>().FirstOrDefaultAsync(x => x.Id == item.Id);
            if (oldOrderDetail != null)
            {
                ProductEntity? product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
                if (product != null)
                {
                    if (product.Stock < item.Count)
                        throw new ArgumentException("failed request! this request is more than stock!");

                    if (dto.Status == OrderStatuses.Paid)
                        if (product.Stock > 0) product.Stock = product.Stock - item.Count;
                        else
                            throw new ArgumentException("product's stock equals zero!");

                }

                oldOrderDetail.ProductId = item.ProductId;
                oldOrderDetail.Price = product?.Price ?? 0;
                oldOrderDetail.SaleCount = item.Count;
            }
        }

        await _dbContext.SaveChangesAsync();

        return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(oldOrder));

    }
    public async Task<GenericResponse<IEnumerable<OrderReadDto>>> Read()
    {
        var orders = await _dbContext.Set<OrderEntity>()
           .AsNoTracking()
           .Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
           .Include(c => c.OrderDetails)!.ThenInclude(f => f.Forms)!.ThenInclude(x => x.FormField)
           .ToListAsync();
        IEnumerable<OrderReadDto> i = _mapper.Map<IEnumerable<OrderReadDto>>(orders).ToList();
        return new GenericResponse<IEnumerable<OrderReadDto>>(i);
    }

    public async Task<GenericResponse<OrderReadDto>> ReadById(Guid id)
    {
        OrderEntity? i = await _dbContext.Set<OrderEntity>()
            .AsNoTracking()
            .Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
            .Include(c => c.OrderDetails)!.ThenInclude(f => f.Forms)!.ThenInclude(x => x.FormField)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
        return new GenericResponse<OrderReadDto>(_mapper.Map<OrderReadDto>(i));

    }

    public async Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine()
    {
        IEnumerable<OrderEntity> orders = await _dbContext.Set<OrderEntity>()
            .AsNoTracking()
            .Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
            .Include(c => c.OrderDetails)!.ThenInclude(f => f.Forms)!.ThenInclude(x => x.FormField)
            .Where(x => x.DeletedAt == null && x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)
            .ToListAsync();
        IEnumerable<OrderReadDto> i = _mapper.Map<IEnumerable<OrderReadDto>>(orders).ToList();
        return new GenericResponse<IEnumerable<OrderReadDto>>(i);
    }
    public async Task<GenericResponse> Delete(Guid id)
    {
        OrderEntity? i = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(i => i.Id == id);

        if (i != null)
        {
            _dbContext.Remove(i);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");
        }

        return new GenericResponse();
    }
}