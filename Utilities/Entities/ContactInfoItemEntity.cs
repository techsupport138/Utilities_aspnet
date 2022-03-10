using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.Utilities.Entities;

public class ContactInfoItemEntity {
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    public ICollection<MediaEntity> Media { get; set; } = null!;
}