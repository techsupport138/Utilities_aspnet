namespace Utilities_aspnet.Entities;

[Table("Reports")]
public class ReportEntity : BaseEntity {
	public string? Title { get; set; }
	public string? Description { get; set; }

	public string? UserId { get; set; }
	public UserEntity? User { get; set; }
	public string? CreatorUserId { get; set; }
	public UserEntity? CreatorUser { get; set; }

	public Guid? ProductId { get; set; }
	public ProductEntity? Product { get; set; }
}

public class ReportCreateDto : BaseReadDto {
	public string? Title { get; set; }
	public string? Description { get; set; }
	public Guid? ProductId { get; set; }
	public string? UserId { get; set; }
}

public class ReportReadDto : BaseReadDto {
	public string? Title { get; set; }
	public string? Description { get; set; }

	public UserEntity? User { get; set; }
	public ProductEntity? Product { get; set; }
}

public class ReportFilterDto : BaseReadDto {
	public bool? User { get; set; }
	public bool? Product { get; set; }
}