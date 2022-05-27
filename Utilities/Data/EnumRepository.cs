namespace Utilities_aspnet.Utilities.Data;

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
            Favorites = _context.Set<FavoriteEntity>().Select(x => new IdTitleReadDto {
                Id = x.Id,
                Title = x.Title
            }).ToList(),
            Colors = _context.Set<ColorEntity>().Select(x => new IdTitleReadDto {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Color
            }).ToList(),
            Specialties = _context.Set<SpecialityEntity>().Select(x => new IdTitleReadDto {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Color
            }).ToList()
        };

        List<KVIdTitle> formFieldType = EnumExtension.GetValues<FormFieldType>();
        List<KVIdTitle> idTitleUseCase = EnumExtension.GetValues<IdTitleUseCase>();
        model.FormFieldType = formFieldType;
        model.IdTitleUseCase = idTitleUseCase;
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
                .Include(x => x.Media)
                .Include(x => x.Parent)
                .OrderBy(x => x.UseCase)
                .Select(w =>
                    new KVPCategoryVM {
                        Key = w.Id,
                        // Image = w.Media.FileName,
                        Value = w.Title,
                        CategoryFor = w.UseCase,
                        ParentId = w.ParentId
                        // Childs = w.InverseParent.Select(x => new KVPCategoryVM {
                        //     Key = x.CategoryId,
                        //     Image = x.Media.FileName,
                        //     Value = x.Title,
                        //     CategoryFor = x.CategoryFor,
                        //     ParentId = x.ParentId,
                        //     ParentTitle = x.Parent.Title
                        // }).ToList()
                    }).ToList();

        return Task.FromResult(new GenericResponse<EnumDto?>(model, UtilitiesStatusCodes.Success, "Success"));
    }
}