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
	public async Task<ActionResult<GenericResponse<EnumDto>>> Read() => Result(await _appSettingRepository.Read());

	[HttpPost("Gender")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> CreateGender(GenderEntity gender) {
		await _dbContext.Set<GenderEntity>().AddAsync(gender);
		await _dbContext.SaveChangesAsync();
		return Ok();
	}
}