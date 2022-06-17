namespace Utilities_aspnet.Content;

[Table("Contents")]
public class ContentEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public UserEntity? User { get; set; }
    public string? UseCase { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
}

public class ContentReadDto : BaseReadDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? UseCase { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class ContentCreateUpdateDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? UseCase { get; set; }
}