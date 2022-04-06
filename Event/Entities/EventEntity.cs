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

        

    }
}
