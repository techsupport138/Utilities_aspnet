namespace Utilities_aspnet.Entities;

[Table("Order")]
public class OrderEntity : BaseEntity {
	public string? Description { get; set; }
	public OrderStatuses? Status { get; set; }
	public decimal? Price { get; set; }
    public DateTime? PayDateTime { get; set; }
    public string? PayNumber { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
}

public class OrderDto {
	public string? Description { get; set; }
	public OrderStatuses? Status { get; set; }
	public decimal? Price { get; set; }
	public DateTime? PayDateTime { get; set; }
	public string? PayNumber { get; set; }
	public DateTime? ReceivedDate { get; set; }
	public UserMinimalReadDto? User { get; set; }

	public ProductReadDto? Product { get; set; }
}

public class OrderCreateUpdateDto {
	public Guid? Id { get; set; }
	public Guid? ProductId { get; set; }
	public string? Description { get; set; }
	public DateTime? ReceivedDate { get; set; }

}