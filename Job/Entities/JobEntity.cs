using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Geo.Entity;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Job.Entities
{
    public class JobEntity: BaseContentEntity
    {
        //[StringLength(200)]
        //public string Title { get; set; }

        //public UserEntity UserEntity { get; set; }
        //public string UserId { get; set; }

        //public CategoryEntity JobCategory { get; set; }
        //public Guid CategoryId { get; set; }

        //public bool Enable { get; set; }

        [Display(Name = "کشور")]
        public int CountryId { get; set; } 
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
        [Display(Name = "شهر")]
        public int CityId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        //public ICollection<MediaEntity>? Media { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        public virtual Province Province { get; set; }

    }
}
