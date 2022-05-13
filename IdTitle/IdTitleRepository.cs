using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.IdTitle;

public interface ITagRepository {
    public Task<GenericResponse<TagReadDto>> Create(TagCreateUpdateDto dto);
    public Task<GenericResponse<IEnumerable<TagReadDto>>> Read();
    public Task<GenericResponse<TagReadDto>> ReadById(int id);
    public Task<GenericResponse<TagReadDto>> Update(TagCreateUpdateDto dto);
    public Task<GenericResponse> Delete(int id);
}

public class TagRepository : ITagRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public TagRepository(DbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<TagReadDto>> Create(TagCreateUpdateDto dto) {
        TagEntity entity = _mapper.Map<TagEntity>(dto);

        EntityEntry<TagEntity>? i = await _context.AddAsync(entity);
        return new GenericResponse<TagReadDto>(_mapper.Map<TagReadDto>(i.Entity));
    }


    public Task<GenericResponse<IEnumerable<TagReadDto>>> Read() {
        throw new NotImplementedException();
    }

    public Task<GenericResponse<TagReadDto>> ReadById(int id) {
        throw new NotImplementedException();
    }

    public Task<GenericResponse> Delete(int id) {
        throw new NotImplementedException();
    }

    public Task<GenericResponse<TagReadDto>> Update(TagCreateUpdateDto dto) {
        throw new NotImplementedException();
    }
}