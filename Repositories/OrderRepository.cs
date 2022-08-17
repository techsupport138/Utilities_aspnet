namespace Utilities_aspnet.Repositories;

public interface IOrderRepository
{
    GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto);
    Task<GenericResponse<OrderReadDto>> ReadById(Guid id);
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
    public GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto)
    {
        IQueryable<OrderEntity> q = _dbContext.Set<OrderEntity>().AsNoTracking();

        if (dto.Description.IsNotNullOrEmpty()) q = q.Where(x => (x.Description ?? "").Contains(dto.Description!));
        if (dto.Status.HasValue) q = q.Where(x => x.Status == dto.Status);
        if (dto.TotalPrice.HasValue) q = q.Where(x => x.TotalPrice == dto.TotalPrice);
        if (dto.DiscountPrice.HasValue) q = q.Where(x => x.DiscountPrice == dto.DiscountPrice);
        if (dto.DiscountPercent.HasValue) q = q.Where(x => x.DiscountPercent == dto.DiscountPercent);
        if (dto.DiscountCode.IsNotNullOrEmpty()) q = q.Where(x => (x.DiscountCode ?? "").Contains(dto.DiscountCode!));
        if (dto.SendPrice.HasValue) q = q.Where(x => x.SendPrice == dto.SendPrice);
        if (dto.SendType.HasValue) q = q.Where(x => x.SendType == dto.SendType);
        if (dto.PayType.HasValue) q = q.Where(x => x.PayType == dto.PayType);
        if (dto.PayDateTime.HasValue) q = q.Where(x => x.PayDateTime == dto.PayDateTime);
        if (dto.PayNumber.IsNotNullOrEmpty()) q = q.Where(x => (x.PayNumber ?? "").Contains(dto.PayNumber!));
        if (dto.ReceivedDate.HasValue) q = q.Where(x => x.ReceivedDate == dto.ReceivedDate);
         
        if (dto.UserId.IsNotNullOrEmpty())
        {
            q = q.Where(x => x.UserId == dto.UserId);
        }

        int totalCount = q.Count();

        q = q.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).AsNoTracking();

        return new GenericResponse<IQueryable<OrderEntity>>(q)
        {
            TotalCount = totalCount,
            PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto?.PageSize : totalCount / dto?.PageSize + 1,
            PageSize = dto?.PageSize
        };
    }

    public async Task<GenericResponse<OrderReadDto>> ReadById(Guid id)
    {
        OrderEntity? i = await _dbContext.Set<OrderEntity>()
            .AsNoTracking()
            .Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
        return new GenericResponse<OrderReadDto>(_mapper.Map<OrderReadDto>(i));

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