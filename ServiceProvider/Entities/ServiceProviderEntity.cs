using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Comment.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.ServiceProvider.Entities;

public class ServiceProviderEntity : BaseEntity {
    [Required]
    public string Type { get; set; } = default!;

    [Required]
    public int Point { get; set; } = 0;

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Services { get; set; }
    public string? Address { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformation { get; set; }
    public IEnumerable<CommentEntity>? Comment { get; set; }
}