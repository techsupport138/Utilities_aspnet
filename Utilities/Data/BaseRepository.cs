using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.Utilities.Data {
    public interface IBaseRepository {
        List<KVVM> GetAllFor();
        Dictionary<Guid, string> GetParentCategory(IdTitleUseCase filter, string Language);
    }

    public class BaseRepository : IBaseRepository {
        internal readonly DbContext _context;
        internal readonly IMapper _mapper;

        public BaseRepository(DbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public List<KVVM> GetAllFor() {
            string[] trans = Enum.GetNames(typeof(IdTitleUseCase));
            var data = new List<KVVM>();
            var v = trans.Select((value, key) => new {value, key}).ToDictionary(x => x.key + 1, x => x.value);
            foreach (var item in v) {
                data.Add(new KVVM() {
                    Key = item.Key,
                    Value = item.Value
                });
            }

            return data;
        }

        public Dictionary<Guid, string> GetParentCategory(IdTitleUseCase filter, string Language) {
            var data = _context.Set<CategoryEntity>().ToDictionary(x => x.Id, x => x.Title);
            return data;
        }
    }
}