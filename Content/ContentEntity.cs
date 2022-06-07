namespace Utilities_aspnet.Content;

public class BaseContentEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }
    [Required]
    public ContentUseCase UseCase { get; set; }
    [Required]
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
    public IEnumerable<MediaEntity>? Media { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }
}

[Table("Contents")]
public class ContentEntity : BaseContentEntity { }

public class ContentReadDto : BaseReadDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase UseCase { get; set; }
    public IEnumerable<ContactInformationReadDto>? ContactInformation { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class ContentCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase UseCase { get; set; }
    public List<ContactInformationCreateUpdateDto>? ContactInformations { get; set; }
}