namespace Utilities_aspnet.Product;

public interface IProductRepository<T> where T : BaseProductEntity {
    Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? paraneters);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadMine();
    Task<GenericResponse<ProductReadDto>> ReadById(Guid id);
    Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ProductRepository<T> : IProductRepository<T> where T : BaseProductEntity, new() {
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
        T entity = _mapper.Map<T>(dto);

        FillProductDetail(entity, dto);
        EntityEntry<T> i = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? parameters) {
        List<T>? queryable = await _context.Set<T>()
            .AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
            .Include(i=> i.Comments)
            .Include(i => i.Locations)
            .Include(i => i.Reports)
            .Include(i => i.Specialities)
            .Include(i => i.Tags)
            .Include(i => i.Brands)
            .Include(i => i.References)
            .Include(i => i.User)
            .Include(i => i.Bookmarks)
            .Include(i => i.Forms)!
            .ThenInclude(x => x.FormField)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();

        int totalCount = queryable.Count;

        if (parameters != null) {
            if (!string.IsNullOrEmpty(parameters.Title))
                queryable = queryable.Where(x => !string.IsNullOrEmpty(x.Title) && x.Title.Contains(parameters.Title))
                    .ToList();

            if (!string.IsNullOrEmpty(parameters.SubTitle))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Subtitle) && x.Subtitle.Contains(parameters.SubTitle)).ToList();

            if (!string.IsNullOrEmpty(parameters.Type))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Type) && x.Type.Contains(parameters.Type)).ToList();

            if (!string.IsNullOrEmpty(parameters.Details))
                queryable = queryable.Where(x => !string.IsNullOrWhiteSpace(x.Details) && x.Details.Contains(parameters.Details))
                    .ToList();

            if (!string.IsNullOrEmpty(parameters.Description))
                queryable = queryable.Where(x =>
                    !string.IsNullOrEmpty(x.Description) && x.Description.Contains(parameters.Description)).ToList();

            if (parameters.StartPriceRange.HasValue)
                queryable = queryable.Where(x => x.Price >= parameters.StartPriceRange.Value).ToList();

            if (parameters.EndPriceRange.HasValue)
                queryable = queryable.Where(x => x.Price <= parameters.EndPriceRange.Value).ToList();

            if (parameters.Enabled == true)
                queryable = queryable.Where(x => x.Enabled == parameters.Enabled).ToList();

            if (parameters.IsForSale.HasValue)
                queryable = queryable.Where(x => x.IsForSale == parameters.IsForSale).ToList();

            if (parameters.IsBookmarked == true)
                queryable = queryable.Where(x =>
                    x.Bookmarks!.Any(y => y.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)).ToList();

            if (parameters.VisitsCount.HasValue)
                queryable = queryable.Where(x => x.VisitCount == parameters.VisitsCount).ToList();

            if (!string.IsNullOrEmpty(parameters.Address))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Address) && x.Address.Contains(parameters.Address)).ToList();

            if (parameters.StartDate.HasValue)
                queryable = queryable.Where(x => x.StartDate >= parameters.StartDate).ToList();

            if (parameters.EndDate.HasValue)
                queryable = queryable.Where(x => x.EndDate <= parameters.EndDate).ToList();

            if (!string.IsNullOrEmpty(parameters.Author))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Author) && x.Author.Contains(parameters.Author)).ToList();

            if (!string.IsNullOrEmpty(parameters.Email))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.Contains(parameters.Email)).ToList();

            if (!string.IsNullOrEmpty(parameters.PhoneNumber))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber.Contains(parameters.PhoneNumber)).ToList();

            if (parameters.Locations != null && parameters.Locations.Any())
                queryable = queryable.Where(x => x.Locations != null &&
                                                 x.Locations.Any(y => parameters.Locations.Contains(y.Id))).ToList();

            if (parameters.Brands != null && parameters.Brands.Any())
                queryable = queryable.Where(x => x.Brands != null &&
                                                 x.Brands.Any(y => parameters.Brands.Contains(y.Id))).ToList();

            if (parameters.Categories != null && parameters.Categories.Any())
                queryable = queryable.Where(x => x.Categories != null &&
                                                 x.Categories.Any(y => parameters.Categories.Contains(y.Id))).ToList();

            if (parameters.References != null && parameters.References.Any())
                queryable = queryable.Where(x => x.References != null &&
                                                 x.References.Any(y => parameters.References.Contains(y.Id))).ToList();

            if (parameters.Tags != null && parameters.Tags.Any())
                queryable = queryable.Where(x => x.Tags != null &&
                                                 x.Tags.Any(y => parameters.Tags.Contains(y.Id))).ToList();

            if (parameters.Specialities != null && parameters.Specialities.Any())
                queryable = queryable.Where(x => x.Specialities != null &&
                                                 x.Specialities.Any(y => parameters.Specialities.Contains(y.Id)))
                    .ToList();

            totalCount = queryable.Count;

            if (parameters.FilterOrder.HasValue) {
                queryable = parameters.FilterOrder switch {
                    ProductFilterOrder.LowPrice => queryable.OrderBy(x => x.Price).ToList(),
                    ProductFilterOrder.HighPrice => queryable.OrderByDescending(x => x.Price).ToList(),
                    ProductFilterOrder.AToZ => queryable.OrderBy(x => x.Title).ToList(),
                    ProductFilterOrder.ZToA => queryable.OrderByDescending(x => x.Title).ToList(),
                    _ => queryable.OrderBy(x => x.CreatedAt).ToList()
                };
            }

            queryable = queryable.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
        }

        IEnumerable<ProductReadDto>? dto = _mapper.Map<IEnumerable<ProductReadDto>>(queryable).ToList();

        if (_httpContextAccessor?.HttpContext?.User.Identity == null)
            return new GenericResponse<IEnumerable<ProductReadDto>>(dto) {
                TotalCount = totalCount,
                PageCount = totalCount % parameters?.PageSize == 0
                    ? totalCount / parameters?.PageSize
                    : totalCount / parameters?.PageSize + 1,
                PageSize = parameters?.PageSize
            };

        IEnumerable<BookmarkEntity> bookmark = _context.Set<BookmarkEntity>()
            .AsNoTracking()
            .Where(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
            .ToList();

        foreach (ProductReadDto productReadDto in dto)
        foreach (BookmarkEntity bookmarkEntity in bookmark) {
            if (bookmarkEntity.AdId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.ProductId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.ProjectId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.CompanyId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.EventId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.MagazineId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.TenderId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.TutorialId == productReadDto.Id) productReadDto.IsBookmarked = true;
            if (bookmarkEntity.ServiceId == productReadDto.Id) productReadDto.IsBookmarked = true;
        }

        return new GenericResponse<IEnumerable<ProductReadDto>>(dto) {
            TotalCount = totalCount,
            PageCount = totalCount % parameters?.PageSize == 0
                ? totalCount / parameters?.PageSize
                : totalCount / parameters?.PageSize + 1,
            PageSize = parameters?.PageSize
        };
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> ReadMine() {
        GenericResponse<IEnumerable<ProductReadDto>> e = await Read(null);
        IEnumerable<ProductReadDto> i = e.Result.Where(i => i.UserId == _httpContextAccessor.HttpContext.User.Identity.Name);
        return new GenericResponse<IEnumerable<ProductReadDto>>(i);
    }

    public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id) {
        T? i = await _context.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
            .Include(i => i.Locations)
            .Include(i => i.Reports)
            .Include(i => i.Specialities)
            .Include(i => i.Tags)
            .Include(i => i.Brands)
            .Include(i => i.References)
            .Include(i => i.Comments)
            .Include(i => i.User)
            .Include(i => i.Forms)!.ThenInclude(x => x.FormField)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
    }

    public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto) {
        T? entity = await _context.Set<T>().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

        if (entity == null)
            return new GenericResponse<ProductReadDto>(new ProductReadDto());

        FillProductDetail(entity, dto);
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(entity));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        T? i = await _context.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        i.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return new GenericResponse();
    }

    private async void FillProductDetail(T entity, ProductCreateUpdateDto dto) {
        entity.UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        entity.Title = dto.Title ?? entity.Title;
        entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
        entity.Details = dto.Details ?? entity.Details;
        entity.Author = dto.Author ?? entity.Author;
        entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
        entity.Email = dto.Email ?? entity.Email;
        entity.Latitude = dto.Latitude ?? entity.Latitude;
        entity.Longitude = dto.Longitude ?? entity.Longitude;
        entity.Description = dto.Description ?? entity.Description;
        entity.Price = dto.Price ?? entity.Price;
        entity.IsForSale = dto.IsForSale ?? entity.IsForSale;
        entity.Enabled = dto.Enabled ?? entity.Enabled;
        entity.VisitCount = dto.VisitsCount ?? entity.VisitCount;
        entity.Address = dto.Address ?? entity.Address;
        entity.StartDate = dto.StartDate ?? entity.StartDate;
        entity.EndDate = dto.EndDate ?? entity.EndDate;

        if (dto.Brands.IsNotNullOrEmpty()) {
            List<BrandEntity> list = new();
            foreach (Guid item in dto.Brands ?? new List<Guid>()) {
                BrandEntity? e = await _context.Set<BrandEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Brands = list;
        }

        if (dto.Categories.IsNotNullOrEmpty()) {
            List<CategoryEntity> list = new();
            foreach (Guid item in dto.Categories ?? new List<Guid>()) {
                CategoryEntity? e = await _context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Categories = list;
        }

        if (dto.Favorites.IsNotNullOrEmpty()) {
            List<FavoriteEntity> list = new();
            foreach (Guid item in dto.Favorites ?? new List<Guid>()) {
                FavoriteEntity? e = await _context.Set<FavoriteEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Favorites = list;
        }

        if (dto.Locations.IsNotNullOrEmpty()) {
            List<LocationEntity> list = new();
            foreach (int item in dto.Locations ?? new List<int>()) {
                LocationEntity? e = await _context.Set<LocationEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Locations = list;
        }

        if (dto.References.IsNotNullOrEmpty()) {
            List<ReferenceEntity> list = new();
            foreach (Guid item in dto.References ?? new List<Guid>()) {
                ReferenceEntity? e = await _context.Set<ReferenceEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.References = list;
        }

        if (dto.Specialties.IsNotNullOrEmpty()) {
            List<SpecialityEntity> list = new();
            foreach (Guid item in dto.Specialties ?? new List<Guid>()) {
                SpecialityEntity? e = await _context.Set<SpecialityEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Specialities = list;
        }

        if (dto.Forms.IsNotNullOrEmpty()) {
            List<FormEntity> list = new();
            foreach (Guid item in dto.Forms ?? new List<Guid>()) {
                FormEntity? e = await _context.Set<FormEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Forms = list;
        }

        if (dto.Reports.IsNotNullOrEmpty()) {
            List<ReportEntity> list = new();
            foreach (Guid item in dto.Reports ?? new List<Guid>()) {
                ReportEntity? e = await _context.Set<ReportEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.Reports = list;
        }

        if (dto.VoteFields.IsNotNullOrEmpty()) {
            List<VoteFieldEntity> list = new();
            foreach (Guid item in dto.VoteFields ?? new List<Guid>()) {
                VoteFieldEntity? e = await _context.Set<VoteFieldEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) list.Add(e);
            }

            entity.VoteFields = list;
        }
    }
}