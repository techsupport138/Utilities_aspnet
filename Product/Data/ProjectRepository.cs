using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Data;

public interface IProjectRepository {
    bool SaveChanges();
    Task<GetProjectDto> Add(AddUpdateProjectDto dto);
    Task<IEnumerable<GetProjectDto>> Get();
    GetProjectDto GetById(string id);
    void Delete(string id);
}

public class ProjectRepository : IProjectRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ProjectRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public bool SaveChanges() => _dbContext.SaveChanges() >= 0;

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

    public GetProjectDto GetById(string id) {
        ProjectEntity? i = _dbContext.Set<ProjectEntity>().AsNoTracking().FirstOrDefault(i => i.Id == id);
        return _mapper.Map<GetProjectDto>(i);
    }

    public async void Delete(string id) {
        GetProjectDto i = GetById(id);
        _dbContext.Set<ProjectEntity>().Remove(_mapper.Map<ProjectEntity>(i));
        await _dbContext.SaveChangesAsync();
    }
}