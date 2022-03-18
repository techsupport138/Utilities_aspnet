using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.Utilities.Entities;

public class BaseEntity {
    [Key] public long Id { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }
}