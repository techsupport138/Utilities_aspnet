using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class BaseProductEntity : BaseEntity
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Subtitle { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = null!;
}