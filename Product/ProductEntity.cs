using Utilities_aspnet.Tag.Entities;
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
    public UserEntity? User { get; set; }

    public int? LocationId { get; set; }
    public LocationEntity? Location { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryEntity>? Category { get; set; }
    public IEnumerable<SpecialtyEntity>? Specialty { get; set; }
    public IEnumerable<TagEntity>? Tag { get; set; }
    public IEnumerable<VoteFieldEntity> VoteFields { get; set; }

    public static ProductReadDto MapReadDto(ProductEntity e) {
        ProductReadDto dto = new ProductReadDto {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Enabled = e.Enabled,
            Price = e.Price,
            VisitsCount = e.VisitCount,
            CreatedAt = e.CreatedAt,
            IsBookmarked = false,
            Subtitle = e.SubTitle,
            IsForSale = e.IsForSale,
            User = UserEntity.MapReadDto(e.User),
            Location = LocationEntity.MapReadDto(e.Location),
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
    public LocationReadDto? Location { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<IdTitleDto>? Categories { get; set; }
    public IEnumerable<IdTitleDto>? Team { get; set; }
    public IEnumerable<IdTitleDto>? Tags { get; set; }
    public IEnumerable<VoteReadDto>? Votes { get; set; }
}

public class AddUpdateProductDto {
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public Guid? Location { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Specialties { get; set; }
    public IEnumerable<Guid>? Tags { get; set; }
    public IEnumerable<Guid>? Teams { get; set; }
    public IEnumerable<VoteFieldCreateDto>? Votes { get; set; }
}