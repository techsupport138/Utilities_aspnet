using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.BookMark.Entities;
using Utilities_aspnet.Product.Entities;
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
    public Guid? LocationId { get; set; }

    [ForeignKey(nameof(LocationId))]
    public LocationEntity? Location { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }

    public ICollection<BookMarkEntity>? BookMark { get; set; }

    ///todo:از بوکمارک برای نشان کردن استفاده کن
    //public ICollection<SpecialtyEntity>? Specialties { get; set; }
    //public ICollection<PostCategoryEntity>? Favorites { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }
}