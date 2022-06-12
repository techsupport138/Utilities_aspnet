using Utilities_aspnet.User;

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
    public bool? IsRead { get; set; }
}

[Table("ChatGroups")]
public class ChatGroupEntity : BaseEntity
{
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public bool Private { get; set; }
    [StringLength(450)]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = default!;    
    public virtual UserEntity Owner { get; set; } = null!;
    public virtual IEnumerable<ChatGroupMemberEntity>? ChatGroupMembers { get; set; }
    public virtual IEnumerable<ChatEntity>? Messages { get; set; }
}

[Table("ChatGroupMember")]
public class ChatGroupMemberEntity : BaseEntity
{
    [StringLength(450)]
    [ForeignKey(nameof(ChatGroup))]
    public Guid ChatGroupId { get; set; }
    [StringLength(450)]
    [ForeignKey(nameof(Memeber))]
    public string MemebrId { get; set; }

    public virtual ChatGroupEntity ChatGroup { get; set; }
    public virtual UserEntity Memeber { get; set; }
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

public class ChatGroupReadDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public bool Private { get; set; }
    public string? OwnerId { get; set; }
    public DateTime DateTime { get; set; }
    public ChatReadDto? LastMessage { get; set; }
    public virtual IEnumerable<ChatReadDto>? Messages { get; set; }
}

public class ChatGroupCreateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public bool Private { get; set; }
    public string? OwnerId { get; set; }
}

public class AddMemberToGroup
{
    public Guid GroupId { get; set; }
    public IEnumerable<string>? MemberIds { get; set; }

}