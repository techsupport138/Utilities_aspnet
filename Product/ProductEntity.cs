using Utilities_aspnet.IdTitle;
using Utilities_aspnet.Report;
using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.Product;

public abstract class BaseProductEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public int? VisitCount { get; set; }

    public string? UserId { get; set; }

    [ForeignKey("UserEntity")]
    public UserEntity? User { get; set; }

    public IEnumerable<LocationEntity>? Location { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryEntity>? Category { get; set; }
    public IEnumerable<TagEntity>? Tag { get; set; }
    public IEnumerable<VoteFieldEntity> VoteField { get; set; }
    public IEnumerable<ReportEntity> Report { get; set; }

    public static ProductReadDto MapReadDto(ProductEntity e) {
        ProductReadDto dto = new ProductReadDto {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Enabled = e.Enabled,
            Price = e.Price,
            VisitsCount = e.VisitCount,
            CreatedAt = e.CreatedAt,
            Subtitle = e.SubTitle,
            IsForSale = e.IsForSale,
            User = UserEntity.MapReadDto(e.User),
            Media = MediaEntity.MapEnumarableDto(e.Media),
        };

        return dto;
    }

    public static IEnumerable<ProductReadDto> MapMediaEnumarableDto(IEnumerable<ProductEntity>? e) {
        IEnumerable<ProductReadDto> dto =
            new List<ProductReadDto>(e?.Select(MapReadDto) ?? Array.Empty<ProductReadDto>());
        return dto;
    }
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

public class ProductReadDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public bool? Enabled { get; set; }
    public bool? IsForSale { get; set; }
    public bool? IsBookmarked { get; set; }
    public decimal? Price { get; set; }
    public int? VisitsCount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public UserReadDto? User { get; set; }
    public IEnumerable<LocationReadDto>? Location { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<IdTitleDto>? Categories { get; set; }
    public IEnumerable<IdTitleDto>? Team { get; set; }
    public IEnumerable<IdTitleDto>? Tags { get; set; }
    public IEnumerable<VoteReadDto>? Votes { get; set; }
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
    public IEnumerable<LocationReadDto>? Location { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Specialties { get; set; }
    public IEnumerable<Guid>? Tags { get; set; }
    public IEnumerable<Guid>? Teams { get; set; }
    public IEnumerable<VoteFieldCreateDto>? Votes { get; set; }
}

public class ProductProfile : Profile {
    public ProductProfile() {
        CreateMap<BaseProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<BaseProductEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ProjectEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProjectEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<TutorialEntity, ProductReadDto>().ReverseMap();
        CreateMap<TutorialEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<EventEntity, ProductReadDto>().ReverseMap();
        CreateMap<EventEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<AdEntity, ProductReadDto>().ReverseMap();
        CreateMap<AdEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<CompanyEntity, ProductReadDto>().ReverseMap();
        CreateMap<CompanyEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<TenderEntity, ProductReadDto>().ReverseMap();
        CreateMap<TenderEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ServiceEntity, ProductReadDto>().ReverseMap();
        CreateMap<ServiceEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<MagazineEntity, ProductReadDto>().ReverseMap();
        CreateMap<MagazineEntity, ProductCreateUpdateDto>().ReverseMap();
    }
}