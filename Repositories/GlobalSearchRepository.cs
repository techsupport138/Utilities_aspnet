namespace Utilities_aspnet.Repositories;

public interface IGlobalSearchRepository {
	Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string userId);
}

public class GlobalSearchRepository : IGlobalSearchRepository {
	private readonly DbContext _context;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;
	private readonly ICategoryRepository _categoryRepository;

	public GlobalSearchRepository(
		DbContext context,
		IMapper mapper,
		IProductRepository productRepository,
		ICategoryRepository categoryRepository) {
		_context = context;
		_mapper = mapper;
		_mapper = mapper;
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
	}

	public async Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string userId) {
		GlobalSearchDto model = new();

		IEnumerable<CategoryEntity> categoryList = await _context.Set<CategoryEntity>().Include(x => x.Users)
			.Include(x => x.Media).Include(i => i.Media)
			.Where(x => x.Title.Contains(filter.Title) && filter.Category && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt).ToListAsync();

		IQueryable<UserEntity> userList = _context.Set<UserEntity>().Where(x => x.FullName.Contains(filter.Title) && filter.User);

		if (filter.Minimal)
			userList = userList.OrderByDescending(x => x.CreatedAt);
		else
			userList = userList.Include(u => u.Media)
				.Include(u => u.Categories)
				.Include(u => u.Location)
				.Include(u => u.Products)
				.OrderByDescending(x => x.CreatedAt);

		IQueryable<ProductEntity> productList = _context.Set<ProductEntity>()
			.Where(x => x.Title.Contains(filter.Title) && filter.Product && x.DeletedAt == null)
			.OrderByDescending(x => x.CreatedAt).AsNoTracking();

		if (filter.Minimal)
			productList = productList.Include(i => i.Media)
				.Include(i => i.Categories)
				.Include(i => i.Locations)
				.Include(i => i.User).ThenInclude(x => x.Media)
				.Include(i => i.User).ThenInclude(x => x.Categories)
				.Include(i => i.Bookmarks);
		else
			productList = _context.Set<ProductEntity>()
				.Where(x => x.Title.Contains(filter.Title) && filter.Product && x.DeletedAt == null)
				.Include(i => i.Media)
				.Include(i => i.Categories)
				.Include(i => i.Comments.Where(x => x.ParentId == null))!.ThenInclude(x => x.Children)
				.Include(i => i.Comments.Where(x => x.ParentId == null))!.ThenInclude(x => x.Media)
				.Include(i => i.Locations)
				.Include(i => i.Reports)
				.Include(i => i.Votes)
				.Include(i => i.User)!.ThenInclude(x => x.Media)
				.Include(i => i.User)!.ThenInclude(x => x.Categories)
				.Include(i => i.Bookmarks)
				.Include(i => i.Forms)!
				.ThenInclude(x => x.FormField)
				.Include(i => i.Teams)!
				.ThenInclude(x => x.User)!.ThenInclude(x => x.Media)
				.Include(i => i.VoteFields)!
				.ThenInclude(x => x.Votes)
				.OrderByDescending(x => x.CreatedAt);

		if (filter.IsFollowing) {
			List<string?> userFollowing = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == userId)
				.Select(x => x.FollowsUserId).ToListAsync();

			productList = productList.Where(x => userFollowing.Contains(x.UserId));
		}
		if (filter.Oldest) {
			categoryList = categoryList.OrderBy(x => x.CreatedAt).ToList();
			userList = userList.OrderBy(x => x.CreatedAt);
			productList = productList.OrderBy(x => x.CreatedAt);
		}
		if (filter.Reputation) {
			productList = productList.OrderByDescending(x => x.VisitsCount);
		}

		if (filter.IsMine) {
			productList = productList.Where(x => x.UserId == userId);
			categoryList = categoryList.Where(x => x.Users.Any(x => x.Id == userId));
			userList = userList.Where(x => x.Id == userId);
		}

		if (filter.Categories.IsNotNullOrEmpty()) {
			productList = productList.Where(x => x.Categories.Any(x => filter.Categories.Contains(x.Id)));
			categoryList = categoryList.Where(x => filter.Categories.Contains(x.Id));
			userList = userList.Where(x => x.Categories.Any(x => filter.Categories.Contains(x.Id)));
		}

		model.Categories = _mapper.Map<IEnumerable<CategoryReadDto>>(categoryList);
		model.Users = _mapper.Map<IEnumerable<UserReadDto>>(userList);
		model.Products = _mapper.Map<IEnumerable<ProductReadDto>>(productList);
		if (filter.IsBookmark) {
			model.Products = model.Products.Where(x => x.IsBookmarked).ToList();
		}
		return new GenericResponse<GlobalSearchDto>(model);
	}
}