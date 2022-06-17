namespace Utilities_aspnet.Notification;

[Table("Notifications")]
public class NotificationEntity : BaseEntity {
    public string? Title { get; set; } = null!;
    public string? Message { get; set; }
    public string? Link { get; set; }
    public string? UseCase { get; set; }
    public bool? Visited { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }

    public string? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
}

public class NotificationDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Link { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Visited { get; set; }
    public string? UseCase { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class NotificationCreateUpdateDto {
    public Guid? Id { get; set; } = null!;
    public string? Title { get; set; } = null!;
    public string? UserId { get; set; }
    public string? Message { get; set; }
    public string? Link { get; set; }
    public string? Media { get; set; }
    public string? UseCase { get; set; }
}