namespace Utilities_aspnet.Controllers; 


[ApiController]
[Route("api/[controller]")]
public class SignalRHubController : BaseApiController {
    // public readonly StartupExtension.UtilitiesHub Type { get; set; }
    

    public SignalRHubController() {

    }

    // [HttpGet]
    // public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
    //     GenericResponse? i = await _appSettingRepository.Read();
    //     return Ok(i);
    // }
    //
    // [HttpGet("ReadLocation")]
    // public async Task<ActionResult<GenericResponse<IEnumerable<LocationReadDto?>>>> ReadLocation() {
    //     GenericResponse? i = await _appSettingRepository.ReadLocation();
    //     return Ok(i);
    // }
}