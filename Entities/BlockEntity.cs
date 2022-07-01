namespace Utilities_aspnet.Entities;

[Table("Blocks")]
public class BlockEntity : BaseEntity {
	public string? BlockedUserId { get; set; }
	public UserEntity? BlockedUser { get; set; }

	public string? UserId { get; set; }
	public UserEntity? User { get; set; }
}