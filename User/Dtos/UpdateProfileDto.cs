using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Models.Dto
{
    public class UpdateProfileDto
    {
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? UserName { get; set; }
        public string? Headline { get; set; }
        public string? Education { get; set; }
        public string? Degree { get; set; }
        public Guid? ColorId { get; set; }
        public IEnumerable<ContactInformationCreateDto>? contactInformations { get; set; }
    }
}
