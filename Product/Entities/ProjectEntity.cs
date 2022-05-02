using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.Comment.Entities;
using Utilities_aspnet.Geo.Entity;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class ProjectEntity : BasePEntity {

    [Required]
    [StringLength(200)]
    public string Subtitle { get; set; } 

    [Display(Name = "کشور")]
    public int CountryId { get; set; }
    [Display(Name = "استان")]
    public int ProvinceId { get; set; }
    [Display(Name = "شهر")]
    public int CityId { get; set; }

    [ForeignKey(nameof(CityId))]
    public virtual City City { get; set; }
    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; }
    [ForeignKey(nameof(ProvinceId))]
    public virtual Province Province { get; set; }

    public IEnumerable<CommentEntity>? Comment { get; set; }

    public ICollection<MediaEntity>? Media { get; set; }

} 

