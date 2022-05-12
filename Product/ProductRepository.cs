using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Product.Data;

public interface IProductRepository<T> where T : BaseProductEntity {
    Task<GenericResponse<ProductReadDto>> Create(AddUpdateProductDto dto);
    Task<GenericResponse<IEnumerable<ProductReadDto>>> Read();
    Task<GenericResponse<ProductReadDto>> ReadById(Guid id);
    Task<GenericResponse<ProductReadDto>> Update(Guid id, AddUpdateProductDto dto);
    void Delete(Guid id);
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

    public async Task<GenericResponse<ProductReadDto>> Create(AddUpdateProductDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);
        entity.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        EntityEntry<T> i = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ProductReadDto>>> Read() {
        IEnumerable<T> i = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        return new GenericResponse<IEnumerable<ProductReadDto>>(_mapper.Map<IEnumerable<ProductReadDto>>(i));
    }

    public async Task<GenericResponse<ProductReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking().Include(i => i.User).Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ProductReadDto>(_mapper.Map<ProductReadDto>(i));
    }

    public Task<GenericResponse<ProductReadDto>> Update(Guid id, AddUpdateProductDto dto) {
        throw new NotImplementedException();
    }

    public async void Delete(Guid id) {
        GenericResponse<ProductReadDto> i = await ReadById(id);
        _dbContext.Set<T>().Remove(_mapper.Map<T>(i.Result));
        await _dbContext.SaveChangesAsync();
    }
}