using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities;

public class ColorEntity : BaseEntity {
    [StringLength(100)]
    public string Title { get; set; } = null!;
    [StringLength(100)]
    public string ColorHex { get; set; } = null!;
}