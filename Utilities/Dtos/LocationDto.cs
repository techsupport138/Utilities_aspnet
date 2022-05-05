namespace Utilities_aspnet.Utilities.Dtos;

public class LocationReadDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public LocationReadDto? Parent { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public LocationType Type { get; set; }

    public LocationReadDto MapLocationReadDto(LocationEntity e) {
        LocationReadDto dto = new LocationReadDto {
            Id = e.Id,
            Latitude = e.Latitude,
            Longitude = e.Longitude,
            ParentId = e.ParentId,
            Title = e.Title,
            Type = e.Type,
            Parent = e.Parent == null ? null : MapLocationReadDto(e),
            Media = MediaEntity.MapMediaEnumarableDto(e.Media),
        };

        return dto;
    }
}