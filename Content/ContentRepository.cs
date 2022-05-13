using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Content;

public interface IContentRepository<T> where T : BaseContentEntity {
    Task<GenericResponse<ContentReadDto>> Create(ContentCreateUpdateDto dto);
    Task<GenericResponse<IEnumerable<ContentReadDto>>> Read();
    Task<GenericResponse<ContentReadDto>> ReadById(Guid id);
    Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto);
    Task<GenericResponse> Delete(Guid id);
}

public class ContentRepository<T> : IContentRepository<T> where T : BaseContentEntity {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public ContentRepository(DbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GenericResponse<ContentReadDto>> Create(ContentCreateUpdateDto dto) {
        if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
        T entity = _mapper.Map<T>(dto);
        EntityEntry<T> i = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i.Entity));
    }

    public async Task<GenericResponse<IEnumerable<ContentReadDto>>> Read() {
        IEnumerable<T> i = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        return new GenericResponse<IEnumerable<ContentReadDto>>(_mapper.Map<IEnumerable<ContentReadDto>>(i));
    }

    public async Task<GenericResponse<ContentReadDto>> ReadById(Guid id) {
        T? i = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return new GenericResponse<ContentReadDto>(_mapper.Map<ContentReadDto>(i));
    }

    public Task<GenericResponse<ContentReadDto>> Update(ContentCreateUpdateDto dto) {
        throw new NotImplementedException();
    }

    public async Task<GenericResponse> Delete(Guid id) {
        GenericResponse<ContentReadDto> i = await ReadById(id);
        _dbContext.Set<T>().Remove(_mapper.Map<T>(i.Result));
        await _dbContext.SaveChangesAsync();
        return new GenericResponse();
    }
}