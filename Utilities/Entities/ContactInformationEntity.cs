namespace Utilities_aspnet.Utilities.Entities;

[Table("ContactInformations")]
public class ContactInformationEntity : BaseEntity {
    [Required]
    public string Value { get; set; }

    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.Public;

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

    public Guid? ContactInfoItemId { get; set; }
    public virtual ContactInfoItemEntity ContactInfoItem { get; set; }

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
}

public class ContactInformationReadDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public IdTitleReadDto? ContactInfoItem { get; set; }
    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
}

public class ContactInformationCreateUpdateDto
{
    public Guid? Id { get; set; }
    public string Value { get; set; }
    public Guid ContactInfoItemId { get; set; }
    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
}