namespace Utilities_aspnet.Entities;

[Table("Notifications")]
public class NotificationEntity : BaseEntity {
	public string? Title { get; set; } = null!;
	public string? Message { get; set; }
	public string? Link { get; set; }
	public string? UseCase { get; set; }
	public bool? Visited { get; set; }
	public IEnumerable<MediaEntity>? Media { get; set; }

	public string? UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public UserEntity? User { get; set; }

	public string? CreatorUserId { get; set; }

	[ForeignKey(nameof(CreatorUserId))]
	public UserEntity? CreatorUser { get; set; }
}

public class NotificationDto {
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public string? Message { get; set; }
	public string? Link { get; set; }
	public DateTime CreatedAt { get; set; }
	public bool Visited { get; set; }
	public string? UseCase { get; set; }
	public UserReadDto? CreatorUser { get; set; }
	public IEnumerable<MediaEntity>? Media { get; set; }
}

public class NotificationCreateUpdateDto {
	public Guid? Id { get; set; } = null!;
	public string? Title { get; set; } = null!;
	public string? UserId { get; set; }
	public string? CreatorUserId { get; set; }
	public string? Message { get; set; }
	public string? Link { get; set; }
	public string? Media { get; set; }
	public string? UseCase { get; set; }
}