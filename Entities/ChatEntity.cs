namespace Utilities_aspnet.Entities;

[Table("Chats")]
public class ChatEntity : BaseEntity {
	[ForeignKey(nameof(FromUser))]
	public string FromUserId { get; set; } = null!;

	public UserEntity FromUser { get; set; } = null!;

	[ForeignKey(nameof(ToUser))]
	public string ToUserId { get; set; } = null!;

	public UserEntity ToUser { get; set; } = null!;

	[StringLength(2000)]
	public string MessageText { get; set; } = null!;

	public bool ReadMessage { get; set; }
	public IEnumerable<MediaEntity>? Media { get; set; }
}

[Table("GroupChat")]
public class GroupChatEntity : BaseEntity {
	[StringLength(500)]
	public string? Title { get; set; }

	[StringLength(500)]
	public string? Description { get; set; }

	[StringLength(500)]
	public string? Value { get; set; }

	[StringLength(500)]
	public string? Type { get; set; }

	[StringLength(500)]
	public string? UseCase { get; set; }

	[StringLength(500)]
	public string? Department { get; set; }

	public ChatStatus? ChatStatus { get; set; }

	public Priority? Priority { get; set; }

	public IEnumerable<MediaEntity>? Media { get; set; }
	public IEnumerable<UserEntity>? Users { get; set; }
	public IEnumerable<ProductEntity>? Products { get; set; }
	public IEnumerable<GroupChatMessageEntity>? GroupChatMessage { get; set; }
}

[Table("GroupChatMessage")]
public class GroupChatMessageEntity : BaseEntity {
	[StringLength(2000)]
	public string? Message { get; set; }

	[StringLength(500)]
	public string? Type { get; set; }

	[StringLength(500)]
	public string? UseCase { get; set; }

	public GroupChatEntity? GroupChat { get; set; }
	public Guid? GroupChatId { get; set; }

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public IEnumerable<MediaEntity>? Media { get; set; }
}

public class ChatReadDto {
	public Guid Id { get; set; }
	public string UserId { get; set; } = null!;
	public string? MessageText { get; set; }
	public DateTime? DateTime { get; set; }
	public bool Send { get; set; }
	public int? UnReadMessages { get; set; } = 0;
	public IEnumerable<MediaEntity>? Media { get; set; }
	public UserEntity? User { get; set; }
}

public class ChatCreateUpdateDto {
	public string? UserId { get; set; } = null!;
	public string MessageText { get; set; } = null!;
}

public class GroupChatCreateUpdateDto {
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Value { get; set; }
	public string? Type { get; set; }
	public string? UseCase { get; set; }
	public string? Department { get; set; }
	public ChatStatus? ChatStatus { get; set; }
	public Priority? Priority { get; set; }
	public IEnumerable<string>? UserIds { get; set; }
	public IEnumerable<Guid>? ProductIds { get; set; }
}

public class GroupChatMessageCreateUpdateDto {
	public string? Message { get; set; }
	public string? Type { get; set; }
	public string? UseCase { get; set; }
	public Guid? GroupChatId { get; set; }
}