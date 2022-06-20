namespace Utilities_aspnet.Repositories;

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
		EnumDto model = new();

		IEnumerable<CategoryReadDto> formFieldType = EnumExtension.GetValues<FormFieldType>();
		IEnumerable<CategoryReadDto> transactionStatus = EnumExtension.GetValues<TransactionStatus>();
		model.FormFieldType = formFieldType;
		model.TransactionStatuses = transactionStatus;

		model.Categories = _context.Set<CategoryEntity>()
			.Include(x => x.Media)
			.Include(x => x.Parent)
			.OrderBy(x => x.UseCase)
			.Select(w =>
				        new CategoryReadDto {
					        Id = w.Id,
					        Title = w.Title,
					        UseCase = w.UseCase,
					        ParentId = w.ParentId
				        }).ToList();
		model.Genders = _context.Set<GenderEntity>().ToList();

		return Task.FromResult(new GenericResponse<EnumDto?>(model, UtilitiesStatusCodes.Success, "Success"));
	}

	public Task<GenericResponse<IEnumerable<LocationReadDto?>>> ReadLocation() {
		IEnumerable<LocationEntity> model = _context.Set<LocationEntity>().Include(x => x.Children).ThenInclude(x => x.Media)
			.Include(x => x.Media);

		return Task.FromResult(
			new GenericResponse<IEnumerable<LocationReadDto?>>(_mapper.Map<IEnumerable<LocationReadDto>>(model)));
	}
}