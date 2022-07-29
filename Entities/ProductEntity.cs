namespace Utilities_aspnet.Entities;

[Table("Products")]
public class ProductEntity : BaseEntity {
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
	public string? Unit { get; set; }
	public string? Packaging { get; set; }
	public string? Shipping { get; set; }
	public string? Port { get; set; }
	public string? KeyValues1 { get; set; }
	public string? KeyValues2 { get; set; }
	public string? Value { get; set; }
	public string? Value1 { get; set; }
	public string? Value2 { get; set; }
	public string? Value3 { get; set; }
	public string? Value4 { get; set; }
	public string? Value5 { get; set; }
	public string? Value6 { get; set; }
	public string? Value7 { get; set; }
	public string? Value8 { get; set; }
	public string? Value9 { get; set; }
	public string? Value10 { get; set; }
	public string? Value11 { get; set; }
	public string? Value12 { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public double? Length { get; set; }
	public double? Width { get; set; }
	public double? Height { get; set; }
	public double? Weight { get; set; }
	public double? MinOrder { get; set; }
	public double? MaxOrder { get; set; }
	public double? Price { get; set; }
	public bool? IsForSale { get; set; }
	public bool? Enabled { get; set; }
	public int? VisitsCount { get; set; }
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
	public IEnumerable<VoteEntity>? Votes { get; set; }
	public IEnumerable<ReportEntity>? Reports { get; set; }
	public IEnumerable<BookmarkEntity>? Bookmarks { get; set; }
	public IEnumerable<CommentEntity>? Comments { get; set; }
	public IEnumerable<TeamEntity>? Teams { get; set; }
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
	public string? Packaging { get; set; }
	public string? Port { get; set; }
	public string? Shipping { get; set; }
	public string? KeyValues1 { get; set; }
	public string? KeyValues2 { get; set; }
	public string? Value { get; set; }
	public string? Value1 { get; set; }
	public string? Value2 { get; set; }
	public string? Value3 { get; set; }
	public string? Value4 { get; set; }
	public string? Value5 { get; set; }
	public string? Value6 { get; set; }
	public string? Value7 { get; set; }
	public string? Value8 { get; set; }
	public string? Value9 { get; set; }
	public string? Value10 { get; set; }
	public string? Value11 { get; set; }
	public string? Value12 { get; set; }
	public bool? IsForSale { get; set; }
	public bool? Enabled { get; set; }
	public bool IsBookmarked { get; set; }
	public int? VisitsCount { get; set; }
	public int? CommentsCount { get; set; }
	public int? DownloadCount { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public double? MinOrder { get; set; }
	public double? MaxOrder { get; set; }
	public double? Score { get; set; }
	public double? Price { get; set; }
	public double? Length { get; set; }
	public double? Width { get; set; }
	public double? Height { get; set; }
	public double? Weight { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
	public ProductStatus? Status { get; set; }
	public UserReadDto? User { get; set; }
	public IEnumerable<LocationReadDto>? Locations { get; set; }
	public IEnumerable<MediaDto>? Media { get; set; }
	public IEnumerable<CategoryReadDto>? Categories { get; set; }
	public IEnumerable<VoteReadDto>? VoteFields { get; set; }
	public IEnumerable<MyVoteReadDto>? MyVotes { get; set; }
	public IEnumerable<FormDto>? Forms { get; set; }
	public IEnumerable<CommentReadDto>? Comments { get; set; }
	public IEnumerable<TeamReadDto>? Teams { get; set; }
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
	public string? Packaging { get; set; }
	public string? Port { get; set; }
	public string? Shipping { get; set; }
	public string? KeyValues1 { get; set; }
	public string? KeyValues2 { get; set; }
	public string? Value { get; set; }
	public string? Value1 { get; set; }
	public string? Value2 { get; set; }
	public string? Value3 { get; set; }
	public string? Value4 { get; set; }
	public string? Value5 { get; set; }
	public string? Value6 { get; set; }
	public string? Value7 { get; set; }
	public string? Value8 { get; set; }
	public string? Value9 { get; set; }
	public string? Value10 { get; set; }
	public string? Value11 { get; set; }
	public string? Value12 { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public double? Price { get; set; }
	public double? Length { get; set; }
	public double? Width { get; set; }
	public double? Height { get; set; }
	public double? Weight { get; set; }
	public double? MinOrder { get; set; }
	public double? MaxOrder { get; set; }
	public bool? IsForSale { get; set; }
	public bool? Enabled { get; set; }
	public int? VisitsCount { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public ProductStatus? Status { get; set; }
	public IEnumerable<int>? Locations { get; set; }
	public IEnumerable<Guid>? Categories { get; set; }
	public IEnumerable<string>? Teams { get; set; }
}

public class ProductFilterDto {
	public string? Title { get; set; }
	public string? Subtitle { get; set; }
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
	public string? Packaging { get; set; }
	public string? Port { get; set; }
	public string? Shipping { get; set; }
	public string? KeyValues1 { get; set; }
	public string? KeyValues2 { get; set; }
	public string? Value { get; set; }
	public string? Value1 { get; set; }
	public string? Value2 { get; set; }
	public string? Value3 { get; set; }
	public string? Value4 { get; set; }
	public string? Value5 { get; set; }
	public string? Value6 { get; set; }
	public string? Value7 { get; set; }
	public string? Value8 { get; set; }
	public string? Value9 { get; set; }
	public string? Value10 { get; set; }
	public string? Value11 { get; set; }
	public string? Value12 { get; set; }
	public double? Length { get; set; }
	public double? Width { get; set; }
	public double? Height { get; set; }
	public double? Weight { get; set; }
	public double? MinOrder { get; set; }
	public double? MaxOrder { get; set; }
	public double? StartPriceRange { get; set; }
	public double? EndPriceRange { get; set; }
	public bool? Enabled { get; set; }
	public bool? IsForSale { get; set; }
	public bool? IsBookmarked { get; set; }
	public bool? IsFollowing { get; set; }
	public bool? ShowLocation { get; set; } = false;
	public bool? ShowMedia { get; set; } = false;
	public bool? ShowForms { get; set; } = false;
	public bool? ShowCategories { get; set; } = false;
	public bool? ShowVoteFields { get; set; } = false;
	public bool? ShowVotes { get; set; } = false;
	public bool? ShowReports { get; set; } = false;
	public bool? ShowComments { get; set; } = false;
	public bool? ShowTeams { get; set; } = false;
	public bool? ShowCreator { get; set; } = false;
	public int? VisitsCount { get; set; }
	public int PageSize { get; set; } = 1000;
	public int PageNumber { get; set; } = 1;
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public ProductStatus? Status { get; set; }
	public IEnumerable<int>? Locations { get; set; }
	public IEnumerable<Guid>? Categories { get; set; }

	public ProductFilterOrder? FilterOrder { get; set; } = ProductFilterOrder.AToZ;
}

public class FilterProductDto {
	public string? Title { get; set; }
	public string? Subtitle { get; set; }
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
	public string? Shipping { get; set; }
	public string? KeyValues1 { get; set; }
	public string? KeyValues2 { get; set; }
	public double? StartPriceRange { get; set; }
	public double? EndPriceRange { get; set; }
	public double? Length { get; set; }
	public double? Width { get; set; }
	public double? Height { get; set; }
	public double? Weight { get; set; }
	public double? MinOrder { get; set; }
	public double? MaxOrder { get; set; }
	public bool? Enabled { get; set; }
	public bool? IsForSale { get; set; }
	public bool? IsBookmarked { get; set; }
	public bool? Minimal { get; set; }
	public bool? IsFollowing { get; set; }
	public int? VisitsCount { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public ProductStatus? Status { get; set; }
	public IEnumerable<int>? Locations { get; set; }
	public IEnumerable<Guid>? Categories { get; set; }

	public ProductFilterOrder? FilterOrder { get; set; } = ProductFilterOrder.AToZ;

	public int PageSize { get; set; } = 100;
	public int PageNumber { get; set; } = 1;
}

public class SeederProductDto {
	public List<ProductCreateUpdateDto> Products { get; set; }
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