namespace Utilities_aspnet.Entities;

[Table("Forms")]
public class FormEntity : BaseEntity {
	[StringLength(2000)]
	public string? Title { get; set; }

	[ForeignKey(nameof(FormFieldId))]
	[InverseProperty(nameof(FormFieldEntity.Forms))]
	public FormFieldEntity? FormField { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public Guid? FormFieldId { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public UserEntity? User { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public string? UserId { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public ProductEntity? Product { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public Guid? ProductId { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public OrderDetailEntity? OrderDetail { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public Guid? OrderDetailId { get; set; }
}

[Table("FormFields")]
public class FormFieldEntity : BaseEntity {
	[StringLength(500)]
	public string? Label { get; set; }

	public bool? IsRequired { get; set; } = false;

	[StringLength(2000)]
	public string? OptionList { get; set; }

	public FormFieldType? Type { get; set; }

	public Guid? CategoryId { get; set; }

	[System.Text.Json.Serialization.JsonIgnore]
	public CategoryEntity? Category { get; set; }

	[InverseProperty(nameof(FormEntity.FormField))]
	public IEnumerable<FormEntity>? Forms { get; set; }
}

public class FormCreateDto {
	public string? UserId { get; set; }
	public Guid? ProductId { get; set; }
	public Guid? OrderDetailId { get; set; }
	public IEnumerable<FormTitleDto>? Form { get; set; }
}

public class FormTitleDto {
	public Guid? Id { get; set; }
	public string? Title { get; set; }
}