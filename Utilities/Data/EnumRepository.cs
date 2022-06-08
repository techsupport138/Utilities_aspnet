namespace Utilities_aspnet.Utilities.Data;

public interface IAppSettingRepository {
    Task<GenericResponse<EnumDto?>> Read();
    Task<GenericResponse<IEnumerable<LocationReadDto?>>> ReadLocation();
}

public class AppSettingRepository : IAppSettingRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public AppSettingRepository(DbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<GenericResponse<EnumDto?>> Read() {
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

        List<IdTitleReadDto> formFieldType = EnumExtension.GetValues<FormFieldType>();
        List<IdTitleReadDto> idTitleUseCase = EnumExtension.GetValues<IdTitleUseCase>();
        model.FormFieldType = formFieldType;
        model.CategoryUseCase = idTitleUseCase;

        model.Categories = _context.Set<CategoryEntity>()
            .Include(x => x.Media)
            .Include(x => x.Parent)
            .OrderBy(x => x.UseCase)
            .Select(w =>
                new IdTitleReadDto {
                    Id = w.Id,
                    Title = w.Title,
                    UseCase = w.UseCase,
                    ParentId = w.ParentId
                }).ToList();

        return Task.FromResult(new GenericResponse<EnumDto?>(model, UtilitiesStatusCodes.Success, "Success"));
    }
    
    public Task<GenericResponse<IEnumerable<LocationReadDto?>>> ReadLocation() {

        IEnumerable<LocationEntity> model = _context.Set<LocationEntity>().Include(x=>x.Parent).ThenInclude(x=>x.Media).Include(x=>x.Media);

        return Task.FromResult(new GenericResponse<IEnumerable<LocationReadDto?>>(_mapper.Map<IEnumerable<LocationReadDto>>(model)));
    }
}