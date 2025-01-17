﻿namespace Utilities_aspnet.Repositories;

public interface IGlobalSearchRepository {
	Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string? userId);
}

public class GlobalSearchRepository : IGlobalSearchRepository {
	private readonly DbContext _dbContext;

	public GlobalSearchRepository(DbContext dbContext) => _dbContext = dbContext;

	public async Task<GenericResponse<GlobalSearchDto>> Filter(GlobalSearchParams filter, string? userId) {
		GlobalSearchDto model = new();

		IEnumerable<CategoryEntity> categoryList = await _dbContext.Set<CategoryEntity>()
			.Include(x => x.Media)
			.Where(x => x.Title.Contains(filter.Title) && filter.Category && x.DeletedAt == null && (x.Title.Contains(filter.Query) ||
			                                                                                         x.Subtitle.Contains(filter.Query) ||
			                                                                                         x.TitleTr1.Contains(filter.Query) ||
			                                                                                         x.TitleTr2.Contains(filter.Query)))
			.OrderByDescending(x => x.CreatedAt).ToListAsync();

		IQueryable<UserEntity> userList = _dbContext.Set<UserEntity>().Where(x => x.UserName.Contains(filter.Title) && filter.User ||
		                                                                        x.FullName.Contains(filter.Title) && filter.User ||
		                                                                        x.AppUserName.Contains(filter.Title) && filter.User &&
		                                                                        (x.FullName.Contains(filter.Query) || x.AppUserName.Contains(filter.Query) ||
		                                                                         x.FirstName.Contains(filter.Query) || x.LastName.Contains(filter.Query)));

		userList = filter.Minimal
			? userList.OrderByDescending(x => x.CreatedAt)
			: userList.Include(u => u.Media).Include(u => u.Categories).Include(u => u.Products).OrderByDescending(x => x.CreatedAt);

		IQueryable<ProductEntity> productList = _dbContext.Set<ProductEntity>()
			.Where(x => x.Title.Contains(filter.Title) && filter.Product && x.DeletedAt == null && (x.Title.Contains(filter.Query) ||
			                                                                                        x.Subtitle.Contains(filter.Query) ||
			                                                                                        x.Description.Contains(filter.Query) ||
			                                                                                        x.Details.Contains(filter.Query)))
			.OrderByDescending(x => x.CreatedAt).AsNoTracking();

		if (filter.Minimal)
			productList = productList
				.Include(i => i.Media)
				.Include(i => i.Categories)
				.Include(i => i.User).ThenInclude(x => x.Media)
				.Include(i => i.User).ThenInclude(x => x.Categories)
				.Include(i => i.Bookmarks)
				.Where(x => x.DeletedAt == null);
		else
			productList =
				_dbContext.Set<ProductEntity>().Where(x => x.Title.Contains(filter.Title) && filter.Product && x.DeletedAt == null)
					.Include(i => i.Media)
					.Include(i => i.Categories)
					.Include(i => i.Comments.Where(x => x.ParentId == null)).ThenInclude(x => x.Children)
					.Include(i => i.Comments.Where(x => x.ParentId == null)).ThenInclude(x => x.Media)
					.Include(i => i.Reports)
					.Include(i => i.Votes)
					.Include(i => i.User).ThenInclude(x => x.Media)
					.Include(i => i.User).ThenInclude(x => x.Categories)
					.Include(i => i.Bookmarks)
					.Include(i => i.Forms)!.ThenInclude(x => x.FormField)
					.Include(i => i.Teams)!.ThenInclude(x => x.User)!.ThenInclude(x => x.Media)
					.Include(i => i.VoteFields)!.ThenInclude(x => x.Votes)
					.OrderByDescending(x => x.CreatedAt)
					.Where(x => x.DeletedAt == null);

		if (filter.Categories.IsNotNullOrEmpty()) {
			productList = productList.Where(x => x.Categories.Any(x => filter.Categories.Contains(x.Id) && x.DeletedAt == null));
			categoryList = categoryList.Where(x => filter.Categories.Contains(x.Id) && x.DeletedAt == null);
			userList = userList.Where(x => x.Categories.Any(x => filter.Categories.Contains(x.Id)) && x.DeletedAt == null);
		}

		if (filter.IsFollowing) {
			List<string?> userFollowing = await _dbContext.Set<FollowEntity>().Where(x => x.FollowerUserId == userId).Select(x => x.FollowsUserId).ToListAsync();

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
			userList = userList.Where(x => x.Id == userId);
		}

		model.Categories = categoryList;
		model.Users = userList;
		model.Products = productList;
		if (filter.IsBookmark) model.Products = model.Products.Where(x => x.IsBookmarked).ToList();
		return new GenericResponse<GlobalSearchDto>(model);
	}
}