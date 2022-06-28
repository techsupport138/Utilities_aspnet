namespace Utilities_aspnet.Entities;

[Table("Chats")]
public class ChatEntity : BaseEntity {
	[ForeignKey(nameof(FromUser))]
	public string FromUserId { get; set; } = null!;

	public UserEntity FromUser { get; set; } = null!;

	[ForeignKey(nameof(ToUser))]
	public string ToUserId { get; set; } = null!;

	public UserEntity ToUser { get; set; } = null!;

	public string MessageText { get; set; } = null!;
}

public class ChatReadDto {
	public Guid Id { get; set; }
	public string UserId { get; set; } = null!;
	public string? MessageText { get; set; }
	public string? FullName { get; set; }
	public string? PhoneNumber { get; set; }
	public DateTime? DateTime { get; set; }
	public string? ProfileImage { get; set; }
	public bool Send { get; set; }
}

public class ChatCreateUpdateDto {
	public string UserId { get; set; } = null!;
	public string MessageText { get; set; } = null!;
}