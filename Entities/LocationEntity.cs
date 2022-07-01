namespace Utilities_aspnet.Entities;

[Table("Location")]
public class LocationEntity {
	[Key]
	public int Id { get; set; }

	public int? ParentId { get; set; }

	[ForeignKey(nameof(ParentId))]
	public LocationEntity? Parent { get; set; }

	[Required]
	[StringLength(100)]
	public string Title { get; set; }

	public IEnumerable<MediaEntity>? Media { get; set; }

	[Required]
	[EnumDataType(typeof(LocationType))]
	public LocationType Type { get; set; }

	public double? Latitude { get; set; }

	public double? Longitude { get; set; }

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
	[InverseProperty("Parent")]
	public IEnumerable<LocationEntity>? Children { get; set; }
}

public class LocationReadDto {
	public int? I { get; set; }
	public string? N { get; set; }
	public double? Lat { get; set; }
	public double? Lon { get; set; }
	public LocationReadDto? P { get; set; }
	public int? Pi { get; set; }
	public IEnumerable<LocationReadDto>? Ch { get; set; }
}

public class LocationCreateDto {
	public string city { get; set; }
	public string lat { get; set; }
	public string lng { get; set; }
	public string country { get; set; }
}