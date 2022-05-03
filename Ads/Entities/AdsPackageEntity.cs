using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Ads.Entities
{
    [Table("AdsPackage")]
    public class AdsPackageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شناسه")]
        public int AdsPackageId { get; set; }
        
        [DefaultValue("fa-IR")]
        public string LanguageId { get; set; } = "fa-IR";


        [Display(Name = "فعال")]
        public bool Enable { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        //[Required]
        
        [Display(Name = "توضیحات")]
        public string? Details { get; set; }
        
        [Display(Name = "قیمت")]
        public decimal Amount { get; set; }
        [Display(Name = "تعداد روز ویژه")]
        public int SpecialExpireDate { get; set; }
        [Display(Name = "تعداد روز صفحه اصلی")]
        public int HomePageExpireDate { get; set; }


        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("AdsPackages")]
        public virtual LanguageEntity LanguageNavigation { get; set; }


    }
}
