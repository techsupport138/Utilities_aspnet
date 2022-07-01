namespace Utilities_aspnet.Entities;

public class BaseEntity {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; } = Guid.NewGuid();

	public DateTime? CreatedAt { get; set; } = DateTime.Now;
	public DateTime? UpdatedAt { get; set; } = DateTime.Now;
	public DateTime? DeletedAt { get; set; }
}

public class BaseReadDto {
	public Guid? Id { get; set; }
	public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}