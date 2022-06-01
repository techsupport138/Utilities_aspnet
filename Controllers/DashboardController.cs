using Utilities_aspnet.Dashboard;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DashboardController : BaseApiController {
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardController(IDashboardRepository dashboardRepository) {
        _dashboardRepository = dashboardRepository;
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse<DashboardDto>>> FilterReports(FilterDashboardDto model) {
        GenericResponse<DashboardDto> i = await _dashboardRepository.FilterReports(model);
        return Result(i);
    }
}