namespace Utilities_aspnet.Entities;

[Table("TopProducts")]
public class TopProductEntity : BaseEntity {
	
	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
}


public class TopProductReadDto {
    public Guid? Id { get; set; }
	public ProductEntity? Product { get; set; }
}

public class TopProductCreateDto {
	public Guid? ProductId { get; set; }
}
