namespace Utilities_aspnet.Repositories;

public interface IProductRepository {
	Task<GenericResponse> SeederProduct(SeederProductDto dto);
	Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto dto);
	Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadV2(ProductFilterDto dto);
	Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadMine();
	Task<GenericResponse<ProductReadDto>> ReadById(Guid id);
	Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto);
	Task<GenericResponse> Delete(Guid id);
}

public class ProductRepository : IProductRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public ProductRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse> SeederProduct(SeederProductDto dto) {
		if (dto == null || dto.Products.Count < 1) throw new ArgumentException("Dto must not be null", nameof(dto));

		try {
			foreach (ProductCreateUpdateDto? item in dto.Products) {
				ProductEntity entity = _mapper.Map<ProductEntity>(item);

				ProductEntity e = await entity.FillData(item, _httpContextAccessor, _context);
				await _context.Set<ProductEntity>().AddAsync(e);
				await _context.SaveChangesAsync();
			}
		}
		catch {
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}
		return new GenericResponse();
	}

	public async Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		ProductEntity entity = _mapper.Map<ProductEntity>(dto);

		ProductEntity e = await entity.FillData(dto, _httpContextAccessor, _context);
		EntityEntry<ProductEntity> i = await _context.Set<ProductEntity>().AddAsync(e);
		await _context.SaveChangesAsync();

		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
	}

	public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto dto) {
		List<ProductEntity> queryable;

		if (dto.Minimal ?? false)
			queryable = await _context.Set<ProductEntity>()
				.Include(i => i.Media)
				.Include(i => i.Categories)
				.Include(i => i.User).ThenInclude(x => x.Media)
				.Where(x => x.DeletedAt == null)
				.AsNoTracking()
				.ToListAsync();
		else
			queryable = await _context.Set<ProductEntity>()
				.Include(i => i.Media)
				.Include(i => i.Categories)
				.Include(i => i.Comments)!.ThenInclude(x => x.LikeComments)

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
				.Where(x => x.DeletedAt == null)
				.AsNoTracking()
				.ToListAsync();

		if (dto.Title.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Title ?? "").Contains(dto.Title)).ToList();
		if (dto.IsFollowing == true) {
			string? userId = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			List<string?> userFollowing = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == userId)
				.Select(x => x.FollowsUserId).ToListAsync();

			queryable = queryable.Where(x => userFollowing.Contains(x.UserId)).ToList();
		}

		if (dto.Subtitle.IsNotNullOrEmpty())
			queryable = queryable
				.Where(x => (x.Subtitle ?? "").Contains(dto.Subtitle)).ToList();

		if (dto.Type.IsNotNullOrEmpty())
			queryable = queryable
				.Where(x => (x.Type ?? "").Contains(dto.Type)).ToList();

		if (dto.Details.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Details ?? "").Contains(dto.Details))
				.ToList();

		if (dto.Description.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Description ?? "").Contains(dto.Description)).ToList();

		if (dto.StartPriceRange.HasValue)
			queryable = queryable.Where(x => x.Price >= dto.StartPriceRange.Value).ToList();

		if (dto.EndPriceRange.HasValue)
			queryable = queryable.Where(x => x.Price <= dto.EndPriceRange.Value).ToList();

		if (dto.Enabled == true)
			queryable = queryable.Where(x => x.Enabled == dto.Enabled).ToList();

		if (dto.IsForSale.HasValue)
			queryable = queryable.Where(x => x.IsForSale == dto.IsForSale).ToList();

		if (dto.IsBookmarked == true)
			queryable = queryable
				.Where(x => x.Bookmarks!.Any(y => y.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)).ToList();

		if (dto.VisitsCount.HasValue)
			queryable = queryable.Where(x => x.VisitsCount == dto.VisitsCount).ToList();

		if (dto.Length.HasValue)
			queryable = queryable.Where(x => x.Length == dto.Length).ToList();

		if (dto.Width.HasValue)
			queryable = queryable.Where(x => x.Width == dto.Width).ToList();

		if (dto.Height.HasValue)
			queryable = queryable.Where(x => x.Height == dto.Height).ToList();

		if (dto.Weight.HasValue)
			queryable = queryable.Where(x => x.Weight == dto.Weight).ToList();

		if (dto.MinOrder.HasValue)
			queryable = queryable.Where(x => x.MinOrder >= dto.MinOrder).ToList();

		if (dto.MaxOrder.HasValue)
			queryable = queryable.Where(x => x.MaxOrder <= dto.MaxOrder).ToList();

		if (dto.Unit.IsNotNullOrEmpty())
			queryable = queryable.Where(x => x.Unit == dto.Unit).ToList();

		if (dto.UseCase.IsNotNullOrEmpty())
			queryable = queryable.Where(x => x.UseCase == dto.UseCase).ToList();

		if (dto.Address.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Address ?? "").Contains(dto.Address)).ToList();

		if (dto.StartDate.HasValue)
			queryable = queryable.Where(x => x.StartDate >= dto.StartDate).ToList();

		if (dto.EndDate.HasValue)
			queryable = queryable.Where(x => x.EndDate <= dto.EndDate).ToList();

		if (dto.Author.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Author ?? "").Contains(dto.Author)).ToList();

		if (dto.Email.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.Email ?? "").Contains(dto.Email)).ToList();

		if (dto.PhoneNumber.IsNotNullOrEmpty())
			queryable = queryable.Where(x => (x.PhoneNumber ?? "").Contains(dto.PhoneNumber)).ToList();

		if (dto.Locations != null && dto.Locations.Any())
			queryable = queryable.Where(x => x.Locations != null &&
			                                 x.Locations.Any(y => dto.Locations.Contains(y.Id))).ToList();

		if (dto.Categories != null && dto.Categories.Any())
			queryable = queryable.Where(x => x.Categories != null &&
			                                 x.Categories.Any(y => dto.Categories.Contains(y.Id))).ToList();

		int totalCount = queryable.Count;

		if (dto.FilterOrder.HasValue)
			queryable = dto.FilterOrder switch {
				ProductFilterOrder.LowPrice => queryable.OrderBy(x => x.Price).ToList(),
				ProductFilterOrder.HighPrice => queryable.OrderByDescending(x => x.Price).ToList(),
				ProductFilterOrder.AToZ => queryable.OrderBy(x => x.Title).ToList(),
				ProductFilterOrder.ZToA => queryable.OrderByDescending(x => x.Title).ToList(),
				_ => queryable.OrderBy(x => x.CreatedAt).ToList()
			};

		queryable = queryable.Skip((dto.PageNumber - 1) * dto.PageSize)
			.Take(dto.PageSize)
			.ToList();

		IEnumerable<ProductReadDto> readDto = _mapper.Map<IEnumerable<ProductReadDto>>(queryable).ToList();

		if (_httpContextAccessor?.HttpContext?.User.Identity == null)
			return new GenericResponse<IEnumerable<ProductReadDto>>(readDto) {
				TotalCount = totalCount,
				PageCount = totalCount % dto.PageSize == 0
					? totalCount / dto?.PageSize
					: totalCount / dto?.PageSize + 1,
				PageSize = dto?.PageSize
			};

		IEnumerable<BookmarkEntity> bookmark = _context.Set<BookmarkEntity>()
			.AsNoTracking()
			.Where(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
			.ToList();

		foreach (ProductReadDto productReadDto in readDto)
		foreach (BookmarkEntity bookmarkEntity in bookmark)
			if (bookmarkEntity.ProductId == productReadDto.Id)
				productReadDto.IsBookmarked = true;

		return new GenericResponse<IEnumerable<ProductReadDto>>(readDto) {
			TotalCount = totalCount,
			PageCount = totalCount % dto?.PageSize == 0
				? totalCount / dto?.PageSize
				: totalCount / dto?.PageSize + 1,
			PageSize = dto?.PageSize
		};
	}

	public async Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadV2(ProductFilterDto dto) {
		IIncludableQueryable<ProductEntity, object?> dbSet = _context.Set<ProductEntity>().Include(i => i.Media);

		if (dto.ShowCategories.IsTrue()) dbSet = dbSet.Include(i => i.Categories);
		if (dto.ShowComments.IsTrue())
			dbSet = dbSet.Include(i => i.Comments);
		if (dto.ShowLocation.IsTrue()) dbSet = dbSet.Include(i => i.Locations);
		if (dto.ShowForms.IsTrue()) dbSet = dbSet.Include(i => i.Forms);
		if (dto.ShowMedia.IsTrue()) dbSet = dbSet.Include(i => i.Media);
		if (dto.ShowReports.IsTrue()) dbSet = dbSet.Include(i => i.Reports);
		if (dto.ShowTeams.IsTrue()) dbSet = dbSet.Include(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x.Media);
		if (dto.ShowVotes.IsTrue()) dbSet = dbSet.Include(i => i.Votes);
		if (dto.ShowVoteFields.IsTrue()) dbSet = dbSet.Include(i => i.VoteFields);
		if (dto.ShowCreator.IsTrue()) dbSet = dbSet.Include(i => i.User).ThenInclude(x => x!.Media);

		IQueryable<ProductEntity> q = dbSet.Where(x => x.DeletedAt == null);

		if (dto.IsFollowing == true) {
			string? userId = _httpContextAccessor?.HttpContext?.User.Identity?.Name;
			List<string?> userFollowing = await _context.Set<FollowEntity>().Where(x => x.FollowerUserId == userId)
				.Select(x => x.FollowsUserId).ToListAsync();

			q = q.Where(x => userFollowing.Contains(x.UserId));
		}
		if (dto.IsBookmarked == true)
			q = q.Where(x => x.Bookmarks!.Any(y => y.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!));

		if (dto.Title.IsNotNullOrEmpty()) q = q.Where(x => (x.Title ?? "").Contains(dto.Title!));
		if (dto.Subtitle.IsNotNullOrEmpty()) q = q.Where(x => (x.Subtitle ?? "").Contains(dto.Subtitle!));
		if (dto.Type.IsNotNullOrEmpty()) q = q.Where(x => (x.Type ?? "").Contains(dto.Type!));
		if (dto.Details.IsNotNullOrEmpty()) q = q.Where(x => (x.Details ?? "").Contains(dto.Details!));
		if (dto.Description.IsNotNullOrEmpty()) q = q.Where(x => (x.Description ?? "").Contains(dto.Description!));
		if (dto.Author.IsNotNullOrEmpty()) q = q.Where(x => (x.Author ?? "").Contains(dto.Author!));
		if (dto.Email.IsNotNullOrEmpty()) q = q.Where(x => (x.Email ?? "").Contains(dto.Email!));
		if (dto.PhoneNumber.IsNotNullOrEmpty()) q = q.Where(x => (x.PhoneNumber ?? "").Contains(dto.PhoneNumber!));
		if (dto.Address.IsNotNullOrEmpty()) q = q.Where(x => (x.Address ?? "").Contains(dto.Address!));
		if (dto.Unit.IsNotNullOrEmpty()) q = q.Where(x => x.Unit == dto.Unit);
		if (dto.UseCase.IsNotNullOrEmpty()) q = q.Where(x => x.UseCase == dto.UseCase);
		if (dto.StartPriceRange.HasValue) q = q.Where(x => x.Price >= dto.StartPriceRange.Value);
		if (dto.EndPriceRange.HasValue) q = q.Where(x => x.Price <= dto.EndPriceRange.Value);
		if (dto.Enabled.HasValue) q = q.Where(x => x.Enabled == dto.Enabled);
		if (dto.IsForSale.HasValue) q = q.Where(x => x.IsForSale == dto.IsForSale);
		if (dto.VisitsCount.HasValue) q = q.Where(x => x.VisitsCount == dto.VisitsCount);
		if (dto.Length.HasValue) q = q.Where(x => x.Length == dto.Length);
		if (dto.Width.HasValue) q = q.Where(x => x.Width == dto.Width);
		if (dto.Height.HasValue) q = q.Where(x => x.Height == dto.Height);
		if (dto.Weight.HasValue) q = q.Where(x => x.Weight == dto.Weight);
		if (dto.MinOrder.HasValue) q = q.Where(x => x.MinOrder >= dto.MinOrder);
		if (dto.MaxOrder.HasValue) q = q.Where(x => x.MaxOrder <= dto.MaxOrder);
		if (dto.StartDate.HasValue) q = q.Where(x => x.StartDate >= dto.StartDate);
		if (dto.EndDate.HasValue) q = q.Where(x => x.EndDate <= dto.EndDate);

		if (dto.Locations != null && dto.Locations.Any())
			q = q.Where(x => x.Locations != null &&
			                 x.Locations.Any(y => dto.Locations.Contains(y.Id)));

		if (dto.Categories != null && dto.Categories.Any())
			q = q.Where(x => x.Categories != null &&
			                 x.Categories.Any(y => dto.Categories.Contains(y.Id)));

		int totalCount = q.Count();

		if (dto.FilterOrder.HasValue)
			q = dto.FilterOrder switch {
				ProductFilterOrder.LowPrice => q.OrderBy(x => x.Price),
				ProductFilterOrder.HighPrice => q.OrderByDescending(x => x.Price),
				ProductFilterOrder.AToZ => q.OrderBy(x => x.Title),
				ProductFilterOrder.ZToA => q.OrderByDescending(x => x.Title),
				_ => q.OrderBy(x => x.CreatedAt)
			};

		q = q.Skip((dto.PageNumber - 1) * dto.PageSize)
			.Take(dto.PageSize);

		IEnumerable<ProductReadDto> readDto = _mapper.Map<IEnumerable<ProductReadDto>>(q).ToList();

		if (_httpContextAccessor?.HttpContext?.User.Identity == null)
			return new GenericResponse<IEnumerable<ProductReadDto>>(readDto) {
				TotalCount = totalCount,
				PageCount = totalCount % dto?.PageSize == 0
					? totalCount / dto?.PageSize
					: totalCount / dto?.PageSize + 1,
				PageSize = dto?.PageSize
			};

		IEnumerable<BookmarkEntity> bookmark = _context.Set<BookmarkEntity>()
			.AsNoTracking()
			.Where(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
			.ToList();

		foreach (ProductReadDto productReadDto in readDto)
		foreach (BookmarkEntity bookmarkEntity in bookmark)
			if (bookmarkEntity.ProductId == productReadDto.Id)
				productReadDto.IsBookmarked = true;

		return new GenericResponse<IEnumerable<ProductReadDto>>(readDto) {
			TotalCount = totalCount,
			PageCount = totalCount % dto?.PageSize == 0
				? totalCount / dto?.PageSize
				: totalCount / dto?.PageSize + 1,
			PageSize = dto?.PageSize
		};
	}

	public async Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadMine() {
		IEnumerable<ProductEntity> products = await _context.Set<ProductEntity>()
			.AsNoTracking()
			.Include(i => i.Media)
			.Include(i => i.Categories)
			.Include(i => i.Comments)!.ThenInclude(x => x.LikeComments)
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
			.Where(x => x.DeletedAt == null && x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
			.ToListAsync();
		IEnumerable<ProductReadDto> i = _mapper.Map<IEnumerable<ProductReadDto>>(products).ToList();
		return new GenericResponse<IEnumerable<ProductReadDto>>(i);
	}

	public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id) {
		ProductEntity? i = await _context.Set<ProductEntity>().AsNoTracking()
			.Include(i => i.Media)
			.Include(i => i.Categories)
			.Include(i => i.Locations)
			.Include(i => i.Reports)
			.Include(i => i.Comments)!.ThenInclude(x => x.LikeComments)
			.Include(i => i.Bookmarks)
			.Include(i => i.Votes)
			.Include(i => i.User)!.ThenInclude(x => x.Media)
			.Include(i => i.User)!.ThenInclude(x => x.Categories)
			.Include(i => i.Forms)!.ThenInclude(x => x.FormField)
			.Include(i => i.Teams)!.ThenInclude(x => x.User)!.ThenInclude(x => x.Media)
			.Include(i => i.VoteFields)!.ThenInclude(x => x.Votes)
			.FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
	}

	public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto) {
		ProductEntity? entity = await _context.Set<ProductEntity>().Include(x => x.Categories).Include(x => x.Locations)
			.Include(x => x.Teams).Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

		if (entity == null)
			return new GenericResponse<ProductReadDto>(new ProductReadDto());

		ProductEntity e = await entity.FillData(dto, _httpContextAccessor, _context);
		_context.Update(e);
		await _context.SaveChangesAsync();

		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(e));
	}

	public async Task<GenericResponse> Delete(Guid id) {
		ProductEntity? i = await _context.Set<ProductEntity>()
			.FirstOrDefaultAsync(i => i.Id == id);
		if(i != null)
        {
			i.DeletedAt = DateTime.Now;
			await _context.SaveChangesAsync();
        }
        else
        {
			return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");
		}

		return new GenericResponse();
	}
}

public static class ProductEntityExtension {
	public static async Task<ProductEntity> FillData(
		this ProductEntity entity,
		ProductCreateUpdateDto dto,
		IHttpContextAccessor httpContextAccessor,
		DbContext context) {
		entity.UserId = httpContextAccessor.HttpContext?.User.Identity?.Name;
		entity.Title = dto.Title ?? entity.Title;
		entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
		entity.Details = dto.Details ?? entity.Details;
		entity.Author = dto.Author ?? entity.Author;
		entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
		entity.Link = dto.Link ?? entity.Link;
		entity.Website = dto.Website ?? entity.Website;
		entity.Email = dto.Email ?? entity.Email;
		entity.Latitude = dto.Latitude ?? entity.Latitude;
		entity.Longitude = dto.Longitude ?? entity.Longitude;
		entity.Description = dto.Description ?? entity.Description;
		entity.UseCase = dto.UseCase ?? entity.UseCase;
		entity.Price = dto.Price ?? entity.Price;
		entity.IsForSale = dto.IsForSale ?? entity.IsForSale;
		entity.Enabled = dto.Enabled ?? entity.Enabled;
		entity.VisitsCount = dto.VisitsCount ?? entity.VisitsCount;
		entity.Length = dto.Length ?? entity.Length;
		entity.Width = dto.Width ?? entity.Width;
		entity.Height = dto.Height ?? entity.Height;
		entity.Weight = dto.Weight ?? entity.Weight;
		entity.MinOrder = dto.MinOrder ?? entity.MinOrder;
		entity.MaxOrder = dto.MaxOrder ?? entity.MaxOrder;
		entity.Unit = dto.Unit ?? entity.Unit;
		entity.Address = dto.Address ?? entity.Address;
		entity.StartDate = dto.StartDate ?? entity.StartDate;
		entity.EndDate = dto.EndDate ?? entity.EndDate;
		entity.Status = dto.Status ?? entity.Status;

		if (dto.Categories.IsNotNullOrEmpty()) {
			List<CategoryEntity> listCategory = new();
			foreach (Guid item in dto.Categories ?? new List<Guid>()) {
				CategoryEntity? e = await context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item);
				if (e != null) listCategory.Add(e);
			}
			entity.Categories = listCategory;
		}

		if (dto.Locations.IsNotNullOrEmpty()) {
			List<LocationEntity> listLocation = new();
			foreach (int item in dto.Locations ?? new List<int>()) {
				LocationEntity? e = await context.Set<LocationEntity>().FirstOrDefaultAsync(x => x.Id == item);
				if (e != null) listLocation.Add(e);
			}
			entity.Locations = listLocation;
		}

		if (dto.Teams.IsNotNullOrEmpty()) {
			List<TeamEntity> listTeam = new();
			foreach (string item in dto.Teams ?? new List<string>()) {
				UserEntity? e = await context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == item);
				if (e != null) {
					TeamEntity t = new() {UserId = e.Id};
					await context.Set<TeamEntity>().AddAsync(t);
					listTeam.Add(t);
				}
			}
			entity.Teams = listTeam;
		}

		return entity;
	}
}