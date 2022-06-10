using Utilities_aspnet.User;

namespace Utilities_aspnet.FollowBookmark;

[Table("Follows")]
public class FollowEntity : BaseEntity {
    public string? SourceUserId { get; set; }
    public UserEntity? SourceUser { get; set; }

    public string? TargetUserId { get; set; }
    public UserEntity? TargetUser { get; set; }
}

[Table("Bookmarks")]
public class BookmarkEntity : BaseEntity {
    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public DailyPriceEntity? DailyPrice { get; set; }
    public Guid? DailyPriceId { get; set; }

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

public class BookmarkCreateDto {
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

public class BookmarkReadDto {
    public IEnumerable<UserReadDto>? Users { get; set; }
    public IEnumerable<ProductReadDto>? Products { get; set; }
    public IEnumerable<ProductReadDto>? DailyPrices { get; set; }
    public IEnumerable<ProductReadDto>? Projects { get; set; }
    public IEnumerable<ProductReadDto>? Tutorials { get; set; }
    public IEnumerable<ProductReadDto>? Events { get; set; }
    public IEnumerable<ProductReadDto>? Ads { get; set; }
    public IEnumerable<ProductReadDto>? Companies { get; set; }
    public IEnumerable<ProductReadDto>? Tenders { get; set; }
    public IEnumerable<ProductReadDto>? Services { get; set; }
    public IEnumerable<ProductReadDto>? Magazines { get; set; }
    public IEnumerable<IdTitleReadDto>? Tags { get; set; }
    public IEnumerable<IdTitleReadDto>? Specialities { get; set; }
    public IEnumerable<IdTitleReadDto>? Colors { get; set; }
}