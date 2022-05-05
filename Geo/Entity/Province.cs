//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities_aspnet.Ads.Entities;
//using Utilities_aspnet.Job.Entities;
//using Utilities_aspnet.Product.Entities;
//using Utilities_aspnet.Tender.Entities;

//namespace Utilities_aspnet.Geo.Entity
//{
//    [Table("Province")]
//    public class Province
//    {
//        public Province()
//        {
//            Ads = new HashSet<AdsEntity>();
//            Cities = new HashSet<City>();
//            Jobs = new HashSet<JobEntity>();
//            Projects = new HashSet<ProjectEntity>();
//            Tenders = new HashSet<TenderEntity>();
//        }

//        [Key]
//        [Display(Name = "شناسه")]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int ProvinceId { get; set; }
//        [Display(Name = "کشور")]
//        public int CountryId { get; set; }
//        //[Required]
//        [StringLength(50)]
//        [Display(Name = "نام استان")]
//        public string ProvinceName { get; set; }

//        [ForeignKey(nameof(CountryId))]
//        [InverseProperty("Provinces")]
//        public virtual Country Country { get; set; }

//        [InverseProperty(nameof(City.Province))]
//        public virtual ICollection<City> Cities { get; set; }


//        [InverseProperty(nameof(AdsEntity.Province))]
//        public virtual ICollection<AdsEntity> Ads { get; set; }
//        //[InverseProperty(nameof(JobEntity.ProvinceId))]
//        public virtual ICollection<JobEntity> Jobs { get; set; }
//        //[InverseProperty(nameof(ProjectEntity.ProvinceId))]
//        public virtual ICollection<ProjectEntity> Projects { get; set; }
//        //[InverseProperty(nameof(TenderEntity.ProvinceId))]
//        public virtual ICollection<TenderEntity> Tenders { get; set; }



//    }
//}
