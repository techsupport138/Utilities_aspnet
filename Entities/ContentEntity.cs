namespace Utilities_aspnet.Entities;

[Table("Contents")]
public class ContentEntity : BaseEntity {
	[StringLength(500)]
	public string? Title { get; set; }

	[StringLength(500)]
	public string? SubTitle { get; set; }

	[StringLength(2000)]
	public string? Description { get; set; }

	[StringLength(500)]
	public string? UseCase { get; set; }

	public IEnumerable<MediaEntity>? Media { get; set; }
}