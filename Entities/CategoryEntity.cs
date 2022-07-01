namespace Utilities_aspnet.Entities;

[Table("Categories")]
public class CategoryEntity : BaseEntity {
	public string? Title { get; set; }
	public string? TitleTr1 { get; set; }
	public string? TitleTr2 { get; set; }
	public string? Subtitle { get; set; }
	public string? Color { get; set; }
	public string? Link { get; set; }
	public string? UseCase { get; set; }
	public string? Type { get; set; }

	public IEnumerable<MediaEntity> Media { get; set; }
	public IEnumerable<UserEntity>? Users { get; set; }
	public IEnumerable<ProductEntity>? Products { get; set; }
	public IEnumerable<FormEntity>? FormFields { get; set; }

	public Guid? ParentId { get; set; }
	public CategoryEntity? Parent { get; set; }

	[InverseProperty("Parent")]
	public IEnumerable<CategoryEntity>? Children { get; set; }
}

public class CategoryReadDto {
	public Guid? Id { get; set; }
	public int? SecondaryId { get; set; }
	public string? Title { get; set; }
	public string? TitleTr1 { get; set; }
	public string? TitleTr2 { get; set; }
	public string? Subtitle { get; set; }
	public string? Color { get; set; }
	public string? Link { get; set; }
	public string? UseCase { get; set; }
	public string? Type { get; set; }
	public CategoryReadDto? Parent { get; set; }
	public IEnumerable<CategoryReadDto>? Children { get; set; }
	public Guid? ParentId { get; set; }
	public IEnumerable<MediaDto>? Media { get; set; }
}

public class CategoryCreateUpdateDto {
	public Guid? Id { get; set; }
	public Guid? ParentId { get; set; }
	public string? Title { get; set; }
	public string? TitleTr1 { get; set; }
	public string? TitleTr2 { get; set; }
	public string? Subtitle { get; set; }
	public string? Link { get; set; }
	public string? Color { get; set; }
	public string? UseCase { get; set; }
	public string? Type { get; set; }
}

public class SeederCategoryDto {
	public List<CategoryCreateUpdateDto> Categories { get; set; }
}