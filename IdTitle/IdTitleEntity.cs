using Utilities_aspnet.User;

namespace Utilities_aspnet.IdTitle;

[Table("Categories")]
public abstract class CategoryEntity : BaseEntity {
    [Required]
    public string Title { get; set; }

    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }
    public string? UseCase { get; set; }
    public string? Type { get; set; }

    public ICollection<MediaEntity> Media { get; set; }
    public IEnumerable<UserEntity>? User { get; set; }
    public IEnumerable<ProductEntity>? Product { get; set; }
    public IEnumerable<FormEntity>? FormBuilderFieldLists { get; set; }

    public Guid? ParentId { get; set; }

    [InverseProperty("Children")]
    public CategoryEntity? Parent { get; set; }
    
    [InverseProperty("Parent")]
    public IEnumerable<CategoryEntity>? Children { get; set; }
}

public class IdTitleReadDto {
    public Guid? Id { get; set; }
    public int? SecondaryId { get; set; }
    public string? Title { get; set; }
    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Color { get; set; }
    public string? Link { get; set; }
    public string? UseCase { get; set; }
    public string? Type { get; set; }
    public IdTitleReadDto? Parent { get; set; }
    public IEnumerable<IdTitleReadDto>? Children { get; set; }
    public Guid? ParentId { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
}

public class IdTitleCreateUpdateDto {
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? TitleTr1 { get; set; }
    public string? Subtitle { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public string? UseCase { get; set; }
    public string? Type { get; set; }
}