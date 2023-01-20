namespace Utilities_aspnet.Entities; 

public class SendMailDto {
	public string PlainText { get; set; }
	public string Html { get; set; }
	public string From { get; set; }
	public string To { get; set; }

}