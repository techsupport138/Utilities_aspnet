namespace Utilities_aspnet.Conversation; 

public class ConversationEntity : BaseEntity {
    [StringLength(450)]
    [ForeignKey(nameof(FromUserNavigation))]
    public string FromUserId { get; set; } = null!;

    [StringLength(450)]
    [ForeignKey(nameof(ToUserNavigation))]
    public string ToUserId { get; set; } = null!;

    [StringLength(500)]
    public string MessageText { get; set; } = null!;

    public virtual UserEntity FromUserNavigation { get; set; } = null!;
    public virtual UserEntity ToUserNavigation { get; set; } = null!;
}

public class ConversationsDto {
    public Guid Id { get; set; }

    [StringLength(450)]
    public string UserId { get; set; }

    [StringLength(500)]
    public string MessageText { get; set; }

    public string Name { get; set; }
    public DateTime DateTime { get; set; }
    public string ImageUrl { get; set; }
    public bool Send { get; set; }
}

public class AddConversationDto {
    [StringLength(450)]
    public string UserId { get; set; }

    [StringLength(500)]
    public string MessageText { get; set; }
}