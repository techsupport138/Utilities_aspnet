using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Utilities.Data {
    public interface IEnumRepository {
        Task<GenericResponse<EnumDto?>> GetAll(bool showCatehory, bool showGeo);
    }

    public class EnumRepository : IEnumRepository {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public EnumRepository(DbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public Task<GenericResponse<EnumDto?>> GetAll(bool showCatehory = false, bool showGeo = false) {
            EnumDto model = new() {
                Language = _context.Set<LanguageEntity>().Select(x => new IdTitleDto {
                    Title = x.LanguageName,
                    SubTitle = x.Symbol,
                }).ToList(),
                Favorites = _context.Set<FavoriteEntity>().Select(x => new IdTitleDto {
                    Id = x.Id.ToString(),
                    Title = x.Title
                }).ToList(),
                Colors = _context.Set<ColorEntity>().Select(x => new IdTitleDto {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    SubTitle = x.ColorHex
                }).ToList(),
                Specialties = _context.Set<SpecialtyEntity>().Include(x => x.Media).Select(x => new IdTitleDto {
                    Id = x.Id.ToString(),
                    Title = x.SpecialtyTitle,
                    Media = x.Media.FileName
                }).ToList(),
                UserRole = _context.Set<UserRoleEntity>().Select(x => new IdTitleDto {
                    Title = x.RoleId.ToString(),
                    SubTitle = x.RoleName,
                }).ToList(),
                SpecialtyCategories = _context.Set<SpecialtyCategoryEntity>().Select(x => new IdTitleDto {
                    Id = x.Id.ToString(),
                    Title = x.SpecialtyCategoryTitle,
                }).ToList()
            };

            //if (showGeo)
            //    model.GeoList = _context.Set<Province>().Include(x => x.Cities).Select(x => new KVPIVM {
            //        Key = x.ProvinceId,
            //        Value = x.ProvinceName,
            //        Childs = x.Cities.Select(y => new KVPIVM {
            //            Key = y.CityId,
            //            Value = y.CityName,
            //        }).ToList()
            //    }).ToList();

            if (showCatehory)
                model.Categories = _context.Set<CategoryEntity>()
                    //.Where(x => x.LanguageId == filter.Language && x.CategoryFor == filter.CategoryFor)
                    .Include(x => x.Media).Include(x => x.Parent).OrderBy(x => x.LanguageId).OrderBy(x => x.CategoryFor).Select(w =>
                        new KVPCategoryVM {
                            Key = w.CategoryId,
                            Image = w.Media.FileName,
                            Value = w.Title,
                            CategoryFor = w.CategoryFor,
                            LanguageId = w.LanguageId,
                            ParentId = w.ParentId,
                            Childs = w.InverseParent.Select(x => new KVPCategoryVM {
                                Key = x.CategoryId,
                                Image = x.Media.FileName,
                                Value = x.Title,
                                CategoryFor = x.CategoryFor,
                                LanguageId = x.LanguageId,
                                ParentId = x.ParentId,
                                ParentTitle = x.Parent.Title
                            }).ToList()
                        }).ToList();

            return Task.FromResult(new GenericResponse<EnumDto?>(model, UtilitiesStatusCodes.Success, "Success"));
        }
    }
}