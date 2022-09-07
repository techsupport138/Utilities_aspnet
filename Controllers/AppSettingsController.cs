namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : BaseApiController {

	[HttpGet]
	public static ActionResult<GenericResponse<EnumDto>> Read() {
		EnumDto model = new() {
			FormFieldType = EnumExtension.GetValues<FormFieldType>(),
			TransactionStatuses = EnumExtension.GetValues<TransactionStatus>(),
			UtilitiesStatusCodes = EnumExtension.GetValues<UtilitiesStatusCodes>(),
			OtpResult = EnumExtension.GetValues<OtpResult>(),
			DatabaseType = EnumExtension.GetValues<DatabaseType>(),
			OrderStatuses = EnumExtension.GetValues<OrderStatuses>(),
			PayType = EnumExtension.GetValues<PayType>(),
			SendType = EnumExtension.GetValues<SendType>(),
			ProductStatus = EnumExtension.GetValues<ProductStatus>(),
			Sender = EnumExtension.GetValues<Sender>()
		};
		return Result(new GenericResponse<EnumDto?>(model));
	}
}