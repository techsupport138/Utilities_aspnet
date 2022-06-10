using Utilities_aspnet.User;

namespace Utilities_aspnet.IdTitle;

public abstract class BaseIdTitleEntity : BaseEntity {
    [Required]
    public string Title { get; set; }

    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }

    public IdTitleUseCase UseCase { get; set; } = IdTitleUseCase.Null;

    public ICollection<MediaEntity> Media { get; set; }
    public IEnumerable<UserEntity>? User { get; set; }
    public IEnumerable<ProductEntity>? Product { get; set; }
    public IEnumerable<DailyPriceEntity>? DailyPrice { get; set; }
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
public class TagEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public TagEntity? Parent { get; set; }
}

[Table("Brands")]
public class BrandEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public BrandEntity? Parent { get; set; }
}

[Table("References")]
public class ReferenceEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public ReferenceEntity? Parent { get; set; }
}

[Table("Categories")]
public class CategoryEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public CategoryEntity? Parent { get; set; }
    public ICollection<FormEntity>? FormBuilderFieldLists { get; set; }
}

[Table("Specialities")]
public class SpecialityEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public SpecialityEntity? Parent { get; set; }
}

[Table("Favorites")]
public class FavoriteEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public FavoriteEntity? Parent { get; set; }
}

[Table("Colors")]
public class ColorEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public ColorEntity? Parent { get; set; }
}

[Table("ContactInfoItems")]
public class ContactInfoItemEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }
    public ContactInfoItemEntity? Parent { get; set; }
}

public enum IdTitleUseCase {
    Null = 100,
    Ads = 101,
    Event = 102,
    Job = 103,
    Learn = 104,
    Product = 105,
    Project = 107,
    Tender = 108,
    Brand = 109,
    Refrence = 110
}

public class IdTitleReadDto {
    public Guid? Id { get; set; }
    public int? SecondaryId { get; set; }
    public string? Title { get; set; }
    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
    public IdTitleReadDto? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class IdTitleCreateUpdateDto {
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
}