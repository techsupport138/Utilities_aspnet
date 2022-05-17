using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.IdTitle;
using Utilities_aspnet.Utilities.Responses;

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
        List<CategoryEntity> categories = new List<CategoryEntity>();
        List<LocationEntity> locations = new List<LocationEntity>();
        List<SpecialityEntity> specialities = new List<SpecialityEntity>();
        List<TagEntity> tags = new List<TagEntity>();
        foreach (var item in dto.Categories)
        {
            var category = await _dbContext.Set<CategoryEntity>().Include(x => x.Project).Include(x => x.Product).Include(x => x.Ad).Include(x => x.Tender).FirstOrDefaultAsync(x=>x.Id == item);
            if(category != null)
            {
                categories.Add(category);
            }
        }
        foreach (var item in dto.Location)
        {
            var location = await _dbContext.Set<LocationEntity>().Include(x => x.Project).Include(x => x.Product).Include(x => x.Ad).Include(x => x.Tender).FirstOrDefaultAsync(x=>x.Id == item);
            if(location != null)
            {
                locations.Add(location);
            }
        }
        foreach (var item in dto.Specialties)
        {
            var speciality = await _dbContext.Set<SpecialityEntity>().Include(x => x.Project).Include(x => x.Product).Include(x => x.Ad).Include(x => x.Tender).FirstOrDefaultAsync(x=>x.Id == item);
            if(speciality != null)
            {
                specialities.Add(speciality);
            }
        }
        foreach (var item in dto.Tags)
        {
            var tag = await _dbContext.Set<TagEntity>().Include(x=>x.Project).Include(x=>x.Product).Include(x=>x.Ad).Include(x=>x.Tender).FirstOrDefaultAsync(x=>x.Id == item);
            if(tag != null)
            {
                tags.Add(tag);
            }
        }


        entity.Category = categories;
        entity.Location = locations;
        entity.Speciality = specialities;
        entity.Tag = tags;
        EntityEntry<T> i = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read() {
        IEnumerable<T> i = await _dbContext.Set<T>().AsNoTracking().Include(i => i.Media).ToListAsync();
        return new GenericResponse<IEnumerable<ProductReadDto>>(_mapper.Map<IEnumerable<ProductReadDto>>(i));
    }

    public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking().Include(i => i.User).Include(i => i.Category).Include(i => i.Media).Include(i => i.Location)
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