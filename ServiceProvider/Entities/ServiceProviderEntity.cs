using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.Comment.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.ServiceProvider.Entities;

[Table("ServiceProvider")]
public class ServiceProviderEntity : BaseEntity
{
    [Required]
    public string Type { get; set; } = default!;

    [Required]
    public int Point { get; set; } = 0;

    public double? Latitude { get; set; }
    public double? stringitude { get; set; }
    public string? Services { get; set; }
    public string? Address { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformation { get; set; }
    public IEnumerable<CommentEntity>? Comment { get; set; }
}