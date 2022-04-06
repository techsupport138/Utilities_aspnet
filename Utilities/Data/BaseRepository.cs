using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Data
{
    public interface IBaseRepository
    {
        List<LanguageEntity> GetAllLanguages();
        List<KVVM> GetAllFor();
        Dictionary<Guid, string> GetParentCategory(CategoryForEnum filter, string Language);
    }
    public class BaseRepository: IBaseRepository
    {
        internal readonly DbContext _context;
        internal readonly IMapper _mapper;

        public BaseRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<KVVM> GetAllFor()
        {
            string[] trans = Enum.GetNames(typeof(CategoryForEnum));
            var data = new List<KVVM>();
            var v = trans.Select((value, key) =>
            new { value, key }).ToDictionary(x => x.key+ 1, x => x.value);
            foreach (var item in v)
            {
                data.Add(new KVVM()
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
            return data;
        }

        public List<LanguageEntity> GetAllLanguages()
        {
            return _context.Set<LanguageEntity>().ToList();
        }

        public Dictionary<Guid, string> GetParentCategory(CategoryForEnum filter, string Language)
        {
            var data = _context.Set<CategoryEntity>()
                .Where(x => x.ParentId == null && x.CategoryFor == filter && x.LanguageId==Language)
                .ToDictionary(x => x.CategoryId, x => x.Title);
            return data;
        }
    }
}
