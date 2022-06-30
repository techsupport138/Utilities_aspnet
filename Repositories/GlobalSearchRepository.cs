namespace Utilities_aspnet.Repositories;

public interface IGlobalSearchRepository {
	Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string userId);
}

public class GlobalSearchRepository : IGlobalSearchRepository
{
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public GlobalSearchRepository(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor)
	{
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	
	public async Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string userId) {
		var model = new GlobalSearchDto();

        IEnumerable<CategoryEntity> categoryList = await _context.Set<CategoryEntity>().Include(x => x.Users).Include(x => x.Media).Include(i => i.Media)
			.Include(i => i.Children).ThenInclude(i => i.Media).Where(x => x.ParentId == null).Where(x => x.Title.Contains(filter.Title) && filter.Category && x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt).ToListAsync();


        IEnumerable<UserEntity> userList = await _context.Set<UserEntity>().Where(x => x.FullName.Contains(filter.Title)  && filter.User).AsNoTracking()
			.Include(u => u.Media)
			.Include(u => u.Categories)
			.Include(u => u.Location)
			.Include(u => u.Products)
		  .OrderByDescending(x => x.CreatedAt).ToListAsync();



        IEnumerable<ProductEntity> productList = await _context.Set<ProductEntity>().Where(x => x.Title.Contains(filter.Title) && filter.Product && x.DeletedAt == null).AsNoTracking()
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
		.OrderByDescending(x => x.CreatedAt).ToListAsync();




        if (filter.Oldest)
        {
            categoryList = categoryList.OrderBy(x => x.CreatedAt).ToList();
            userList = userList.OrderBy(x => x.CreatedAt).ToList();
            productList = productList.OrderBy(x => x.CreatedAt).ToList();
        }
        if (filter.Reputation)
        {
            productList = productList.OrderByDescending(x => x.VisitsCount).ToList();
        }

        if (filter.IsMine)
        {
            productList = productList.Where(x => x.UserId == userId).ToList();
            categoryList = categoryList.Where(x => x.Users.Any(x=>x.Id == userId)).ToList();
            userList = userList.Where(x => x.Id == userId).ToList();

        }

        model.Categories = _mapper.Map<IEnumerable<CategoryReadDto>>(categoryList);
        model.Users = _mapper.Map<IEnumerable<UserReadDto>>(userList);
        model.Products = _mapper.Map<IEnumerable<ProductReadDto>>(productList);
        if (filter.IsBookmark)
        {
            model.Products = model.Products.Where(x => x.IsBookmarked == true).ToList();
        }
        return new GenericResponse<GlobalSearchDto>(model);
	}
}