using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utilities_aspnet.Ads.Entities;
using Utilities_aspnet.Job.Entities;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Tender.Entities;


namespace Utilities_aspnet.Geo.Entity
{
    public class City
    {
        [Key]
        [Display(Name = "شناسه")]
        public int CityId { get; set; }
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "شهر")]
        public string CityName { get; set; }

        [ForeignKey(nameof(ProvinceId))]
        public virtual Province Province { get; set; }


        [InverseProperty(nameof(AdsEntity.City))]
        public virtual ICollection<AdsEntity> Ads { get; set; }
        [InverseProperty(nameof(JobEntity.City))]
        public virtual ICollection<JobEntity> Jobs { get; set; }
        [InverseProperty(nameof(ProjectEntity.City))]
        public virtual ICollection<ProjectEntity> Projects { get; set; }
        [InverseProperty(nameof(TenderEntity.City))]
        public virtual ICollection<TenderEntity> Tenders { get; set; }

    }
}
