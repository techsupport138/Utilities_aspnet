namespace Utilities_aspnet.Repositories;

public interface IOrderRepository
{
    Task<GenericResponse<IEnumerable<OrderReadDto>>> Read();
    Task<GenericResponse<OrderReadDto>> ReadById(Guid id);
    Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine();
    Task<GenericResponse<OrderReadDto?>> CreateUpdate(OrderCreateUpdateDto dto);

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

    public async Task<GenericResponse<OrderReadDto?>> CreateUpdate(OrderCreateUpdateDto dto)
    {
        string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
        OrderEntity? oldOrder = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.UserId == userId && x.Id == dto.Id);

        if (oldOrder == null)
        {
            //create
            OrderEntity entityOrder = new()
            {
                Description = dto.Description,
                ReceivedDate = dto.ReceivedDate,
                UserId = userId,
                TotalPrice = dto.OrderDetails.Sum(x => x.Price),
                DiscountPercent = dto.DiscountPercent,
                DiscountPrice = (dto.OrderDetails.Sum(x => x.Price) * dto.DiscountPercent) / 100,
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
                if (productEntity != null && productEntity.Stock < item.SaleCount)
                {
                    await Delete(entityOrder.Id);
                    throw new ArgumentException("failed request! this request is more than stock!");
                }

                OrderDetailEntity orderDetailEntity = new()
                {
                    OrderId = entityOrder.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    SaleCount = item.SaleCount,
                };
                await _dbContext.Set<OrderDetailEntity>().AddAsync(orderDetailEntity);
                await _dbContext.SaveChangesAsync();

                if (item.Forms != null)
                    foreach (var data in item.Forms)
                    {
                        FormEntity formEntity = new()
                        {
                            Title = data.Title,
                            UserId = userId,
                            ProductId = orderDetailEntity.ProductId,
                            FormFieldId = data.FormField?.Id,
                            OrderDetailId = orderDetailEntity.Id,
                        };
                        await _dbContext.Set<FormEntity>().AddAsync(formEntity);
                        await _dbContext.SaveChangesAsync();
                    }

            }


            return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(entityOrder));
        }


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
                    if (product.Stock < item.SaleCount)
                        throw new ArgumentException("failed request! this request is more than stock!");

                    if (dto.Status == OrderStatuses.Paid)
                        if (product.Stock > 0) product.Stock = product.Stock - item.SaleCount;
                        else
                            throw new ArgumentException("product's stock equals zero!");

                }

                oldOrderDetail.ProductId = item.ProductId;
                oldOrderDetail.Price = item.Price;
                oldOrderDetail.SaleCount = item.SaleCount;

                if (item.Forms != null)
                    foreach (var data in item.Forms)
                    {
                        FormEntity? oldForms = await _dbContext.Set<FormEntity>().FirstOrDefaultAsync(x => x.Id == data.Id);
                        if (oldForms != null)
                        {
                            oldForms.Title = data.Title;
                        }
                    }
            }
        }

        await _dbContext.SaveChangesAsync();

        return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(oldOrder));
    }

    public async Task<GenericResponse<IEnumerable<OrderReadDto>>> Read()
    {
        var orders = await _dbContext.Set<OrderEntity>()
           .AsNoTracking()
           .Include(i => i.OrderDetails)!
           .ThenInclude(i => i.Forms)!
           .ThenInclude(x => x.FormField)
           .ToListAsync();
        IEnumerable<OrderReadDto> i = _mapper.Map<IEnumerable<OrderReadDto>>(orders).ToList();
        return new GenericResponse<IEnumerable<OrderReadDto>>(i);
    }

    public async Task<GenericResponse<OrderReadDto>> ReadById(Guid id)
    {
        OrderEntity? i = await _dbContext.Set<OrderEntity>().AsNoTracking()
            .Include(i => i.OrderDetails)!
            .ThenInclude(i => i.Forms)!
            .ThenInclude(x => x.FormField)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
        return new GenericResponse<OrderReadDto>(_mapper.Map<OrderReadDto>(i));

    }

    public async Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine()
    {
        IEnumerable<OrderEntity> orders = await _dbContext.Set<OrderEntity>()
            .AsNoTracking()
            .Include(i => i.OrderDetails)!
            .ThenInclude(i => i.Forms)!
            .ThenInclude(x => x.FormField)
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