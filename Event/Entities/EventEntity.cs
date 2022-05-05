using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.Event.Entities
{
    public class EventEntity: BaseContentEntity
    {
        [StringLength(100)]
        public string EventDate { get; set; }

        [StringLength(200)]
        public string EventLocation { get; set; }

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public virtual LocationEntity Location { get; set; }

        //[Display(Name = "کشور")]
        //public int CountryId { get; set; }
        //[Display(Name = "استان")]
        //public int ProvinceId { get; set; }
        //[Display(Name = "شهر")]
        //public int CityId { get; set; }

        //[ForeignKey(nameof(CityId))]
        //public virtual City City { get; set; }
        //[ForeignKey(nameof(CountryId))]
        //public virtual Country Country { get; set; }
        //[ForeignKey(nameof(ProvinceId))]
        //public virtual Province Province { get; set; }

    }
}
