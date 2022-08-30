namespace Utilities_aspnet.Entities;

public class BaseEntity {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; } = Guid.NewGuid();

	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime UpdatedAt { get; set; } = DateTime.Now;
	public DateTime? DeletedAt { get; set; }
}