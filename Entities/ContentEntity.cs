namespace Utilities_aspnet.Entities;

[Table("Contents")]
public class ContentEntity : BaseEntity {
	[MaxLength(500)]
	public string? Title { get; set; }

	[MaxLength(500)]
	public string? SubTitle { get; set; }

	[MaxLength(2000)]
	public string? Description { get; set; }

	[MaxLength(500)]
	public string? UseCase { get; set; }

	public IEnumerable<MediaEntity>? Media { get; set; }
}