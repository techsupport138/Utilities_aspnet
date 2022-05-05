using Utilities_aspnet.Product;

namespace Utilities_aspnet.Utilities.Entities;

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

    public Guid? ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    public Guid? AdsId { get; set; }

    [ForeignKey(nameof(AdsId))]
    public AdsEntity? Ads { get; set; }
    
    public static LocationReadDto MapReadDto(LocationEntity? e) {
        LocationReadDto dto = new LocationReadDto {
            Id = e.Id,
            Latitude = e.Latitude,
            Longitude = e.Longitude,
            ParentId = e.ParentId,
            Title = e.Title,
            Type = e.Type,
            Parent = e.Parent == null ? null : MapReadDto(e),
            Media = MediaEntity.MapEnumarableDto(e.Media),
        };

        return dto;
    }
}