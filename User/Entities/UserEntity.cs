using Utilities_aspnet.Follow.Entities;

namespace Utilities_aspnet.User.Entities;

public class UserEntity : IdentityUser
{
    public UserEntity()
    {
        Location = new List<LocationEntity>();
        FormBuilders = new List<FormEntity>();
        Media = new List<MediaEntity>();
        Colors = new List<ColorEntity>();
        Favorites = new List<FavoriteEntity>();
        ContactInformation = new List<ContactInformationEntity>();
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
        Specialties = new List<SpecialityEntity>();
        //Follower = new List<FollowEntity>();
        //Following = new List<FollowEntity>();
    }

    public bool Suspend { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public double Wallet { get; set; } = 0;
    public DateTime? Birthdate { get; set; }

    //public List<FollowEntity> Follower { get; set; }
    //public List<FollowEntity> Following { get; set; }
    public List<LocationEntity> Location { get; set; }
    public List<FormEntity> FormBuilders { get; set; }
    public List<MediaEntity> Media { get; set; }
    public List<ColorEntity> Colors { get; set; }
    public List<FavoriteEntity> Favorites { get; set; }
    public List<ContactInformationEntity> ContactInformation { get; set; }
    public List<ReportEntity> Reports { get; set; }
    public List<ProductEntity> Products { get; set; }
    public List<ProjectEntity> Projects { get; set; }
    public List<TutorialEntity> Tutorials { get; set; }
    public List<EventEntity> Events { get; set; }
    public List<AdEntity> Ads { get; set; }
    public List<CompanyEntity> Companys { get; set; }
    public List<TenderEntity> Tenders { get; set; }
    public List<ServiceEntity> Services { get; set; }
    public List<MagazineEntity> Magazines { get; set; }
    public List<SpecialityEntity> Specialties { get; set; }
}