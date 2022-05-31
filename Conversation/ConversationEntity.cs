namespace Utilities_aspnet.Conversation;

public class ConversationEntity : BaseEntity
{
    [StringLength(450)]
    [ForeignKey(nameof(FromUser))]
    public string FromUserId { get; set; } = null!;

    [StringLength(450)]
    [ForeignKey(nameof(ToUser))]
    public string ToUserId { get; set; } = null!;

    [StringLength(500)]
    public string MessageText { get; set; } = null!;

    public virtual UserEntity FromUser { get; set; } = null!;
    public virtual UserEntity ToUser { get; set; } = null!;
}

public class ConversationsDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string? MessageText { get; set; }
    public string? FullName { get; set; }
    public DateTime DateTime { get; set; }
    public string? ProfileImage { get; set; }
    public bool Send { get; set; }
}

public class AddConversationDto
{
    public string UserId { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}