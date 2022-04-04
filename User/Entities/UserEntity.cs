using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser {


    [Required]
    public bool Suspend { get; set; } = false;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(100)]
    public string? Headline { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }

    [StringLength(100)]
    public string? Education { get; set; }


    [StringLength(100)]
    public string? Degree { get; set; }

    public decimal Wallet { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? Birthday { get; set; }
    public DateTime CreateAccount { get; set; } = DateTime.Now;

    public Guid? ColorId { get; set; }

    [ForeignKey(nameof(ColorId))]
    public ColorEntity? Color { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }
}