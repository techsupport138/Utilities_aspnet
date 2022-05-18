namespace Utilities_aspnet.Content;

public class BaseContentEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase? UseCase { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }

    public static BaseContentEntity MapEntity(ContentCreateUpdateDto dto) {
        BaseContentEntity e = new() {
            Title = dto.Title,
            SubTitle = dto.SubTitle,
            UseCase = dto.UseCase,
            Description = dto.Description,
        };
        return e;
    }

    public static BaseContentEntity MapEntity(ContentReadDto dto) {
        BaseContentEntity e = new() {
            Id = dto.Id,
            Title = dto.Title,
            SubTitle = dto.SubTitle,
            UseCase = dto.UseCase,
            Description = dto.Description,
        };
        return e;
    }

    public static ContentReadDto MapReadDto(BaseContentEntity e) {
        ContentReadDto dto = new ContentReadDto() {
            Id = e.Id,
            Title = e.Title,
            SubTitle = e.SubTitle,
            UseCase = e.UseCase,
            Description = e.Description,
            CreatedAt = e.CreatedAt,
            DeletedAt = e.DeletedAt,
            UpdatedAt = e.UpdatedAt,
            Media = MediaEntity.MapEnumarableDto(e.Media),
        };
        return dto;
    }

    public static IEnumerable<ContentReadDto> MapMediaEnumarableDto(IEnumerable<BaseContentEntity>? e) {
        IEnumerable<ContentReadDto> dto =
            new List<ContentReadDto>(e?.Select(MapReadDto) ?? Array.Empty<ContentReadDto>());
        return dto;
    }
}

public class ContentEntity : BaseContentEntity { }

public enum ContentUseCase {
    AboutUs = 100,
    Terms = 101,
    OnBoarding = 102,
}

public class ContentReadDto : BaseReadDto {
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase? UseCase { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class ContentCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase? UseCase { get; set; }
}