using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Geo.Entity;
using Utilities_aspnet.Tender.Enum;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;
namespace Utilities_aspnet.Tender.Entities
{
    public class TenderEntity: BasePEntity
    {
        public TenderType Type { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "تاریخ انتشار")]
        public string PublishDate { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "شرکت")]
        public string CompanyName { get; set; }
        [Display(Name = "کشور")]
        public int CountryId { get; set; }
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
        [Display(Name = "شهر")]
        public int CityId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "تلفن")]
        public string Tel { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "پست الکترونیک")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(200)]
        [Display(Name = "وب سایت")]
        [DataType(DataType.Url)]
        public string WebSite { get; set; }
        [StringLength(200)]
        [Display(Name = "موقعیت")]
        public string Location { get; set; }
        
        [Display(Name = "شرایط")]
        public string Conditions { get; set; }


        public ICollection<MediaEntity>? Media { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Tenders")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Tenders")]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        [InverseProperty("Tenders")]
        public virtual Province Province { get; set; }


    }
}
