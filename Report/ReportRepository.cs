namespace Utilities_aspnet.Report;

public interface IReportRepository {
    Task Create(ReportCreateDto dto);
    Task<GenericResponse<IEnumerable<ReportReadDto>>> Read(ReportFilterDto dto);
}

public class ReportRepository : IReportRepository {
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReportRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Create(ReportCreateDto dto) {
        ReportEntity entity = new() {UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!};

        if (dto.ProductId.HasValue) entity.ProductId = dto.ProductId;
        if (dto.DailyPriceId.HasValue) entity.DailyPriceId = dto.DailyPriceId;
        if (dto.ProjectId.HasValue) entity.ProjectId = dto.ProjectId;
        if (dto.TutorialId.HasValue) entity.TutorialId = dto.TutorialId;
        if (dto.EventId.HasValue) entity.EventId = dto.EventId;
        if (dto.AdId.HasValue) entity.AdId = dto.AdId;
        if (dto.CompanyId.HasValue) entity.CompanyId = dto.CompanyId;
        if (dto.TenderId.HasValue) entity.TenderId = dto.TenderId;
        if (dto.ServiceId.HasValue) entity.ServiceId = dto.ServiceId;
        if (dto.MagazineId.HasValue) entity.MagazineId = dto.MagazineId;

        await _dbContext.Set<ReportEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GenericResponse<IEnumerable<ReportReadDto>>> Read(ReportFilterDto dto) {
        IQueryable<ReportEntity> e = _dbContext.Set<ReportEntity>().AsNoTracking();
        IEnumerable<ReportReadDto> readDto = Array.Empty<ReportReadDto>();
        return new GenericResponse<IEnumerable<ReportReadDto>>(null);
    }
}