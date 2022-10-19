namespace Utilities_aspnet.Repositories;

public interface ITopProductRepository {
	Task<GenericResponse<TopProductEntity?>> ReadTopProduct();
	Task<GenericResponse<TopProductEntity?>> Create(TopProductCreateDto dto);
	GenericResponse<IQueryable<TopProductEntity>?> Read();
}

public class TopProductRepository : ITopProductRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly INotificationRepository _notificationRepository;

	public TopProductRepository(
		DbContext dbContext,
		IHttpContextAccessor httpContextAccessor,
		INotificationRepository notificationRepository) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
		_notificationRepository = notificationRepository;
	}

	public async Task<GenericResponse<TopProductEntity?>> Create(TopProductCreateDto dto) {
		ProductEntity? product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == dto.ProductId);
		TopProductEntity topProduct = new() {
			ProductId = dto.ProductId,
			CreatedAt = DateTime.Now,
			UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name
		};
		await _dbContext.Set<TopProductEntity>().AddAsync(topProduct);
		await _dbContext.SaveChangesAsync();

		TopProductEntity? entity = await _dbContext.Set<TopProductEntity>().Include(x => x.Product).ThenInclude(x => x.Media)
			.FirstOrDefaultAsync(x => x.Id == topProduct.Id);

		string? linkMedia = entity?.Product?.Media?.OrderBy(x => x.CreatedAt).Select(x => x.Link)?.FirstOrDefault();

		await _notificationRepository.Create(new NotificationCreateUpdateDto {
			Link = dto.ProductId.ToString(),
			Title = "Your Post Is TopPost",
			UserId = product?.UserId,
			UseCase = "TopProduct",
			Media = linkMedia
		});

		return new GenericResponse<TopProductEntity?>(entity);
	}

	public async Task<GenericResponse<TopProductEntity?>> ReadTopProduct() {
		TopProductEntity? entity = await _dbContext.Set<TopProductEntity>()
			.Include(x => x.Product).ThenInclude(x => x.Media)
			.Include(x => x.Product).ThenInclude(x => x.Categories)
			.Include(x => x.Product).ThenInclude(x => x.User)
			.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

		return new GenericResponse<TopProductEntity?>(entity);
	}

	public GenericResponse<IQueryable<TopProductEntity>?> Read() {
		IQueryable<TopProductEntity> entity = _dbContext.Set<TopProductEntity>()
			.Include(x => x.Product).ThenInclude(x => x.Media)
			.Include(x => x.Product).ThenInclude(x => x.Categories)
			.Include(x => x.Product).ThenInclude(x => x.User)
			.OrderByDescending(x => x.CreatedAt);

		return new GenericResponse<IQueryable<TopProductEntity>?>(entity);
	}
}