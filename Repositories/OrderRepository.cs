namespace Utilities_aspnet.Repositories;

public interface IOrderRepository {
	Task<GenericResponse<IEnumerable<OrderReadDto>>> Read();
	Task<GenericResponse<OrderReadDto?>> ReadById(Guid id);
	Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine();
	Task<GenericResponse<OrderReadDto?>> CreateUpdate(OrderCreateUpdateDto dto);
}

public class OrderRepository : IOrderRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public OrderRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
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


        oldOrder.Description = dto.Description;
        oldOrder.ReceivedDate = dto.ReceivedDate;
        oldOrder.Status = dto.Status;

        await _dbContext.SaveChangesAsync();

        return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(oldOrder));
    }

	public async Task<GenericResponse<IEnumerable<OrderReadDto>>> Read() {
		IEnumerable<OrderEntity> model = await _dbContext.Set<OrderEntity>().ToListAsync();
		return new GenericResponse<IEnumerable<OrderReadDto>>(_mapper.Map<IEnumerable<OrderReadDto>>(model));
	}

	public async Task<GenericResponse<OrderReadDto?>> ReadById(Guid id) {
		OrderEntity? model = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.Id == id);
		return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto?>(model));
	}

	public async Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine() {
		IEnumerable<OrderEntity> model =
			await _dbContext.Set<OrderEntity>()
				.Where(i => i.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)
				.ToListAsync();
		return new GenericResponse<IEnumerable<OrderReadDto>>(_mapper.Map<IEnumerable<OrderReadDto>>(model));
	}
   
}