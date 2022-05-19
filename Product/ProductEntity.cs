namespace Utilities_aspnet.Product;

public abstract class BaseProductEntity : BaseEntity {
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public int? VisitCount { get; set; }
    public string? Address { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

    public IEnumerable<LocationEntity>? Locations { get; set; }
    public IEnumerable<FavoriteEntity>? Favorites { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryEntity>? Categories { get; set; }
    public IEnumerable<TagEntity>? Tags { get; set; }
    public IEnumerable<VoteFieldEntity>? VoteFields { get; set; }
    public IEnumerable<ReportEntity>? Reports { get; set; }
    public IEnumerable<SpecialityEntity>? Specialities { get; set; }
    public IEnumerable<BrandEntity>? Brands { get; set; }
    public IEnumerable<ReferenceEntity>? Reference { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformations { get; set; }
}

[Table("Products")]
public class ProductEntity : BaseProductEntity { }

[Table("Projects")]
public class ProjectEntity : BaseProductEntity { }

[Table("Tutorials")]
public class TutorialEntity : BaseProductEntity { }

[Table("Events")]
public class EventEntity : BaseProductEntity { }

[Table("Ads")]
public class AdEntity : BaseProductEntity { }

[Table("Company")]
public class CompanyEntity : BaseProductEntity { }

[Table("Tenders")]
public class TenderEntity : BaseProductEntity { }

[Table("Services")]
public class ServiceEntity : BaseProductEntity { }

[Table("Magazine")]
public class MagazineEntity : BaseProductEntity { }

public class ProductReadDto : BaseReadDto {
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public bool? IsForSale { get; set; }
    public bool? IsBookmarked { get; set; }
    public decimal? Price { get; set; }
    public int? VisitsCount { get; set; }
    public string? Address { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public UserReadDto? User { get; set; }
    public IEnumerable<LocationReadDto>? Locations { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<IdTitleReadDto>? Categories { get; set; }
    public IEnumerable<IdTitleReadDto>? Brands { get; set; }
    public IEnumerable<IdTitleReadDto>? Reference { get; set; }
    public IEnumerable<IdTitleReadDto>? Tags { get; set; }
    public IEnumerable<VoteReadDto>? VoteFields { get; set; }
    public IEnumerable<IdTitleReadDto>? Specialities { get; set; }
}

public class ProductCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public string? Address { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IEnumerable<int>? Locations { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Reference { get; set; }
    public IEnumerable<Guid>? Brands { get; set; }
    public IEnumerable<Guid>? Specialties { get; set; }
    public IEnumerable<Guid>? Tags { get; set; }
}