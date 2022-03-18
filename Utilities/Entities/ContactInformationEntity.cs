using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.ServiceProvider.Entities;
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

    [Required]
    public virtual ContactInfoItemEntity ContactInfoItem { get; set; } = null!;

    [Required]
    [ForeignKey("ContactInfoItem")]
    public long ContactInfoItemId { get; set; }

    public virtual ServiceProviderEntity? ServiceProvider { get; set; }

    [ForeignKey("ServiceProvider")]
    public long? ServiceProviderId { get; set; }
}