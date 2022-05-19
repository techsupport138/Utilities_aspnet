namespace Utilities_aspnet.IdTitle;

public class BaseIdTitleEntity : BaseEntity {
    // public Guid? ParentId { get; set; }
    // public BaseIdTitleEntity? Parent { get; set; }

    [Required]
    public string Title { get; set; }
    
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }

    public IdTitleUseCase UseCase { get; set; } = IdTitleUseCase.Null;

    public ICollection<MediaEntity> Media { get; set; }

    
    // public string UserId { get; set; }
    // public UserEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public ProductEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public ProjectEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public TutorialEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public EventEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public AdEntity User { get; set; }
    //
    // public string UserId { get; set; }
    // public CompanyEntity User { get; set; }    
    //
    // public string UserId { get; set; }
    // public TenderEntity User { get; set; }    
    //
    // public string UserId { get; set; }
    // public ServiceEntity User { get; set; }    
    //
    // public string MagazineId { get; set; }
    // public MagazineEntity Magazine { get; set; }

    public IEnumerable<UserEntity>? User { get; set; }
    public IEnumerable<ProductEntity>? Product { get; set; }
    public IEnumerable<ProjectEntity>? Project { get; set; }
    public IEnumerable<TutorialEntity>? Tutorial { get; set; }
    public IEnumerable<EventEntity>? Event { get; set; }
    public IEnumerable<AdEntity>? Ad { get; set; }
    public IEnumerable<CompanyEntity>? Company { get; set; }
    public IEnumerable<TenderEntity>? Tender { get; set; }
    public IEnumerable<ServiceEntity>? Service { get; set; }
    public IEnumerable<MagazineEntity>? Magazine { get; set; }
}

[Table("Tags")]
public class TagEntity : BaseIdTitleEntity { }

[Table("Brands")]
public class BrandEntity : BaseIdTitleEntity { }

[Table("References")]
public class ReferenceEntity : BaseIdTitleEntity { }

[Table("Categories")]
public class CategoryEntity : BaseIdTitleEntity { }

[Table("Specialities")]
public class SpecialityEntity : BaseIdTitleEntity { }

[Table("Favorites")]
public class FavoriteEntity : BaseIdTitleEntity { }

[Table("Colors")]
public class ColorEntity : BaseIdTitleEntity { }

[Table("ContactInfoItems")]
public class ContactInfoItemEntity : BaseIdTitleEntity { }

public enum IdTitleUseCase {
    Null = 100,
    Ads = 101,
    Event = 102,
    Job = 103,
    Learn = 104,
    product = 105,
    Project = 107,
    Tender = 108,
    Brand = 109,
    Refrence = 110,
}

public class IdTitleReadDto: BaseReadDto {
    public BaseIdTitleEntity? Parent { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
}

public class IdTitleCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
}