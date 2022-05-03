using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.BookMark.Entities;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser {
    public UserEntity() {
        ContactInformation = new List<ContactInformationEntity>();
        Media = new List<MediaEntity>();
        BookMark = new List<BookMarkEntity>();
        BookMark = new List<BookMarkEntity>();
        BookMark = new List<BookMarkEntity>();
        BookMark = new List<BookMarkEntity>();
        BookMark = new List<BookMarkEntity>();
    }

    public bool? Suspend { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(100)]
    public string? Headline { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }

    public decimal Wallet { get; set; }
    public DateTime? Birthdate { get; set; }

    public Guid? LocationId { get; set; }

    [ForeignKey(nameof(LocationId))]
    public LocationEntity? Location { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }
    public ICollection<UserToColorEntity>? Colors { get; set; }
    public ICollection<UserToFavoriteEntity>? Favorites { get; set; }
    public ICollection<UserToSpecialtyEntity>? Specialties { get; set; }
    public ICollection<ShoppingListEntity>? ShoppingList { get; set; }
    public ICollection<BookMarkEntity>? BookMark { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }
}