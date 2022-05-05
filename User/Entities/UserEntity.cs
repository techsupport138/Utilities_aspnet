using Microsoft.AspNetCore.Identity;
using Utilities_aspnet.Product;
using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser {
    public UserEntity() {
        ContactInformation = new List<ContactInformationEntity>();
        Media = new List<MediaEntity>();
        BookMark = new List<BookMarkEntity>();
    }

    public bool? Suspend { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public double? Wallet { get; set; }
    public DateTime? Birthdate { get; set; }

    public Guid? LocationId { get; set; }
    public LocationEntity? Location { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }
    public ICollection<UserToColorEntity>? Colors { get; set; }
    public ICollection<UserToFavoriteEntity>? Favorites { get; set; }
    public ICollection<UserToSpecialtyEntity>? Specialties { get; set; }
    public ICollection<ShoppingListEntity>? ShoppingList { get; set; }
    public ICollection<BookMarkEntity>? BookMark { get; set; }
    public ICollection<ProductEntity>? Product { get; set; }
    public ICollection<ContactInformationEntity>? ContactInformation { get; set; }

    public static UserReadDto MapReadDto(UserEntity? e) {
        UserReadDto dto = new() {
            Id = e.Id,
            Bio = e.Bio,
            BirthDate = e.Birthdate,
            FullName = e.FullName,
            UserName = e.UserName,
            PhoneNumber = e.PhoneNumber,
            AppPhoneNumber = e.AppPhoneNumber,
            AppUserName = e.AppUserName,
            Media = MediaEntity.MapEnumarableDto(e.Media)
        };

        return dto;
    }

    public static IEnumerable<UserReadDto> MapEnumarableDto(IEnumerable<UserEntity>? e) {
        IEnumerable<UserReadDto> dto = new List<UserReadDto>(e?.Select(MapReadDto) ?? Array.Empty<UserReadDto>());
        return dto;
    }
}