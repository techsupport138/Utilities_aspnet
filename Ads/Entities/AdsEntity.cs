using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Geo.Entity;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Ads.Entities
{
    public class AdsEntity: BaseEntity
    {
        [StringLength(200)]
        public string Title { get; set; }
        public UserEntity UserEntity { get; set; }
        public string UserId { get; set; }

        public AdsCategoryEntity AdsCategory { get; set; }
        public string CategoryId { get; set; }


        public bool Enable { get; set; }


        [Display(Name = "کشور")]
        public int CountryId { get; set; }
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
        [Display(Name = "شهر")]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Ads")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Ads")]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        [InverseProperty("Ads")]
        public virtual Province Province { get; set; }
    }
}
