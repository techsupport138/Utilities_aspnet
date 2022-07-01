namespace Utilities_aspnet.Repositories;

public interface IOrderRepository {
	Task<GenericResponse<IEnumerable<OrderReadDto>>> Read();
	Task<GenericResponse<OrderReadDto?>> ReadById(Guid id);
	Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine();
	Task<GenericResponse<OrderReadDto?>> CreateUpdate(OrderCreateUpdateDto dto);
}

public class OrderRepository : IOrderRepository
{
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public OrderRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<OrderReadDto?>> CreateUpdate(OrderCreateUpdateDto dto) {
		string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
		OrderEntity? oldOrder = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x=>x.UserId == userId && x.Id == dto.Id);
		if(oldOrder == null)
        {
			OrderEntity entity = new OrderEntity { Description = dto.Description, ProductId = dto.ProductId, ReceivedDate = dto.ReceivedDate, UserId = userId };
			await _dbContext.Set<OrderEntity>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(entity));
		}
        else
        {
			oldOrder.ProductId = dto.ProductId;
			oldOrder.Description = dto.Description;
			oldOrder.ReceivedDate = dto.ReceivedDate;
			await _dbContext.SaveChangesAsync();
			
		}


		return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto>(oldOrder));
	}

	public async Task<GenericResponse<IEnumerable<OrderReadDto>>> Read() {
		IEnumerable<OrderEntity> model =
			await _dbContext.Set<OrderEntity>().ToListAsync();
		return new GenericResponse<IEnumerable<OrderReadDto>>(_mapper.Map<IEnumerable<OrderReadDto>>(model));
	}
	
	public async Task<GenericResponse<OrderReadDto?>> ReadById(Guid id) {
		OrderEntity? model =
			await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x=>x.Id == id);
		return new GenericResponse<OrderReadDto?>(_mapper.Map<OrderReadDto?>(model));
	}

	public async Task<GenericResponse<IEnumerable<OrderReadDto>>> ReadMine() {
		IEnumerable<OrderEntity> model =
			await _dbContext.Set<OrderEntity>().Where(i => i.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
				.ToListAsync();
		return new GenericResponse<IEnumerable<OrderReadDto>>(_mapper.Map<IEnumerable<OrderReadDto>>(model));
	}
}