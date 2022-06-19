namespace Utilities_aspnet.Repositories;

public interface IProductRepository {
	Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto dto);
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

	public async Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		ProductEntity entity = _mapper.Map<ProductEntity>(dto);

		ProductEntity e = await entity.FillData(dto, _httpContextAccessor, _context);
		EntityEntry<ProductEntity> i = await _context.Set<ProductEntity>().AddAsync(e);
		await _context.SaveChangesAsync();

		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
	}

	public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto dto) {
		List<ProductEntity> queryable = await _context.Set<ProductEntity>()
			.AsNoTracking()
			.Include(i => i.Media)
			.Include(i => i.Categories)
			.Include(i => i.Comments)
			.Include(i => i.Locations)
			.Include(i => i.Reports)
			.Include(i => i.User)
			.Include(i => i.Bookmarks)
			.Include(i => i.Forms)!
			.ThenInclude(x => x.FormField)
			.Where(x => x.DeletedAt == null)
			.ToListAsync();

		if (!string.IsNullOrEmpty(dto.Title))
			queryable = queryable.Where(x => (x.Title ?? "").Contains(dto.Title))
				.ToList();

		if (!string.IsNullOrEmpty(dto.SubTitle))
			queryable = queryable
				.Where(x => (x.Subtitle ?? "").Contains(dto.SubTitle)).ToList();

		if (!string.IsNullOrEmpty(dto.Type))
			queryable = queryable
				.Where(x => (x.Type ?? "").Contains(dto.Type)).ToList();

		if (!string.IsNullOrEmpty(dto.Details))
			queryable = queryable.Where(x => (x.Details ?? "").Contains(dto.Details))
				.ToList();

		if (!string.IsNullOrEmpty(dto.Description))
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
			queryable = queryable.Where(x => x.VisitCount == dto.VisitsCount).ToList();

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

		if (!string.IsNullOrEmpty(dto.Address))
			queryable = queryable
				.Where(x => (x.Address ?? "").Contains(dto.Address)).ToList();

		if (dto.StartDate.HasValue)
			queryable = queryable.Where(x => x.StartDate >= dto.StartDate).ToList();

		if (dto.EndDate.HasValue)
			queryable = queryable.Where(x => x.EndDate <= dto.EndDate).ToList();

		if (!string.IsNullOrEmpty(dto.Author))
			queryable = queryable
				.Where(x => (x.Author ?? "").Contains(dto.Author)).ToList();

		if (!string.IsNullOrEmpty(dto.Email))
			queryable = queryable
				.Where(x => (x.Email ?? "").Contains(dto.Email)).ToList();

		if (!string.IsNullOrEmpty(dto.PhoneNumber))
			queryable = queryable
				.Where(x => (x.PhoneNumber ?? "").Contains(dto.PhoneNumber)).ToList();

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
		//GenericResponse<IEnumerable<ProductReadDto>> e = await Read(null);
		IEnumerable<ProductEntity> products = await _context.Set<ProductEntity>()
			.AsNoTracking()
			.Include(i => i.Media)
			.Include(i => i.Categories)
			.Include(i => i.Comments)
			.Include(i => i.Locations)
			.Include(i => i.Reports)
			.Include(i => i.User)
			.Include(i => i.Bookmarks)
			.Include(i => i.Forms)!
			.ThenInclude(x => x.FormField)
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
			.Include(i => i.Comments)
			.Include(i => i.Bookmarks)
			.Include(i => i.User)
			.Include(i => i.Forms)!.ThenInclude(x => x.FormField)
			.FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
	}

	public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto) {
		ProductEntity? entity = await _context.Set<ProductEntity>().Where(x => x.Id == dto.Id)
			.Include(x => x.Categories).Include(x => x.Locations).Include(x => x.Forms).Include(x => x.VoteFields).Include(x=>x.Reports).FirstOrDefaultAsync();

		if (entity == null)
			return new GenericResponse<ProductReadDto>(new ProductReadDto());

		ProductEntity e = await entity.FillData(dto, _httpContextAccessor, _context);
		_context.Update(e);
		await _context.SaveChangesAsync();

		return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(e));
	}

	public async Task<GenericResponse> Delete(Guid id) {
		ProductEntity? i = await _context.Set<ProductEntity>().AsNoTracking()
			.FirstOrDefaultAsync(i => i.Id == id);
		i.DeletedAt = DateTime.Now;
		await _context.SaveChangesAsync();
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
		entity.VisitCount = dto.VisitsCount ?? entity.VisitCount;
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

		List<CategoryEntity> listCategory = new();
		List<LocationEntity> listLocation = new();
		//List<VoteFieldEntity> listVoteFields = new();

		foreach (Guid item in dto.Categories ?? new List<Guid>()) {
			CategoryEntity? e = await context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item);
			if (e != null) listCategory.Add(e);
		}
		
		foreach (int item in dto.Locations ?? new List<int>()) {
			LocationEntity? e = await context.Set<LocationEntity>().FirstOrDefaultAsync(x => x.Id == item);
			if (e != null) listLocation.Add(e);
		}

		//foreach (Guid item in dto.VoteFields ?? new List<Guid>()) {
		//	VoteFieldEntity? e = await context.Set<VoteFieldEntity>().FirstOrDefaultAsync(x => x.Id == item);
		//	if (e != null) listVoteFields.Add(e);
		//}
		
		entity.Categories = listCategory;
		entity.Locations = listLocation;
		//entity.VoteFields = listVoteFields;

		return entity;
	}
}