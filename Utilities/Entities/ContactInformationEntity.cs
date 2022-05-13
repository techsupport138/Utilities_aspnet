using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.Utilities.Entities;

public class ContactInformationEntity : BaseEntity {
    [Required]
    [StringLength(500)]
    public string Value { get; set; } = "";

    [EnumDataType(typeof(VisibilityType))]
    public VisibilityType Visibility { get; set; } = VisibilityType.Public;

    [StringLength(500)]
    public string? Link { get; set; }

    [StringLength(450)]
    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }

    [Required]
    public virtual ContactInfoItemEntity ContactInfoItem { get; set; }

    [Required]
    [ForeignKey("ContactInfoItem")]
    public Guid? ContactInfoItemId { get; set; }
}