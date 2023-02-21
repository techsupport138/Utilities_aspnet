using SendGrid;
using SendGrid.Helpers.Mail;

namespace Utilities_aspnet.Repositories;

public interface IMailSmsRepository
{
	public Task<GenericResponse> SendMailSms(SendMailDto dto);
}

public class MailSmsRepository : IMailSmsRepository
{
	public async Task<GenericResponse> SendMailSms(SendMailDto dto)
	{
		try
		{
			SendGridClientOptions sendGridClientOptions = new()
			{
				ApiKey = "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",
				HttpErrorAsException = true
			};
			SendGridClient client = new(sendGridClientOptions);
			EmailAddress from = new(dto.From, "armansami");
			const string subject = "Sending with SendGrid is Fun";
			EmailAddress to = new(dto.To, "Example User");
			SendGridMessage? msg = MailHelper.CreateSingleEmail(from, to, subject, dto.PlainText, dto.Html);
			await client.SendEmailAsync(msg);
			return new GenericResponse();
		}
		catch (Exception ex)
		{
			return new GenericResponse(UtilitiesStatusCodes.Unhandled, ex.ToString());
		}
	}
}