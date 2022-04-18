using Utilities_aspnet.Utilities.Dtos;

namespace Utilities_aspnet.User.Dtos {
    public class UpdateProfileDto {

        public UpdateProfileDto()
        {
            Colors = new List<Guid>();
            Specialties = new List<Guid>();
            Favorites = new List<Guid>();
        }
        public List<Guid> Colors { get; set; }
        public List<Guid> Specialties { get; set; }
        public List<Guid> Favorites { get; set; }


        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? UserName { get; set; }
        public string? Headline { get; set; }
        public string? Education { get; set; }
        public string? Degree { get; set; }

        public string? WebSite { get; set; }
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }
        public bool? PublicBio { get; set; }

        public int? Birth_Year { get; set; } 
        public int? Birth_Month { get; set; }
        public int? Birth_Day { get; set; } 

        public Guid? ColorId { get; set; }
        public IEnumerable<ContactInformationCreateDto>? ContactInformation { get; set; }
    }
}