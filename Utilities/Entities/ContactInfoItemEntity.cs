using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.Utilities.Entities;

public class ContactInfoItemEntity : BaseEntity {
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    public ICollection<MediaEntity> Media { get; set; } = null!;
}