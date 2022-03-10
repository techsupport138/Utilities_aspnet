using System.ComponentModel.DataAnnotations;
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
    public ContactInfoItemEntity ContactInfoItem { get; set; } = null!;

    [Required]
    public long ContactInfoItemId { get; set; }

    public ServiceProviderEntity? ServiceProvider { get; set; }
    public int? ServiceProviderId { get; set; }
}