using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Utilities.Data
{
    public interface IEnumRepository
    {
        Task<GenericResponse<EnumDto?>> GetAll(bool ShowCatehory,bool ShowGeo);
    }
    public class EnumRepository : IEnumRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public EnumRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<GenericResponse<EnumDto?>> GetAll(bool ShowCatehory=false, bool ShowGeo=false)
        {
            var model = new EnumDto();

            model.Language = _context.Set<LanguageEntity>()
                .Select(x => new KVMIVM()
                {
                    Value = x.LanguageName,
                    Alt = x.Symbol,
                })
                .ToList();
            model.Favorites = _context.Set<FavoriteEntity>()
                .Select(x => new KVMVM()
                {
                    Key = x.Id,
                    Value = x.Title
                })
                .ToList();
            model.Colors = _context.Set<ColorEntity>()
                .Select(x => new KVMVM()
                {
                    Key = x.Id,
                    Value = x.Title,
                    Alt = x.ColorHex
                })
                .ToList();
            model.Specialties = _context.Set<SpecialtyEntity>()
                .Include(x => x.Media)
                .Select(x => new KVMVM()
                {
                    Key = x.Id,
                    Value = x.SpecialtyTitle,
                    Media = x.Media.FileName
                })
                .ToList();

            model.UserRole = _context.Set<UserRoleEntity>()
                .Select(x => new KVMIVM()
                {
                    Key = x.RoleId,
                    Value = x.RoleName,
                })
                .ToList();

            model.SpecialtyCategories = _context.Set<SpecialtyCategoryEntity>()
                .Select(x => new KVMVM()
                {
                    Key = x.Id,
                    Value = x.SpecialtyCategoryTitle,
                })
                .ToList();

            if(ShowGeo)
            model.GeoList = _context.Set<Province>()
                .Include(x => x.Cities)
                .Select(x => new KVPIVM()
                {
                    Key = x.ProvinceId,
                    Value = x.ProvinceName,
                    Childs = x.Cities.Select(y => new KVPIVM()
                    {
                        Key = y.CityId,
                        Value = y.CityName,
                    }).ToList()
                }).ToList();

            if(ShowCatehory)
            model.Categories = _context.Set<CategoryEntity>()
                 //.Where(x => x.LanguageId == filter.Language && x.CategoryFor == filter.CategoryFor)
                 .Include(x => x.Media).Include(x => x.Parent)
                 .OrderBy(x => x.LanguageId).OrderBy(x=>x.CategoryFor)
                 .Select(w => new KVPCategoryVM()
                 {
                     Key = w.CategoryId,
                     Image = w.Media.FileName,
                     Value = w.Title,
                     CategoryFor = w.CategoryFor,
                     LanguageId = w.LanguageId,
                     ParentId = w.ParentId,
                     Childs = w.InverseParent.Select(x => new KVPCategoryVM()
                     {
                         Key = x.CategoryId,
                         Image = x.Media.FileName,
                         Value = x.Title,
                         CategoryFor = x.CategoryFor,
                         LanguageId = x.LanguageId,
                         ParentId = x.ParentId,
                         ParentTitle = x.Parent.Title
                     }).ToList()
                 }).ToList();

            return Task.FromResult(new GenericResponse<EnumDto?>
            (model, UtilitiesStatusCodes.Success, "Success"));
        }
    }
}
