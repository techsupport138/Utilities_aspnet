using Utilities_aspnet.User;

namespace Utilities_aspnet.Utilities.Entities;

[Table("ContactInformations")]
public class ContactInformationEntity : BaseEntity {
    [Required]
    public string Value { get; set; }

    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.Public;

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

    public Guid? ContactInfoItemId { get; set; }
    public virtual ContactInfoItemEntity ContactInfoItem { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }
    
    public ContentEntity? Content { get; set; }
    public Guid? ContentId { get; set; }
}

public class ContactInformationReadDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public IdTitleReadDto? ContactInfoItem { get; set; }
    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
}

public class ContactInformationCreateUpdateDto
{
    public Guid? Id { get; set; }
    public string Value { get; set; }
    public Guid ContactInfoItemId { get; set; }
    public string? Link { get; set; }
    public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
}