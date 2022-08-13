namespace Utilities_aspnet.Entities;

[Table("Discount")]
public class DiscountEntity : BaseEntity
{
	public string? Title { get; set; }
	public int? DiscountPercent { get; set; }
	public int? NumberUses { get; set; }
	public string? Code { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}

public class DiscountReadDto
{
	public Guid? Id { get; set; }	 
	public string? Title { get; set; }
	public int? DiscountPercent { get; set; }
	public int? NumberUses { get; set; }
	public string? Code { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}

public class DiscountCreateUpdateDto
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public int? DiscountPercent { get; set; }
	public int? NumberUses { get; set; }
	public string? Code { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}
