using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Data;

public interface IProjectRepository {
    Task<GetProjectDto> Add(AddUpdateProjectDto dto);
    Task<IEnumerable<GetProjectDto>> Get();
    Task<GetProjectDto> GetById(Guid id);
    Task<GetProjectDto> Update(Guid id, AddUpdateProjectDto dto);
    void Delete(Guid id);
}

public class ProjectRepository : IProjectRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ProjectRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetProjectDto> Add(AddUpdateProjectDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        EntityEntry<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().AddAsync(_mapper.Map<ProjectEntity>(dto));
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<GetProjectDto>(i.Entity);
    }

    public async Task<IEnumerable<GetProjectDto>> Get() {
        List<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<GetProjectDto>>(i);
    }

    public async Task<GetProjectDto> GetById(Guid id) {
        ProjectEntity? i = await _dbContext.Set<ProjectEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return _mapper.Map<GetProjectDto>(i);
    }

    public Task<GetProjectDto> Update(Guid id, AddUpdateProjectDto dto) {
        throw new NotImplementedException();
    }

    public async void Delete(Guid id) {
        GetProjectDto i = await GetById(id);
        _dbContext.Set<ProjectEntity>().Remove(_mapper.Map<ProjectEntity>(i));
        await _dbContext.SaveChangesAsync();
    }
}