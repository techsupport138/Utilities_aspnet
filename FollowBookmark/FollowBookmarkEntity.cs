namespace Utilities_aspnet.FollowBookmark;

public class FollowEntity : BaseEntity {
    public string? SourceUserId { get; set; }
    public UserEntity? SourceUser { get; set; }

    public string? TargetUserId { get; set; }
    public UserEntity? TargetUser { get; set; }
}

public class BookmarkEntity : BaseEntity {
    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public ProjectEntity? Project { get; set; }
    public Guid? ProjectId { get; set; }

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
    
    public ColorEntity? Color { get; set; }
    public Guid? ColorId { get; set; }
}

public class BookmarkWriteDto
{
    public Guid? ProductId { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? TutorialId { get; set; }

    public Guid? EventId { get; set; }

    public Guid? AdId { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? TenderId { get; set; }

    public Guid? ServiceId { get; set; }

    public Guid? MagazineId { get; set; }

    public Guid? TagId { get; set; }

    public Guid? SpecialityId { get; set; }
    
    public Guid? ColorId { get; set; }
}