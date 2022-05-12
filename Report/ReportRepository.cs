namespace Utilities_aspnet.Report;

public interface IReportRepository {
    Task Create(ReportCreateDto dto);
}

public class ReportRepository : IReportRepository {
    private readonly DbContext _context;

    public ReportRepository(DbContext context) {
        _context = context;
    }

    public async Task Create(ReportCreateDto dto) { }
}