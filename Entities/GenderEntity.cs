namespace Utilities_aspnet.Entities;

[Table("Gender")]
public class GenderEntity {
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(100)]
	public string Title { get; set; } = null!;

	[MaxLength(100)]
	public string? TitleTr1 { get; set; }
}