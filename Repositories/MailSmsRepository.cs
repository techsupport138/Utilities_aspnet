using SendGrid;
using SendGrid.Helpers.Mail;

namespace Utilities_aspnet.Repositories;

public interface IMailSmsRepository {
	public Task<GenericResponse> SendMailSms(SendMailDto dto);
}

public class MailSmsRepository : IMailSmsRepository {
	public async Task<GenericResponse> SendMailSms(SendMailDto dto) {
		try {
			SendGridClientOptions sendGridClientOptions = new() {
				ApiKey = "SG.y6cEFDU8TCmw4MUlyWEQzA.47sp-0TBDlEgZxmxRwd7fGCxfs1bGsaVKmmxG5c6oHA4",
				HttpErrorAsException = true
			};
			SendGridClient client = new(sendGridClientOptions);
			EmailAddress from = new(dto.From, "Example User");
			const string subject = "Sending with SendGrid is Fun";
			EmailAddress to = new(dto.To, "Example User");
			SendGridMessage? msg = MailHelper.CreateSingleEmail(from, to, subject, dto.PlainText, dto.Html);
			await client.SendEmailAsync(msg);
			return new GenericResponse();
		}
		catch (Exception ex) {
			return new GenericResponse(UtilitiesStatusCodes.Unhandled,ex.ToString());
		}
	}
}