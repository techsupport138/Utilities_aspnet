namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
	private readonly IAppSettingRepository _appSettingRepository;
	private readonly ISeedRepository _seedRepository;
	private readonly IProductRepository _productRepository;
	private readonly IUserRepository _userRepository;

	public AppSettingsController(
		IAppSettingRepository appSettingRepository,
		ISeedRepository seedRepository,
		IProductRepository productRepository,
		IUserRepository userRepository) {
		_appSettingRepository = appSettingRepository;
		_seedRepository = seedRepository;
		_productRepository = productRepository;
		_userRepository = userRepository;
	}

	[HttpGet]
	public async Task<ActionResult<GenericResponse<EnumDto>>> Read() {
		GenericResponse i = await _appSettingRepository.Read();
		return Ok(i);
	}

	[HttpGet("ReadLocation")]
	public async Task<ActionResult<GenericResponse<IEnumerable<LocationReadDto?>>>> ReadLocation() {
		GenericResponse i = await _appSettingRepository.ReadLocation();
		return Ok(i);
	}

	[ApiExplorerSettings(IgnoreApi = true)]
	[HttpGet("SeedLocation")]
	public async Task<ActionResult> SeedLocation() {
		await _seedRepository.SeedLocations();
		return Ok();
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("SeedGenders")]
	public async Task<ActionResult> SeedGenders() {
		await _seedRepository.SeedGenders();
		return Ok();
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("SeedProducts")]
	public async Task<ActionResult<GenericResponse>> SeedProducts(SeederProductDto dto)
		=> Result(await _productRepository.SeederProduct(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("SeedCategories")]
	public async Task<ActionResult<GenericResponse>> SeedCategories(SeederCategoryDto dto)
		=> Result(await _seedRepository.SeedCategories(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("SeedUsers")]
	public async Task<ActionResult<GenericResponse>> SeedUsers(SeederUserDto dto)
		=> Result(await _userRepository.SeederUser(dto));
	
}