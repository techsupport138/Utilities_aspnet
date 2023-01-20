namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ClaimRequirement]
[Route("api/[controller]")]
public class MailSmsController : BaseApiController {
	private readonly IMailSmsRepository _repository;

	public MailSmsController(IMailSmsRepository repository) => _repository = repository;

	[HttpPost("SendMail")]
	public async Task<ActionResult<GenericResponse>> SendMail(SendMailDto dto) => Result(await _repository.SendMailSms(dto));
}