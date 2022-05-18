namespace Utilities_aspnet.Product;

public interface IProductRepository<T> where T : BaseProductEntity {
    Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> Read();
    Task<GenericResponse<ProductReadDto>> ReadById(Guid id);
    Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ProductRepository<T> : IProductRepository<T> where T : BaseProductEntity {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<ProductReadDto>> Create(ProductCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);
        entity.UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<CategoryEntity> categories = new();
        List<LocationEntity> locations = new();
        List<SpecialityEntity> specialities = new();
        List<TagEntity> tags = new();

        foreach (Guid item in dto.Categories ?? Array.Empty<Guid>()) {
            CategoryEntity? category = await _dbContext.Set<CategoryEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (category != null) {
                categories.Add(category);
            }
        }

        foreach (int item in dto.Locations ?? Array.Empty<int>()) {
            LocationEntity? location = await _dbContext.Set<LocationEntity>().Include(x => x.Project)
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (location != null) {
                locations.Add(location);
            }
        }

        foreach (Guid item in dto.Specialties ?? Array.Empty<Guid>()) {
            SpecialityEntity? speciality = await _dbContext.Set<SpecialityEntity>().Include(x => x.Project)
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event).FirstOrDefaultAsync(x => x.Id == item);
            if (speciality != null) {
                specialities.Add(speciality);
            }
        }

        foreach (Guid item in dto.Tags ?? Array.Empty<Guid>()) {
            TagEntity? tag = await _dbContext.Set<TagEntity>()
                .Include(x => x.Project)
                .Include(x => x.Product)
                .Include(x => x.Ad)
                .Include(x => x.Service)
                .Include(x => x.Company)
                .Include(x => x.Magazine)
                .Include(x => x.Tutorial)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (tag != null) {
                tags.Add(tag);
            }
        }

        entity.Categories = categories;
        entity.Locations = locations;
        entity.Speciality = specialities;
        entity.Tags = tags;
        EntityEntry<T> i = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read() {
        IEnumerable<T> i = await _dbContext.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
            .Include(i => i.Locations)
            .Include(i => i.Report)
            .Include(i => i.Speciality)
            .Include(i => i.Tags)
            .Include(i => i.User)
            .ToListAsync();
        IEnumerable<ProductReadDto>? dto = _mapper.Map<IEnumerable<ProductReadDto>>(i);
        return new GenericResponse<IEnumerable<ProductReadDto>>(dto);
    }

    public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking()
            .Include(i => i.Media)
            .Include(i => i.Categories)
            .Include(i => i.Locations)
            .Include(i => i.Report)
            .Include(i => i.Speciality)
            .Include(i => i.Tags)
            .Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
    }

    public async Task<GenericResponse<ProductReadDto>> Update(ProductCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);
        //entity.UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        EntityEntry<T> i = _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse> Delete(Guid id) {
        GenericResponse<ProductReadDto> i = await ReadById(id);
        _dbContext.Set<T>().Remove(_mapper.Map<T>(i.Result));
        await _dbContext.SaveChangesAsync();
        return new GenericResponse();
    }
}