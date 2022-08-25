namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
	private readonly IAppSettingRepository _appSettingRepository;
	private readonly IProductRepositoryV2 _productRepository;
	private readonly IUserRepository _userRepository;

	public AppSettingsController(IAppSettingRepository appSettingRepository, IProductRepositoryV2 productRepository, IUserRepository userRepository) {
		_appSettingRepository = appSettingRepository;
		_productRepository = productRepository;
		_userRepository = userRepository;
	}

	[HttpGet]
	public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
		GenericResponse i = await _appSettingRepository.Read();
		return Ok(i);
	}
}