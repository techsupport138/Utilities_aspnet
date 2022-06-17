namespace Utilities_aspnet.Utilities.Entities;

[Table("Gender")]
public class GenderEntity {
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(200)]
	public string Title { get; set; } = null!;

	[StringLength(200)]
	public string? TitleTr1 { get; set; }
}