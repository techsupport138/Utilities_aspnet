using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;
using MongoDatabaseSettings = Utilities_aspnet.Utilities.MongoDatabaseSettings;

namespace Utilities_aspnet.Product.Data;

public interface IProjectRepository {
    Task<GetProjectDto> Add(AddUpdateProjectDto dto);
    Task<IEnumerable<GetProjectDto>> Get();
    Task<GetProjectDto> GetById(string id);
    Task<GetProjectDto> Update(string id, AddUpdateProjectDto dto);
    void Delete(string id);
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

    public async Task<GetProjectDto> GetById(string id) {
        ProjectEntity? i = await _dbContext.Set<ProjectEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return _mapper.Map<GetProjectDto>(i);
    }

    public Task<GetProjectDto> Update(string id, AddUpdateProjectDto dto) {
        throw new NotImplementedException();
    }

    public async void Delete(string id) {
        GetProjectDto i = await GetById(id);
        _dbContext.Set<ProjectEntity>().Remove(_mapper.Map<ProjectEntity>(i));
        await _dbContext.SaveChangesAsync();
    }
}

public class ProjectRepositoryMongoDb : IProjectRepository {
    private readonly IMongoCollection<ProjectEntity> _collection;
    private readonly IMapper _mapper;

    public ProjectRepositoryMongoDb(IOptions<MongoDatabaseSettings> bookStoreDatabaseSettings, IMapper mapper) {
        _mapper = mapper;
        MongoClient mongoClient = new(bookStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase? mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<ProjectEntity>(bookStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<GetProjectDto> Add(AddUpdateProjectDto dto) {
        await _collection.InsertOneAsync(_mapper.Map<ProjectEntity>(dto));
        return _mapper.Map<GetProjectDto>(dto);
    }

    public async Task<IEnumerable<GetProjectDto>> Get() {
        IEnumerable<ProjectEntity> i = await _collection.Find(_ => true).ToListAsync();
        return _mapper.Map<IEnumerable<GetProjectDto>>(i);
    }

    public async Task<GetProjectDto> GetById(string id) {
        ProjectEntity i = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<GetProjectDto>(i);
    }

    public async Task<GetProjectDto> Update(string id, AddUpdateProjectDto dto) {
        await _collection.ReplaceOneAsync(x => x.Id == id, _mapper.Map<ProjectEntity>(dto));
        return _mapper.Map<GetProjectDto>(dto);
    }

    public async void Delete(string id) {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}