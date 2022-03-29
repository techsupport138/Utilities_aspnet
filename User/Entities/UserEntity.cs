using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser
{
    [Required]
    public bool Suspend { get; set; } = false;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(100)]
    public string? Headline { get; set; }

    [StringLength(100)]
    public string? Education { get; set; }

    [StringLength(100)]
    public string? Degree { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreateAccount { get; set; } = DateTime.Now;
    public ICollection<MediaEntity>? Media { get; set; }
}