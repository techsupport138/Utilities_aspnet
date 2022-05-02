using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.BookMark.Entities;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser
{

    public UserEntity()
    {
        ContactInformation = new List<ContactInformationEntity>();
        Media = new List<MediaEntity>();
        BookMark = new List<BookMarkEntity>();
    }

    [Required]
    public bool Suspend { get; set; } = false;

    [StringLength(100)]
    public string? FirstName { get; set; }
    [StringLength(100)]
    public string? LastName { get; set; }

    //public string FullName => FirstName + " " + LastName;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(100)]
    public string? Headline { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }

    [StringLength(100)]
    public string? Education { get; set; }

    [StringLength(100)]
    public string? WebSite { get; set; }
    [StringLength(100)]
    public string? Instagram { get; set; }
    [StringLength(100)]
    public string? Telegram { get; set; }
    [StringLength(100)]
    public string? PhoneNumber { get; set; }
    [StringLength(100)]
    public string? Link { get; set; }

    public bool PublicBio { get; set; } = true;

    [StringLength(100)]
    public string? Degree { get; set; }

    public decimal Wallet { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? Birthday { get; set; }
    public int? Birth_Year { get; set; }
    public int? Birth_Month { get; set; }
    public int? Birth_Day { get; set; }
    public DateTime CreateAccount { get; set; } = DateTime.Now;

    public Guid? ColorId { get; set; }
    [ForeignKey(nameof(ColorId))]
    public ColorEntity? Color { get; set; }


    public Guid? LocationId { get; set; }

    [ForeignKey(nameof(LocationId))]
    public LocationEntity? Location { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }

    


    public ICollection<UserToColorEntity>? Colors { get; set; }
    public ICollection<UserToFavoriteEntity>? Favorites { get; set; }
    public ICollection<UserToSpecialtyEntity>? Specialties { get; set; }
    public ICollection<ShoppingListEntity>? ShoppingList { get; set; }


    public ICollection<BookMarkEntity>? BookMark { get; set; }
    //todo:از بوکمارک برای نشان کردن استفاده کن
    //public ICollection<SpecialtyEntity>? Specialties { get; set; }
    //public ICollection<PostCategoryEntity>? Favorites { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }
}