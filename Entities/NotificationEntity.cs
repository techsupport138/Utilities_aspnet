namespace Utilities_aspnet.Entities;

[Table("Notifications")]
public class NotificationEntity : BaseEntity {
	[StringLength(500)]
	public string? Title { get; set; }

	[StringLength(2000)]
	public string? Message { get; set; }

	[StringLength(500)]
	public string? Link { get; set; }

	[StringLength(500)]
	public string? UseCase { get; set; }
	
	public SeenStatus? SeenStatus { get; set; }

	public bool? Visited { get; set; }
	public IEnumerable<MediaEntity>? Media { get; set; }

	public string? UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public UserEntity? User { get; set; }

	public string? CreatorUserId { get; set; }

	[ForeignKey(nameof(CreatorUserId))]
	public UserEntity? CreatorUser { get; set; }
	
}

public class NotificationCreateUpdateDto {
	public string? Title { get; set; }
	public string? UserId { get; set; }
	public string? CreatorUserId { get; set; }
	public string? Message { get; set; }
	public string? Link { get; set; }
	public string? Media { get; set; }
	public string? UseCase { get; set; }
}