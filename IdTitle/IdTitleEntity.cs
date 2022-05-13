using Utilities_aspnet.Product;

namespace Utilities_aspnet.IdTitle;

public class BaseIdTitleEntity : BaseEntity {
    [Required]
    public string Title { get; set; }

    public string? Color { get; set; }

    public ICollection<MediaEntity> Media { get; set; }

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

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
}

[Table("Tags")]
public class TagEntity : BaseIdTitleEntity { }

[Table("Speciality")]
public class SpecialityEntity : BaseIdTitleEntity { }

[Table("Favorite")]
public class FavoriteEntity : BaseIdTitleEntity { }

[Table("Colors")]
public class ColorEntity : BaseIdTitleEntity { }

[Table("ContactInfoItems")]
public class ContactInfoItemEntity : BaseIdTitleEntity { }

public class TagReadDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
}

public class TagCreateUpdateDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
}

public class TagProfile : Profile {
    public TagProfile() {
        CreateMap<TagEntity, TagCreateUpdateDto>().ReverseMap();
        CreateMap<TagEntity, TagReadDto>().ReverseMap();
    }
}