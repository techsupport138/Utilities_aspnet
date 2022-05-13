using Utilities_aspnet.Product;

namespace Utilities_aspnet.Tag;

public abstract class BaseTagEntity : BaseEntity {
    [StringLength(100)]
    public string? Title { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }

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
public class TagEntity : BaseTagEntity { }

[Table("Speciality")]
public class SpecialityEntity : BaseTagEntity { }

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