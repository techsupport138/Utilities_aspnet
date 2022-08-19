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
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public DateTime? Date1 { get; set; }
	public DateTime? Date2 { get; set; }

	public Guid? ParentId { get; set; }
	public CategoryEntity? Parent { get; set; }

	[InverseProperty("Parent")]
	public IEnumerable<CategoryEntity>? Children { get; set; }

	public IEnumerable<MediaEntity> Media { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public IEnumerable<UserEntity>? Users { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public IEnumerable<ProductEntity>? Products { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public IEnumerable<FormEntity>? FormFields { get; set; }

	[NotMapped]
	public int? SecondaryId { get; set; }
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
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public DateTime? Date1 { get; set; }
	public DateTime? Date2 { get; set; }
	public CategoryReadDto? Parent { get; set; }
	public IEnumerable<CategoryReadDto>? Children { get; set; }
	public Guid? ParentId { get; set; }
	public IEnumerable<MediaDto>? Media { get; set; }
}

public class SeederCategoryDto {
	public List<CategoryEntity> Categories { get; set; }
}