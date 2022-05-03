using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Data;

public interface IProjectRepository {
    Task<GetProductDto> Add(AddUpdateProductDto dto);
    Task<IEnumerable<GetProductDto>> Get();
    Task<GetProductDto> GetById(Guid id);
    Task<GetProductDto> Update(Guid id, AddUpdateProductDto dto);
    void Delete(Guid id);
}

public class ProjectRepository : IProjectRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ProjectRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetProductDto> Add(AddUpdateProductDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        EntityEntry<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().AddAsync(_mapper.Map<ProjectEntity>(dto));
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<GetProductDto>(i.Entity);
    }

    public async Task<IEnumerable<GetProductDto>> Get() {
        List<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(i);
    }

    public async Task<GetProductDto> GetById(Guid id) {
        ProjectEntity? i = await _dbContext.Set<ProjectEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return _mapper.Map<GetProductDto>(i);
    }

    public Task<GetProductDto> Update(Guid id, AddUpdateProductDto dto) {
        throw new NotImplementedException();
    }

    public async void Delete(Guid id) {
        GetProductDto i = await GetById(id);
        _dbContext.Set<ProjectEntity>().Remove(_mapper.Map<ProjectEntity>(i));
        await _dbContext.SaveChangesAsync();
    }
}