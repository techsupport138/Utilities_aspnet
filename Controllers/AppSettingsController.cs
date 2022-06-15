using Utilities_aspnet.Utilities.Seeder;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
    private readonly IAppSettingRepository _appSettingRepository;
    private readonly ISeedRepository _seedRepository;

    public AppSettingsController(IAppSettingRepository appSettingRepository, ISeedRepository seedRepository) {
        _appSettingRepository = appSettingRepository;
        _seedRepository = seedRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
        GenericResponse? i = await _appSettingRepository.Read();
        return Ok(i);
    }
    
    [HttpGet("ReadLocation")]
    public async Task<ActionResult<GenericResponse<IEnumerable<LocationReadDto?>>>> ReadLocation() {
        GenericResponse? i = await _appSettingRepository.ReadLocation();
        return Ok(i);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("SeedLocation")]
    public async Task<ActionResult> SeedLocation() {
        await _seedRepository.SeedLocations();
        return Ok();
    }
    
    [HttpGet("SeedGenders")]
    public async Task<ActionResult> SeedGenders() {
        await _seedRepository.SeedGenders();
        return Ok();
    }
}