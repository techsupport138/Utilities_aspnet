namespace Utilities_aspnet.Utilities.Entities;

[Table("Media")]
public class MediaEntity : BaseEntity {
    public string? FileName { get; set; }
    public FileTypes? FileType { get; set; }
    public MediaUseCase? UseCase { get; set; } = MediaUseCase.Main;
    public string? Link { get; set; }

    public ContentEntity? Content { get; set; }
    public Guid? ContentId { get; set; }

    public ContactInfoItemEntity? ContactInfoItem { get; set; }
    public Guid? ContactInfoItemId { get; set; }

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public ProjectEntity? Project { get; set; }
    public Guid? ProjectId { get; set; }

    public DailyPriceEntity? DailyPrice { get; set; }
    public Guid? DailyPriceId { get; set; }

    public TutorialEntity? Tutorial { get; set; }
    public Guid? TutorialId { get; set; }

    public EventEntity? Event { get; set; }
    public Guid? EventId { get; set; }

    public AdEntity? Ad { get; set; }
    public Guid? AdId { get; set; }

    public CompanyEntity? Company { get; set; }
    public Guid? CompanyId { get; set; }

    public TenderEntity? Tender { get; set; }
    public Guid? TenderId { get; set; }

    public ServiceEntity? Service { get; set; }
    public Guid? ServiceId { get; set; }

    public MagazineEntity? Magazine { get; set; }
    public Guid? MagazineId { get; set; }

    public TagEntity? Tag { get; set; }
    public Guid? TagId { get; set; }

    public SpecialityEntity? Speciality { get; set; }
    public Guid? SpecialityId { get; set; }

    public NotificationEntity? Notification { get; set; }
    public Guid? NotificationId { get; set; }
}

public class MediaDto {
    public string Id { get; set; }
    public FileTypes Type { get; set; }
    public MediaUseCase UseCase { get; set; } = MediaUseCase.Main;
    public string? Link { get; set; }
}

public class UploadDto {
    public MediaUseCase UseCase { get; set; } = MediaUseCase.Main;
    public string? UserId { get; set; }
    public List<IFormFile>? Files { get; set; }
    public List<string>? Links { get; set; }
    public Guid? AdsId { get; set; }
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
}

public enum MediaUseCase {
    Main = 100,
    Slider = 101,
    Media = 102
}