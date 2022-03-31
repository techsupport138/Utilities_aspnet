using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.Product.Entities;

public class ProjectEntity : BaseProductEntity {
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Subtitle { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;
}