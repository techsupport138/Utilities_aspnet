namespace Utilities_aspnet.Repositories;

public interface ITopProductRepository {
	Task<GenericResponse<TopProductReadDto?>> ReadTopProduct();
	Task<GenericResponse<TopProductReadDto?>> Create(TopProductCreateDto dto);
	Task<GenericResponse<IEnumerable<TopProductReadDto>?>> Read();
}

public class TopProductRepository : ITopProductRepository {
	private readonly DbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly INotificationRepository _notificationRepository;

	public TopProductRepository(
		DbContext dbContext,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		INotificationRepository notificationRepository) {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_notificationRepository = notificationRepository;
	}

	public async Task<GenericResponse<TopProductReadDto?>> Create(TopProductCreateDto dto) {
		TopProductEntity? entity = null;
		try {
			ProductEntity product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == dto.ProductId);
			TopProductEntity topProduct = new() {
				ProductId = dto.ProductId,
				CreatedAt = DateTime.Now,
				UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name
			};
			await _dbContext.Set<TopProductEntity>().AddAsync(topProduct);
			await _dbContext.SaveChangesAsync();
			entity = await _dbContext.Set<TopProductEntity>().Include(x => x.Product).ThenInclude(x => x.Media)
				.FirstOrDefaultAsync(x => x.Id == topProduct.Id);
			string? linkMedia = entity?.Product?.Media?.OrderBy(x => x.CreatedAt).Select(x => x.Link)?.FirstOrDefault();
			_notificationRepository.CreateNotification(new NotificationCreateUpdateDto {
				Link = dto.ProductId.ToString(), Title = "Your Post Is TopPost", UserId = product.UserId, UseCase = "TopProduct",
				Media = linkMedia
			});
		}
		catch {
			// ignored
		}

		return new GenericResponse<TopProductReadDto?>(_mapper.Map<TopProductReadDto>(entity));
	}

	public async Task<GenericResponse<TopProductReadDto?>> ReadTopProduct() {
		TopProductEntity? entity = await _dbContext.Set<TopProductEntity>().Include(x => x.Product).ThenInclude(x => x.Media)
			.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

		return new GenericResponse<TopProductReadDto?>(_mapper.Map<TopProductReadDto>(entity));
	}

	public async Task<GenericResponse<IEnumerable<TopProductReadDto>?>> Read() {
		IEnumerable<TopProductEntity> entity = await _dbContext.Set<TopProductEntity>().Include(x => x.Product)
			.ThenInclude(x => x.Media).OrderByDescending(x => x.CreatedAt).ToListAsync();

		return new GenericResponse<IEnumerable<TopProductReadDto>?>(_mapper.Map<IEnumerable<TopProductReadDto>?>(entity));
	}
}