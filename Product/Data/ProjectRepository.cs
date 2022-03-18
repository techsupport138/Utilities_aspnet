using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.User.Entities;

namespace Utilities_aspnet.Product.Data;

public interface IProjectRepository {
    bool SaveChanges();
    Task<GetProjectDto> Add(AddProjectDto dto);
    Task<IEnumerable<GetProjectDto>> Get();
}

public class ProjectRepository : IProjectRepository {

    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ProjectRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public bool SaveChanges() => _dbContext.SaveChanges() >= 0;
    
    public async Task<GetProjectDto> Add(AddProjectDto dto) {
        EntityEntry<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().AddAsync(_mapper.Map<ProjectEntity>(dto));
        return _mapper.Map<GetProjectDto>(i);
    }

    public async Task<IEnumerable<GetProjectDto>> Get() {
        List<ProjectEntity> i = await _dbContext.Set<ProjectEntity>().ToListAsync();
        return _mapper.Map<IEnumerable<GetProjectDto>>(i);
    }
}