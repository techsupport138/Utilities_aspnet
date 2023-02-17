namespace Utilities_aspnet.Entities;

[Table("Reports")]
public class ReportEntity : BaseEntity
{
	[StringLength(500)]
	public string? Title { get; set; }

	[StringLength(500)]
	public string? Description { get; set; }

	public string? UserId { get; set; }
	public UserEntity? User { get; set; }
	public string? CreatorUserId { get; set; }
	public UserEntity? CreatorUser { get; set; }

	public Guid? ProductId { get; set; }
	public ProductEntity? Product { get; set; }
	public ReportType ReportType { get; set; } = ReportType.All;
	[StringLength(500)]
	public string? ProductUseCase { get; set; }
}

public class ReportFilterDto
{
	public bool? User { get; set; }
	public bool? Product { get; set; }
	public ReportType ReportType { get; set; }
	public string? UserId { get; set; }
	public string? CreatorUserId { get; set; }
	public int? Count { get; set; }
}

public class ReportResponseDto
{
	public string? Title { get; set; }
	public int? Month { get; set; }
	public int? Year { get; set; }
	public int? Day { get; set; }
	public int? Count { get; set; }
	public double? Total { get; set; }
	public Guid? ProductId { get; set; }
}