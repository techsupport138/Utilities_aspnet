using Utilities_aspnet.User;

namespace Utilities_aspnet.Form;

[Table("Forms")]
public class FormEntity : BaseEntity {
    public string Title { get; set; }

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    [ForeignKey(nameof(FormFieldId))]
    [InverseProperty(nameof(FormFieldEntity.Forms))]
    public FormFieldEntity? FormField { get; set; }

    public Guid? FormFieldId { get; set; }
}

[Table("FormFields")]
public class FormFieldEntity : BaseEntity {
    public string? Label { get; set; }
    public bool? IsRequired { get; set; } = false;
    public string? OptionList { get; set; }
    public FormFieldType? Type { get; set; }

    public Guid? CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }

    [InverseProperty(nameof(FormEntity.FormField))]
    public IEnumerable<FormEntity>? Forms { get; set; }
}

public class FormDto {
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public FormFieldDto? FormField { get; set; }
}

public class FormFieldDto {
    public Guid? Id { get; set; }
    public string? Label { get; set; }
    public string? Title { get; set; }
    public bool? IsRequired { get; set; }
    public string? OptionList { get; set; }
    public FormFieldType? Type { get; set; }
    public Guid? CategoryId { get; set; }
}

public class FormCreateDto {
    public string? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }

    public List<IdTitleCreateUpdateDto> Form { get; set; }
}

public enum FormFieldType {
    SingleLineText,
    MultiLineText,
    MultiSelect,
    SingleSelect,
    Bool,
    Number,
    File,
    Image
}