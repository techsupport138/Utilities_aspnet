using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities;

public class ContentEntity : BaseEntity {
    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? SubTitle { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? Link { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }

    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }

    [Required]
    [EnumDataType(typeof(ContentUseCase))]
    public ContentUseCase UseCase { get; set; }

    [Required]
    [EnumDataType(typeof(ApprovalStatus))]
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
}