namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
    private readonly IAppSettingRepository _appSettingRepository;

    public AppSettingsController(IAppSettingRepository appSettingRepository) {
        _appSettingRepository = appSettingRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
        GenericResponse? i = await _appSettingRepository.Read();
        return Ok(i);
    }
    
    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<LocationReadDto?>>>> ReadLocation() {
        GenericResponse? i = await _appSettingRepository.ReadLocation();
        return Ok(i);
    }
}