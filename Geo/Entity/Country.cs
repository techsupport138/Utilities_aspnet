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
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Utilities_aspnet.Geo.Entity
//{
//    [Table("Country")]
//    public class Country
//    {
//        public Country()
//        {            
//            Ads = new HashSet<AdsEntity>();
//            Jobs = new HashSet<JobEntity>();
//            Projects = new HashSet<ProjectEntity>();
//            Provinces = new HashSet<Province>();
//            Tenders = new HashSet<TenderEntity>();
//        }
//        [Key]
//        [Display(Name = "شناسه")]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int CountryId { get; set; }
//        [StringLength(200)]
//        [Display(Name = "نام کشور")]
//        public string CountryName { get; set; }

//        [InverseProperty(nameof(AdsEntity.Country))]
//        public virtual ICollection<AdsEntity> Ads { get; set; }
//        [InverseProperty(nameof(JobEntity.Country))]
//        public virtual ICollection<JobEntity> Jobs { get; set; }
//        public virtual ICollection<ProjectEntity> Projects { get; set; }
//        [InverseProperty(nameof(Province.Country))]
//        public virtual ICollection<Province> Provinces { get; set; }
//        [InverseProperty(nameof(TenderEntity.Country))]
//        public virtual ICollection<TenderEntity> Tenders { get; set; }
//    }
//}
