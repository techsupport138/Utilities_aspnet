using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Geo.Entity;

namespace Utilities_aspnet.Product.Entities;

public class ProjectEntity : BaseProductEntity {
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Subtitle { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;

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
} 