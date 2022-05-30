using System.Security.Principal;
using Utilities_aspnet.FollowBookmark;

namespace Utilities_aspnet.Product;

public interface IProductRepository<T> where T : BaseProductEntity
{
    Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? filterDto);
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

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read(FilterProductDto? filterDto)
    {
        IEnumerable<T> i = await _context.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
            .Include(i => i.Locations)
            .Include(i => i.Reports)
            .Include(i => i.Specialities)
            .Include(i => i.Tags)
            .Include(i => i.Brands)
            .Include(i => i.References)
            .Include(i => i.User)
            .Include(i => i.Forms)!.ThenInclude(x => x.FormField)
            .ToListAsync();


        if (filterDto != null)
        {
            if (filterDto.Query != null) i = i.Where(x => !string.IsNullOrEmpty(x.Title) && x.Title.Contains(filterDto.Query));
            if (filterDto.DescendingDate != null)
                i = filterDto.DescendingDate == true ? i.OrderByDescending(x => x.CreatedAt) : i.OrderBy(x => x.CreatedAt);
        }

        IEnumerable<ProductReadDto>? dto = _mapper.Map<IEnumerable<ProductReadDto>>(i).ToList();

        // ReSharper disable once InvertIf
        if (_user != null)
        {
            IEnumerable<BookmarkEntity> bookmark = _context.Set<BookmarkEntity>()
                .AsNoTracking()
                .Where(x => x.UserId == _user.Name)
                .ToList();

            foreach (ProductReadDto productReadDto in dto)
            {
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
            .Include(i => i.User)
            .Include(i => i.Forms)!.ThenInclude(x => x.FormField)
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
    }

    public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto parameters)
    {
        var entity = await _context.Set<ProductEntity>()
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

        if (!string.IsNullOrEmpty(parameters.Title))
            entity.Title = parameters.Title;

        if (!string.IsNullOrEmpty(parameters.Subtitle))
            entity.Subtitle = parameters.Subtitle;

        if (!string.IsNullOrEmpty(parameters.Description))
            entity.Description = parameters.Description;

        if (parameters.Price.HasValue)
            entity.Price = parameters.Price.Value;

        if (parameters.IsForSale.HasValue)
            entity.IsForSale = parameters.IsForSale.Value;

        if (parameters.Enabled.HasValue)
            entity.Enabled = parameters.Enabled.Value;

        if (parameters.VisitsCount.HasValue)
            entity.VisitCount = parameters.VisitsCount.Value;

        if (!string.IsNullOrEmpty(parameters.Address))
            entity.Address = parameters.Address;

        if (parameters.StartDate.HasValue)
            entity.StartDate = parameters.StartDate.Value;

        if (parameters.EndDate.HasValue)
            entity.EndDate = parameters.EndDate.Value;


        if (parameters.Locations != null && parameters.Locations.Any())
        {
            var locations = await _context.Set<LocationEntity>()
                .Where(x => parameters.Locations.Contains(x.Id))
                .ToListAsync();

            entity.Locations = locations;
        }

        if (parameters.Favorites != null && parameters.Favorites.Any())
        {
            var favorites = await _context.Set<FavoriteEntity>()
                .Where(x => parameters.Favorites.Contains(x.Id))
                .ToListAsync();

            entity.Favorites = favorites;
        }

        if (parameters.Categories != null && parameters.Categories.Any())
        {
            var categories = await _context.Set<CategoryEntity>()
                .Where(x => parameters.Categories.Contains(x.Id))
                .ToListAsync();

            entity.Categories = categories;
        }

        if (parameters.References != null && parameters.References.Any())
        {
            var references = await _context.Set<ReferenceEntity>()
                .Where(x => parameters.References.Contains(x.Id))
                .ToListAsync();

            entity.References = references;
        }

        if (parameters.Brands != null && parameters.Brands.Any())
        {
            var brands = await _context.Set<BrandEntity>()
                .Where(x => parameters.Brands.Contains(x.Id))
                .ToListAsync();

            entity.Brands = brands;
        }

        if (parameters.Specialties != null && parameters.Specialties.Any())
        {
            var specialities = await _context.Set<SpecialityEntity>()
                .Where(x => parameters.Specialties.Contains(x.Id))
                .ToListAsync();

            entity.Specialities = specialities;
        }

        if (parameters.Tags != null && parameters.Tags.Any())
        {
            var tags = await _context.Set<TagEntity>()
                .Where(x => parameters.Tags.Contains(x.Id))
                .ToListAsync();

            entity.Tags = tags;
        }

        if (parameters.Forms != null && parameters.Forms.Any())
        {
            var forms = await _context.Set<FormEntity>()
                .Where(x => parameters.Forms.Contains(x.Id))
                .ToListAsync();

            entity.Forms = forms;
        }

        if (parameters.VoteFields != null && parameters.VoteFields.Any())
        {
            var voteFields = await _context.Set<VoteFieldEntity>()
                .Where(x => parameters.VoteFields.Contains(x.Id))
                .ToListAsync();

            entity.VoteFields = voteFields;
        }

        if (parameters.Reports != null && parameters.Reports.Any())
        {
            var reports = await _context.Set<ReportEntity>()
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
        GenericResponse<ProductReadDto> i = await ReadById(id);
        _context.Set<T>().Remove(_mapper.Map<T>(i.Result));
        await _context.SaveChangesAsync();
        return new GenericResponse();
    }
}