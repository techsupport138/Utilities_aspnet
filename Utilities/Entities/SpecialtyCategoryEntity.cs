using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Entities
{
    [Table("SpecialtyCategory")]
    public class SpecialtyCategoryEntity:BaseEntity
    {
        [StringLength(100)]
        public string SpecialtyCategoryTitle { get; set; }        
    }
}
