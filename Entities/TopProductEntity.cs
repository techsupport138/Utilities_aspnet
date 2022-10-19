namespace Utilities_aspnet.Entities;

[Table("TopProducts")]
public class TopProductEntity : BaseEntity {
	public UserEntity? User { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public Guid? ProductId { get; set; }
}

public class TopProductCreateDto {
	public Guid? ProductId { get; set; }
}