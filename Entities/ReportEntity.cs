namespace Utilities_aspnet.Entities;

[Table("Reports")]
public class ReportEntity : BaseEntity {
	[MaxLength(500)]
	public string? Title { get; set; }

	[MaxLength(500)]
	public string? Description { get; set; }

	public string? UserId { get; set; }
	public UserEntity? User { get; set; }
	public string? CreatorUserId { get; set; }
	public UserEntity? CreatorUser { get; set; }

	public Guid? ProductId { get; set; }
	public ProductEntity? Product { get; set; }
}

public class ReportFilterDto {
	public bool? User { get; set; }
	public bool? Product { get; set; }
}