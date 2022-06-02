namespace Utilities_aspnet.Report;

public interface IReportRepository {
    Task<GenericResponse<ReportReadDto?>> Create(ReportCreateDto dto);
    Task<GenericResponse<IEnumerable<ReportReadDto>>> Read(ReportFilterDto dto);
    Task<GenericResponse> Delete(Guid id);
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

    public async Task<GenericResponse<ReportReadDto?>> ReadById(Guid id) {
        var report = await _dbContext.Set<ReportEntity>()
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Product)
            .Include(x => x.Project)
            .Include(x => x.DailyPrice)
            .Include(x => x.Tutorial)
            .Include(x => x.Event)
            .Include(x => x.Company)
            .Include(x => x.Tender)
            .Include(x => x.Service)
            .Include(x => x.Magazine)
            .Include(x => x.Ad)
            .Select(x => new ReportReadDto() {
                Ad = x.Ad,
                Company = x.Company,
                CreatedAt = x.CreatedAt,
                DailyPrice = x.DailyPrice,
                DeletedAt = x.DeletedAt,
                Description = x.Description,
                Event = x.Event,
                Id = x.Id,
                Magazine = x.Magazine,
                Product = x.Product,
                Project = x.Project,
                Service = x.Service,
                Tender = x.Tender,
                Title = x.Title,
                Tutorial = x.Tutorial,
                UpdatedAt = x.UpdatedAt,
                User = x.User
            })
            .FirstOrDefaultAsync();

        return new GenericResponse<ReportReadDto?>(report);
    }

    public async Task<GenericResponse<ReportReadDto?>> Create(ReportCreateDto dto) {
        ReportEntity entity = new() {
            UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!,
            Title = dto.Title,
            Description = dto.Description
        };

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

        return await ReadById(entity.Id);
    }

    public async Task<GenericResponse<IEnumerable<ReportReadDto>>> Read(ReportFilterDto dto) {
        var entities = _dbContext.Set<ReportEntity>().AsNoTracking();

        if (dto.Company == true)
            entities = entities.Include(x => x.Company);

        if (dto.User == true)
            entities = entities.Include(x => x.User);

        if (dto.Product == true)
            entities = entities.Include(x => x.Product);

        if (dto.Project == true)
            entities = entities.Include(x => x.Project);

        if (dto.DailyPrice == true)
            entities = entities.Include(x => x.DailyPrice);

        if (dto.Tutorial == true)
            entities = entities.Include(x => x.Tutorial);

        if (dto.Event == true)
            entities = entities.Include(x => x.Event);

        if (dto.Ad == true)
            entities = entities.Include(x => x.Ad);

        if (dto.Tender == true)
            entities = entities.Include(x => x.Tender);

        if (dto.Service == true)
            entities = entities.Include(x => x.Service);

        if (dto.Magazine == true)
            entities = entities.Include(x => x.Magazine);

        var result = await entities.Select(x => new ReportReadDto() {
            Ad = x.Ad,
            Company = x.Company,
            CreatedAt = x.CreatedAt,
            DailyPrice = x.DailyPrice,
            DeletedAt = x.DeletedAt,
            Description = x.Description,
            Event = x.Event,
            Id = x.Id,
            Magazine = x.Magazine,
            Product = x.Product,
            Project = x.Project,
            Service = x.Service,
            Tender = x.Tender,
            Title = x.Title,
            Tutorial = x.Tutorial,
            UpdatedAt = x.UpdatedAt,
            User = x.User
        }).ToListAsync();

        return new GenericResponse<IEnumerable<ReportReadDto>>(result);
    }

    public async Task<GenericResponse> Delete(Guid id) {
        var report = await _dbContext.Set<ReportEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (report == null)
            return new GenericResponse(UtilitiesStatusCodes.NotFound, "Report notfound");

        _dbContext.Set<ReportEntity>().Remove(report);
        await _dbContext.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }
}