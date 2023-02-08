namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {
	private readonly IConfiguration _config;

	public AppSettingsController(IConfiguration config) => _config = config;

	[HttpGet]
	public ActionResult<GenericResponse<EnumDto>> Read() {
		AppSettings appSettings = new();
		_config.GetSection("AppSettings").Bind(appSettings);
		string hashedDateTime = Encryption
			.GetMd5HashData($"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}SinaMN75").ToLower();
		return Result(new GenericResponse<EnumDto?>(new EnumDto {
			DateTime = DateTime.Now,
			HashedDateTime = hashedDateTime,
			FormFieldType = EnumExtension.GetValues<FormFieldType>(),
			TransactionStatuses = EnumExtension.GetValues<TransactionStatus>(),
			UtilitiesStatusCodes = EnumExtension.GetValues<UtilitiesStatusCodes>(),
			OtpResult = EnumExtension.GetValues<OtpResult>(),
			DatabaseType = EnumExtension.GetValues<DatabaseType>(),
			OrderStatuses = EnumExtension.GetValues<OrderStatuses>(),
			PayType = EnumExtension.GetValues<PayType>(),
			SendType = EnumExtension.GetValues<SendType>(),
			ProductStatus = EnumExtension.GetValues<ProductStatus>(),
			Currency = EnumExtension.GetValues<Currency>(),
			SeenStatus = EnumExtension.GetValues<SeenStatus>(),
			Priority = EnumExtension.GetValues<Priority>(),
			ChatStatus = EnumExtension.GetValues<ChatStatus>(),
			AppSettings = appSettings
			
		}));
	}
}
