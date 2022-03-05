using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
}