using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace Utilities_aspnet.Product;

public interface IProductRepository<T> where T : BaseProductEntity
{
    Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? paraneters);
    Task<GenericResponse<ProductReadDto>> ReadById(Guid id);
    Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ProductRepository<T> : IProductRepository<T> where T : BaseProductEntity, new()
{
    private readonly DbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IIdentity? _user;

    public ProductRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _user = _httpContextAccessor?.HttpContext?.User.Identity;
    }

    public async Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto)
    {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);

        entity.UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<ReferenceEntity> references = new();
        List<BrandEntity> brands = new();
        List<CategoryEntity> categories = new();
        List<LocationEntity> locations = new();
        List<SpecialityEntity> specialities = new();
        List<TagEntity> tags = new();
        List<FormEntity> forms = new();

        foreach (Guid item in dto.References ?? new List<Guid>())
        {
            ReferenceEntity? e = await _context.Set<ReferenceEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (e != null) references.Add(e);
        }

        foreach (Guid item in dto.Brands ?? new List<Guid>())
        {
            BrandEntity? e = await _context.Set<BrandEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (e != null) brands.Add(e);
        }

        foreach (Guid item in dto.Categories ?? new List<Guid>())
        {
            CategoryEntity? category = await _context.Set<CategoryEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (category != null) categories.Add(category);
        }

        foreach (int item in dto.Locations ?? new List<int>())
        {
            LocationEntity? location = await _context.Set<LocationEntity>().Include(x => x.Project)
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (location != null) locations.Add(location);
        }

        foreach (Guid item in dto.Specialties ?? new List<Guid>())
        {
            SpecialityEntity? speciality = await _context.Set<SpecialityEntity>().Include(x => x.Project)
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (speciality != null) specialities.Add(speciality);
        }

        foreach (Guid item in dto.Tags ?? new List<Guid>())
        {
            TagEntity? tag = await _context.Set<TagEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (tag != null) tags.Add(tag);
        }

        entity.Categories = categories;
        entity.Brands = brands;
        entity.References = references;
        entity.Locations = locations;
        entity.Specialities = specialities;
        entity.Tags = tags;
        EntityEntry<T> i = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? parameters)
    {
        List<T>? queryable = await _context.Set<T>()
            .AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
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


        if (parameters != null)
        {
            if (!string.IsNullOrEmpty(parameters.Title))
                queryable = queryable.Where(x => !string.IsNullOrEmpty(x.Title) && x.Title.Contains(parameters.Title))
                    .ToList();

            if (!string.IsNullOrEmpty(parameters.SubTitle))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Subtitle) && x.Subtitle.Contains(parameters.SubTitle)).ToList();

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

            if (parameters.EndPriceRange.HasValue)
                queryable = queryable.Where(x => x.EndDate <= parameters.EndDate).ToList();

            if (!string.IsNullOrEmpty(parameters.Author))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Author) && x.Author.Contains(parameters.Author)).ToList();

            if(!string.IsNullOrEmpty(parameters.Email))
                queryable = queryable
                    .Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.Contains(parameters.Email)).ToList();

            if(!string.IsNullOrEmpty(parameters.PhoneNumber))
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

            if (parameters.FilterOrder.HasValue)
            {
                queryable = parameters.FilterOrder switch
                {
                    ProductFilterOrder.LowPrice => queryable.OrderBy(x => x.Price).ToList(),
                    ProductFilterOrder.HighPrice => queryable.OrderByDescending(x => x.Price).ToList(),
                    ProductFilterOrder.AToZ => queryable.OrderBy(x => x.Title).ToList(),
                    ProductFilterOrder.ZToA => queryable.OrderByDescending(x => x.Title).ToList(),
                    _ => queryable.OrderBy(x => x.CreatedAt).ToList()
                };
            }

            queryable = queryable.Skip((parameters.PageSize - 1) * parameters.PageNumber)
                .Take(parameters.PageNumber)
                .ToList();
        }

        IEnumerable<ProductReadDto>? dto = _mapper.Map<IEnumerable<ProductReadDto>>(queryable).ToList();

        // ReSharper disable once InvertIf
        if (_user != null)
        {
            IEnumerable<BookmarkEntity> bookmark = _context.Set<BookmarkEntity>()
                .AsNoTracking()
                .Where(x => x.UserId == _user.Name)
                .ToList();

            foreach (ProductReadDto productReadDto in dto)
                foreach (BookmarkEntity bookmarkEntity in bookmark)
                {
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
        }


        return new GenericResponse<IEnumerable<ProductReadDto>>(dto);
    }

    public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id)
    {
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

    public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto parameters)
    {
        T? entity = await _context.Set<T>()
            .AsNoTracking()
            .Include(x => x.Locations)
            .Include(x => x.Favorites)
            .Include(x => x.Media)
            .Include(x => x.Forms)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Include(x => x.VoteFields)
            .Include(x => x.Reports)
            .Include(x => x.Specialities)
            .Include(x => x.Brands)
            .Include(x => x.References)
            .Include(x => x.ContactInformations)
            .Where(x => x.Id == parameters.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            return new GenericResponse<ProductReadDto>(new ProductReadDto());

        entity.Title ??= parameters.Title;
        entity.Subtitle ??= parameters.Subtitle;
        entity.Details ??= parameters.Details;
        entity.Author ??= parameters.Author;
        entity.PhoneNumber ??= parameters.PhoneNumber;
        entity.Email ??= parameters.Email;
        entity.Latitude ??= parameters.Latitude;
        entity.Longitude ??= parameters.Longitude;
        entity.Description ??= parameters.Description;
        entity.Price ??= parameters.Price;
        entity.IsForSale ??= parameters.IsForSale;
        entity.Enabled ??= parameters.Enabled;
        entity.VisitCount ??= parameters.VisitsCount;
        entity.Address ??= parameters.Address;
        entity.StartDate ??= parameters.StartDate;
        entity.EndDate ??= parameters.EndDate;

        if (parameters.Locations != null && parameters.Locations.Any())
        {
            List<LocationEntity> locations = await _context.Set<LocationEntity>()
                .Where(x => parameters.Locations.Contains(x.Id))
                .ToListAsync();

            entity.Locations = locations;
        }

        if (parameters.Favorites != null && parameters.Favorites.Any())
        {
            List<FavoriteEntity> favorites = await _context.Set<FavoriteEntity>()
                .Where(x => parameters.Favorites.Contains(x.Id))
                .ToListAsync();

            entity.Favorites = favorites;
        }

        if (parameters.Categories != null && parameters.Categories.Any())
        {
            List<CategoryEntity> categories = await _context.Set<CategoryEntity>()
                .Where(x => parameters.Categories.Contains(x.Id))
                .ToListAsync();

            entity.Categories = categories;
        }

        if (parameters.References != null && parameters.References.Any())
        {
            List<ReferenceEntity> references = await _context.Set<ReferenceEntity>()
                .Where(x => parameters.References.Contains(x.Id))
                .ToListAsync();

            entity.References = references;
        }

        if (parameters.Brands != null && parameters.Brands.Any())
        {
            List<BrandEntity> brands = await _context.Set<BrandEntity>()
                .Where(x => parameters.Brands.Contains(x.Id))
                .ToListAsync();

            entity.Brands = brands;
        }

        if (parameters.Specialties != null && parameters.Specialties.Any())
        {
            List<SpecialityEntity> specialities = await _context.Set<SpecialityEntity>()
                .Where(x => parameters.Specialties.Contains(x.Id))
                .ToListAsync();

            entity.Specialities = specialities;
        }

        if (parameters.Tags != null && parameters.Tags.Any())
        {
            List<TagEntity> tags = await _context.Set<TagEntity>()
                .Where(x => parameters.Tags.Contains(x.Id))
                .ToListAsync();

            entity.Tags = tags;
        }

        if (parameters.Forms != null && parameters.Forms.Any())
        {
            List<FormEntity> forms = await _context.Set<FormEntity>()
                .Where(x => parameters.Forms.Contains(x.Id))
                .ToListAsync();

            entity.Forms = forms;
        }

        if (parameters.VoteFields != null && parameters.VoteFields.Any())
        {
            List<VoteFieldEntity> voteFields = await _context.Set<VoteFieldEntity>()
                .Where(x => parameters.VoteFields.Contains(x.Id))
                .ToListAsync();

            entity.VoteFields = voteFields;
        }

        if (parameters.Reports != null && parameters.Reports.Any())
        {
            List<ReportEntity> reports = await _context.Set<ReportEntity>()
                .Where(x => parameters.Reports.Contains(x.Id))
                .ToListAsync();

            entity.Reports = reports;
        }

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(entity));
    }

    public async Task<GenericResponse> Delete(Guid id)
    {
        T? i = await _context.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        i.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return new GenericResponse();
    }
}