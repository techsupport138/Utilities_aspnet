using Utilities_aspnet.Product;

namespace Utilities_aspnet.IdTitle;

public abstract class BaseIdTitleEntity : BaseEntity {
    public Guid? ParentId { get; set; }
    public BaseIdTitleEntity? Parent { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Color { get; set; }

    public string? Link { get; set; }

    public IdTitleUseCase UseCase { get; set; } = IdTitleUseCase.Null;

    public ICollection<MediaEntity> Media { get; set; }
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
    Tender = 108
}

public class IdTitleReadDto {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
}

public class IdTitleCreateUpdateDto {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public IdTitleUseCase? UseCase { get; set; }
}

public class IdTitleProfile : Profile {
    public IdTitleProfile() {
        CreateMap<TagEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<TagEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<CategoryEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<CategoryEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<SpecialityEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<SpecialityEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<FavoriteEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<FavoriteEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ColorEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ColorEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleCreateUpdateDto>().ReverseMap();
    }
}