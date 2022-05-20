namespace Utilities_aspnet.Content;

public class BaseContentEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public ContentUseCase? UseCase { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
}

public class ContentEntity : BaseContentEntity { }

public enum ContentUseCase {
    AboutUs = 100,
    Terms = 101,
    OnBoarding = 102
}

public class ContentReadDto : BaseReadDto {
    public Guid? Id { get; set; }
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