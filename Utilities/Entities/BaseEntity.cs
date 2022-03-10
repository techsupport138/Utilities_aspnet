using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.Utilities.Entities;

public class BaseEntity {
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Subtitle { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }
}