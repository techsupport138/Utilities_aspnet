using Utilities_aspnet.Category;
using Utilities_aspnet.User;

namespace Utilities_aspnet.Utilities.Entities;

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
    public Guid? AdsId { get; set; }
    public Guid? DailyPriceId { get; set; }
    public Guid? JobId { get; set; }
    public Guid? LearnId { get; set; }
    public Guid? PostId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? MagazineId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? ContentId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? NotificationId { get; set; }
}