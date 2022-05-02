using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.Utilities.Entities;

public class ColorEntity : BaseEntity {
    [StringLength(100)]
    public string Title { get; set; } 
    [StringLength(100)]
    public string ColorHex { get; set; } 
}