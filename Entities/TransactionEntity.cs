namespace Utilities_aspnet.Entities;

[Table("Transactions")]
public class TransactionEntity : BaseEntity {
	public double? Amount { get; set; }

	[StringLength(500)]
	public string? Descriptions { get; set; }

	[StringLength(500)]
	public string? Authority { get; set; }

	[StringLength(500)]
	public string? GatewayName { get; set; }

	[StringLength(500)]
	public string? PaymentId { get; set; }

	public long? RefId { get; set; }

	public TransactionStatus? StatusId { get; set; } = TransactionStatus.Pending;

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }
	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
}