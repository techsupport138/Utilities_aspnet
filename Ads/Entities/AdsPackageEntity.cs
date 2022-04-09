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
        [Display(Name = "فعال")]
        public bool Enable { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        //[Required]
        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        public string? Details { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "قیمت")]
        public decimal Amount { get; set; }
        [Display(Name = "تعداد روز ویژه")]
        public int SpecialExpireDate { get; set; }
        [Display(Name = "تعداد روز صفحه اصلی")]
        public int HomePageExpireDate { get; set; }
    }
}
