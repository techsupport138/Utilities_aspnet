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
public class TagEntity : BaseIdTitleEntity { }

[Table("Brands")]
public class BrandEntity : BaseIdTitleEntity { }

[Table("References")]
public class ReferenceEntity : BaseIdTitleEntity { }

[Table("Categories")]
public class CategoryEntity : BaseIdTitleEntity {
    public Guid? ParentId { get; set; }

    [InverseProperty("Children")]
    public CategoryEntity? Parent { get; set; }

    public IEnumerable<FormEntity>? FormBuilderFieldLists { get; set; }

    [InverseProperty("Parent")]
    public IEnumerable<CategoryEntity>? Children { get; set; }
}

[Table("Specialities")]
public class SpecialityEntity : BaseIdTitleEntity { }

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
    public IEnumerable<IdTitleReadDto>? Children { get; set; }
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