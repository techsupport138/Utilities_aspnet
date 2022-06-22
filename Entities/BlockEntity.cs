namespace Utilities_aspnet.Entities;

[Table("Blocks")]
public class BlockEntity : BaseEntity {
	public string? BlockedUserId { get; set; }
	public UserEntity? BlockedUser { get; set; }

	public string? UserId { get; set; }
	public UserEntity? User { get; set; }
}


public class BlockReadDto {
	public IEnumerable<UserReadDto>? Blocks { get; set; }
}

public class BlockCreateDto {
	public string UserId { get; set; } = null!;
}