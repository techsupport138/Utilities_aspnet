using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilities_aspnet.Language.Entities;

[Table("Languages")]
public class LanguageEntity
{
   public LanguageEntity()
    {
        Ads = new HashSet<AdsEntity>();
        AdsPackages = new HashSet<AdsPackageEntity>();
        
    }

    [Key]
    [StringLength(5)]
    public string Symbol { get; set; }
    [Required]
    [StringLength(50)]
    public string LanguageName { get; set; }



    [InverseProperty(nameof(AdsEntity.LanguageNavigation))]
    public virtual ICollection<AdsEntity> Ads { get; set; }
    [InverseProperty(nameof(AdsPackageEntity.LanguageNavigation))]
    public virtual ICollection<AdsPackageEntity> AdsPackages { get; set; }
    
}