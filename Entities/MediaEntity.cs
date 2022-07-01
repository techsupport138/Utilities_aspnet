namespace Utilities_aspnet.Entities;

[Table("Media")]
public class MediaEntity : BaseEntity {
	public string? FileName { get; set; }
	public FileTypes? FileType { get; set; }
	public string? UseCase { get; set; }
	public string? Link { get; set; }
	public string? Title { get; set; }
	public VisibilityType? Visibility { get; set; } = VisibilityType.Public;

	public ContentEntity? Content { get; set; }
	public Guid? ContentId { get; set; }

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
	public CommentEntity? Comment { get; set; }
	public Guid? CommentId { get; set; }
	public ChatEntity? Chat { get; set; }
	public Guid? ChatId { get; set; }

	public NotificationEntity? Notification { get; set; }
	public Guid? NotificationId { get; set; }
	public CategoryEntity? Category { get; set; }
	public Guid? CategoryId { get; set; }
}

public class MediaDto {
	public string Id { get; set; }
	public FileTypes Type { get; set; }
	public string? UseCase { get; set; }
	public string? Link { get; set; }
	public string? Title { get; set; }
}

public class UploadDto {
	public string? UseCase { get; set; }
	public string? UserId { get; set; }
	public IEnumerable<IFormFile>? Files { get; set; }
	public IEnumerable<string>? Links { get; set; }
	public string? Title { get; set; }
	public VisibilityType? Visibility { get; set; } = VisibilityType.Public;
	public Guid? ProductId { get; set; }
	public Guid? ContentId { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? NotificationId { get; set; }
	public Guid? CommentId { get; set; }
	public Guid? ChatId { get; set; }
}