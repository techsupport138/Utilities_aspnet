using Microsoft.AspNetCore.Identity;
using Utilities_aspnet.Product;
using Utilities_aspnet.Report;
using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser {
    public UserEntity() {
        ContactInformation = new List<ContactInformationEntity>();
        Location = new List<LocationEntity>();
        Colors = new List<UserToColorEntity>();
        Favorites = new List<UserToFavoriteEntity>();
        ShoppingList = new List<ShoppingListEntity>();
        Reports = new List<ReportEntity>();
        Products = new List<ProductEntity>();
        Projects = new List<ProjectEntity>();
        Tutorials = new List<TutorialEntity>();
        Events = new List<EventEntity>();
        Ads = new List<AdEntity>();
        Companys = new List<CompanyEntity>();
        Tenders = new List<TenderEntity>();
        Services = new List<ServiceEntity>();
        Magazines = new List<MagazineEntity>();
    }
    
    public IEnumerable<LocationEntity>? Location { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<UserToColorEntity>? Colors { get; set; }
    public IEnumerable<UserToFavoriteEntity>? Favorites { get; set; }
    public IEnumerable<ShoppingListEntity>? ShoppingList { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformation { get; set; }
    public IEnumerable<ReportEntity>? Reports { get; set; }
    public IEnumerable<ProductEntity>? Products { get; set; }
    public IEnumerable<ProjectEntity>? Projects { get; set; }
    public IEnumerable<TutorialEntity>? Tutorials { get; set; }
    public IEnumerable<EventEntity>? Events { get; set; }
    public IEnumerable<AdEntity>? Ads { get; set; }
    public IEnumerable<CompanyEntity>? Companys { get; set; }
    public IEnumerable<TenderEntity>? Tenders { get; set; }
    public IEnumerable<ServiceEntity>? Services { get; set; }
    public IEnumerable<MagazineEntity>? Magazines { get; set; }

    public bool? Suspend { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public double Wallet { get; set; } = 0;
    public DateTime? Birthdate { get; set; }

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