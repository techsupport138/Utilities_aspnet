namespace Utilities_aspnet.Chat;

[Table("Chats")]
public class ChatEntity : BaseEntity {
    [StringLength(450)]
    [ForeignKey(nameof(FromUser))]
    public string FromUserId { get; set; } = null!;

    public virtual UserEntity FromUser { get; set; } = null!;

    [StringLength(450)]
    [ForeignKey(nameof(ToUser))]
    public string ToUserId { get; set; } = null!;

    public virtual UserEntity ToUser { get; set; } = null!;

    public string MessageText { get; set; } = null!;
}

public class ChatReadDto {
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string? MessageText { get; set; }
    public string? FullName { get; set; }
    public DateTime DateTime { get; set; }
    public string? ProfileImage { get; set; }
    public bool Send { get; set; }
}

public class ChatCreateUpdateDto {
    public string UserId { get; set; } = null!;
    public string? MessageId { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}