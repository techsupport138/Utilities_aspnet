using Utilities_aspnet.FormBuilder;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser {
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

    public IEnumerable<LocationEntity>? Location { get; set; }
    public IEnumerable<FormEntity>? FormBuilders { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<ColorEntity>? Colors { get; set; }
    public IEnumerable<FavoriteEntity>? Favorites { get; set; }
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
}