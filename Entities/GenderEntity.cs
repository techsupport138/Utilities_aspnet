namespace Utilities_aspnet.Entities;

[Table("Gender")]
public class GenderEntity {
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(500)]
	public string Title { get; set; } = null!;

	[StringLength(500)]
	public string? TitleTr1 { get; set; }
}