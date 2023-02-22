using PostmarkDotNet;
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
			// Send an email asynchronously:
			var message = new PostmarkMessage()
			{
				To = dto.To,
				From = "Support@toubasources.com",
				//TrackOpens = true,
				Subject = "Touba Technical Mail",
				TextBody = dto.PlainText,
				//HtmlBody = "<strong>Hello</strong> "+ dto.PlainText,
				//MessageStream = "outbound",
				//Tag = "New Year's Email Campaign",
			};

			//var imageContent = File.ReadAllBytes("test.jpg");
			//message.AddAttachment(imageContent, "test.jpg", "image/jpg", "cid:embed_name.jpg");

			var client = new PostmarkClient("00ed8f38-a980-4201-bddd-6621fb77eaa6");
			var sendResult = await client.SendMessageAsync(message);

			if (sendResult.Status == PostmarkStatus.Success)
			{
				/* Handle success */
				return new GenericResponse(UtilitiesStatusCodes.Success);
			}
			else
			{
				/* Resolve issue.*/
				return new GenericResponse(UtilitiesStatusCodes.Unhandled);
			}
			#region SendGrid
			//SendGridClientOptions sendGridClientOptions = new()
			//{
			//	ApiKey = "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",
			//	HttpErrorAsException = true
			//};
			//SendGridClient client = new(sendGridClientOptions);
			//EmailAddress from = new(dto.From, "armansami");
			//const string subject = "Sending with SendGrid is Fun";
			//EmailAddress to = new(dto.To, "Example User");
			//SendGridMessage? msg = MailHelper.CreateSingleEmail(from, to, subject, dto.PlainText, dto.Html);
			//await client.SendEmailAsync(msg);
			//return new GenericResponse();
			#endregion

		}
		catch (Exception ex)
		{
			return new GenericResponse(UtilitiesStatusCodes.Unhandled, ex.ToString());
		}
	}

}