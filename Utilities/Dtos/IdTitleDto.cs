namespace Utilities_aspnet.Utilities.Dtos;

public class IdTitleDto {
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }


    public static IdTitleDto MapIdTitle(string? id = null, string? title = null, string? subtitle = null,
        IEnumerable<MediaEntity>? media = null) {
        IdTitleDto dto = new() {
            Id = id,
            Title = title,
            SubTitle = subtitle,
            Media = MediaEntity.MapEnumarableDto(media)
        };

        return dto;
    }

    public static IEnumerable<IdTitleDto> MapEnumarableDto(IEnumerable<IdTitleDto>? e) {
        IEnumerable<IdTitleDto> dto =
            new List<IdTitleDto>(e?.Select(i => MapIdTitle(i.Id, i.Title, i.SubTitle)) ?? Array.Empty<IdTitleDto>());
        return dto;
    }
}