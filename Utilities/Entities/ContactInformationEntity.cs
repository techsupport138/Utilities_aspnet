using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.ServiceProvider.Entities;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Enums;

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
    public virtual ContactInfoItemEntity ContactInfoItem { get; set; } = null!;

    [Required]
    [ForeignKey("ContactInfoItem")]
    public Guid? ContactInfoItemId { get; set; } = null!;

    public virtual ServiceProviderEntity? ServiceProvider { get; set; }

    [ForeignKey("ServiceProvider")]
    public Guid? ServiceProviderId { get; set; }
}