namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
	private readonly IAppSettingRepository _appSettingRepository;
	private readonly DbContext _dbContext;

	public AppSettingsController(IAppSettingRepository appSettingRepository, DbContext dbContext) {
		_appSettingRepository = appSettingRepository;
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
		GenericResponse i = await _appSettingRepository.Read();
		return Ok(i);
	}

	[HttpPost("Gender")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> CreateGender(GenderEntity gender) {
		await _dbContext.Set<GenderEntity>().AddAsync(gender);
		return Ok();
	}
}