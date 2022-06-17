using Utilities_aspnet.Category;
using Utilities_aspnet.Comment;
using Utilities_aspnet.User;
using Utilities_aspnet.Vote;

namespace Utilities_aspnet.Product;

[Table("Products")]
public abstract class ProductEntity : BaseEntity {
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? Address { get; set; }
    public string? Author { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Link { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? Type { get; set; }
    public string? UseCase { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public decimal? Price { get; set; }
    public int? VisitCount { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? MinOrder { get; set; }
    public double? MaxOrder { get; set; }
    public string? Unit { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProductStatus? Status { get; set; }

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

    public IEnumerable<LocationEntity>? Locations { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<FormEntity>? Forms { get; set; }
    public IEnumerable<CategoryEntity>? Categories { get; set; }
    public IEnumerable<VoteFieldEntity>? VoteFields { get; set; }
    public IEnumerable<ReportEntity>? Reports { get; set; }
    public IEnumerable<BookmarkEntity>? Bookmarks { get; set; }
    public IEnumerable<CommentEntity>? Comments { get; set; }
}

public class ProductReadDto {
    public Guid? Id { get; set; }
    public string? UserId { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? Address { get; set; }
    public string? Author { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Link { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public string? UseCase { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public bool IsBookmarked { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? VisitsCount { get; set; }
    public double? MinOrder { get; set; }
    public double? MaxOrder { get; set; }
    public decimal? Price { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public UserReadDto? User { get; set; }
    public ProductStatus? Status { get; set; }
    public IEnumerable<LocationReadDto>? Locations { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<CategoryReadDto>? Categories { get; set; }
    public IEnumerable<VoteReadDto>? VoteFields { get; set; }
    public IEnumerable<FormDto>? Forms { get; set; }
    public IEnumerable<CommentReadDto>? Comments { get; set; }
}

public class ProductCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? Address { get; set; }
    public string? Author { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Link { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public string? UseCase { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? VisitsCount { get; set; }
    public decimal? Price { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? MinOrder { get; set; }
    public double? MaxOrder { get; set; }
    public ProductStatus? Status { get; set; }
    public IEnumerable<int>? Locations { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Forms { get; set; }
    public IEnumerable<Guid>? VoteFields { get; set; }
    public IEnumerable<Guid>? Reports { get; set; }
}

public class FilterProductDto {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? Link { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? Author { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public string? UseCase { get; set; }
    public decimal? StartPriceRange { get; set; }
    public decimal? EndPriceRange { get; set; }
    public bool? Enabled { get; set; }
    public bool? IsForSale { get; set; }
    public bool? IsBookmarked { get; set; }
    public int? VisitsCount { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? MinOrder { get; set; }
    public double? MaxOrder { get; set; }
    public ProductStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IEnumerable<int>? Locations { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Brands { get; set; }
    public IEnumerable<Guid>? References { get; set; }
    public IEnumerable<Guid>? Tags { get; set; }
    public IEnumerable<Guid>? Specialities { get; set; }

    public ProductFilterOrder? FilterOrder { get; set; } = ProductFilterOrder.AToZ;

    public int PageSize { get; set; } = 1000;
    public int PageNumber { get; set; } = 1;
}

public enum ProductFilterOrder {
    LowPrice,
    HighPrice,
    AToZ,
    ZToA
}

public enum ProductStatus {
    Released,
    Expired,
    InQueue,
    Deleted
}