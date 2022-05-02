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
        Categories = new HashSet<CategoryEntity>();
        //Contents = new HashSet<ContentEntity>();
        //Events = new HashSet<EventEntity>();
        //Learns = new HashSet<LearnEntity>();
        //PostEntities = new HashSet<PostEntity>();
        //Products = new HashSet<ProductEntity>();
        //Projects = new HashSet<ProjectEntity>();
        //Tenders = new HashSet<TenderEntity>();

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
    [InverseProperty(nameof(CategoryEntity.LanguageNavigation))]
    public virtual ICollection<CategoryEntity> Categories { get; set; }
    //[InverseProperty(nameof(ContentEntity.Language))]
    //public virtual ICollection<ContentEntity> Contents { get; set; }
    //[InverseProperty(nameof(EventEntity.Language))]
    //public virtual ICollection<EventEntity> Events { get; set; }
    //[InverseProperty(nameof(LearnEntity.Language))]
    //public virtual ICollection<LearnEntity> Learns { get; set; }
    //[InverseProperty(nameof(PostEntity.Language))]
    //public virtual ICollection<PostEntity> PostEntities { get; set; }
    //[InverseProperty(nameof(ProductEntity.Language))]
    //public virtual ICollection<ProductEntity> Products { get; set; }
    //[InverseProperty(nameof(ProjectEntity.Language))]
    //public virtual ICollection<ProjectEntity> Projects { get; set; }
    //[InverseProperty(nameof(TenderEntity.Language))]
    //public virtual ICollection<TenderEntity> Tenders { get; set; }
}