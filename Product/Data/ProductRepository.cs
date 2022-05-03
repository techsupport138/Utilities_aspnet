using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Product.Dto;

namespace Utilities_aspnet.Product.Data;

public interface IProductRepository<T> where T : BasePEntity {
    Task<GetProductDto> Add(AddUpdateProductDto dto);
    Task<IEnumerable<GetProductDto>> Get();
    Task<GetProductDto> GetById(Guid id);
    Task<GetProductDto> Update(Guid id, AddUpdateProductDto dto);
    void Delete(Guid id);
}

public class ProductRepository<T> : IProductRepository<T> where T : BasePEntity {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetProductDto> Add(AddUpdateProductDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        EntityEntry<T> i = await _dbContext.Set<T>().AddAsync(_mapper.Map<T>(dto));
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<GetProductDto>(i.Entity);
    }

    public async Task<IEnumerable<GetProductDto>> Get() {
        List<T> i = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(i);
    }

    public async Task<GetProductDto> GetById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return _mapper.Map<GetProductDto>(i);
    }

    public Task<GetProductDto> Update(Guid id, AddUpdateProductDto dto) {
        throw new NotImplementedException();
    }

    public async void Delete(Guid id) {
        GetProductDto i = await GetById(id);
        _dbContext.Set<T>().Remove(_mapper.Map<T>(i));
        await _dbContext.SaveChangesAsync();
    }
}