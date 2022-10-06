namespace Utilities_aspnet.Entities;

[Table("Order")]
public class OrderEntity : BaseEntity {
	[StringLength(2000)]
	public string? Description { get; set; }

	public OrderStatuses? Status { get; set; }
	public double? TotalPrice { get; set; }
	public double? DiscountPrice { get; set; }
	public int? DiscountPercent { get; set; }

	[StringLength(500)]
	public string? DiscountCode { get; set; }

	public double? SendPrice { get; set; }
	public SendType? SendType { get; set; }
	public PayType? PayType { get; set; }
	public DateTime? PayDateTime { get; set; }

	[StringLength(500)]
	public string? PayNumber { get; set; }

	public DateTime? ReceivedDate { get; set; }

	public UserEntity? User { get; set; }

	[ForeignKey(nameof(User))]
	public string? UserId { get; set; }

	public UserEntity? ProductOwner { get; set; }

	[ForeignKey(nameof(ProductOwner))]
	public string? ProductOwnerId { get; set; }

	public IEnumerable<OrderDetailEntity>? OrderDetails { get; set; }
}

[Table("OrderDetail")]
public class OrderDetailEntity : BaseEntity {
	public OrderEntity? Order { get; set; }
	public Guid? OrderId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }

	public double? Price { get; set; }
	public int? Count { get; set; }

	public IEnumerable<CategoryEntity>? Categories { get; set; }
	public IEnumerable<FormEntity>? Forms { get; set; }
}

public class OrderReadDto {
	public Guid? Id { get; set; }
	public string? Description { get; set; }
	public OrderStatuses? Status { get; set; }

	public double? TotalPrice { get; set; }
	public double? DiscountPrice { get; set; }
	public int? DiscountPercent { get; set; }
	public string? DiscountCode { get; set; }

	public double? SendPrice { get; set; }
	public SendType? SendType { get; set; }

	public PayType? PayType { get; set; }
	public DateTime? PayDateTime { get; set; }
	public string? PayNumber { get; set; }
	public DateTime? ReceivedDate { get; set; }
	public UserReadDto? User { get; set; }
	public IEnumerable<OrderDetailReadDto>? OrderDetails { get; set; }
}

public class OrderDetailReadDto {
	public Guid? Id { get; set; }
	public double? Price { get; set; }
	public int? Count { get; set; }
	public ProductReadDto? Product { get; set; }
	public IEnumerable<CategoryEntity>? Categories { get; set; }
}

public class OrderCreateUpdateDto {
	public Guid? Id { get; set; }
	public string? Description { get; set; }
	public OrderStatuses? Status { get; set; }
	public DateTime? ReceivedDate { get; set; }

	public double? TotalPrice { get; set; }
	public int? DiscountPercent { get; set; }
	public string? DiscountCode { get; set; }
	public double? DiscountPrice { get; set; }
	public double? SendPrice { get; set; }

	public PayType? PayType { get; set; }
	public SendType? SendType { get; set; }

	public IEnumerable<OrderDetailCreateUpdateDto>? OrderDetails { get; set; }
}

public class OrderDetailCreateUpdateDto {
	public Guid? Id { get; set; }
	public Guid? ProductId { get; set; }
	public int? Count { get; set; }
	public IEnumerable<Guid>? Categories { get; set; }
}

public class OrderFilterDto {
	public string? Description { get; set; }
	public OrderStatuses? Status { get; set; }

	public double? TotalPrice { get; set; }
	public double? DiscountPrice { get; set; }
	public int? DiscountPercent { get; set; }
	public string? DiscountCode { get; set; }

	public double? SendPrice { get; set; }
	public SendType? SendType { get; set; }

	public PayType? PayType { get; set; }
	public DateTime? PayDateTime { get; set; }
	public string? PayNumber { get; set; }
	public DateTime? ReceivedDate { get; set; }
	public string? UserId { get; set; }
	public string? ProductOwnerId { get; set; }
	public int PageSize { get; set; } = 100;
	public int PageNumber { get; set; } = 1;
}