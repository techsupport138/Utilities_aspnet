using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Entities
{
    [Table("Specialty")]
    public class SpecialtyEntity:BaseEntity
    {
        [StringLength(100)]
        public string SpecialtyTitle { get; set; }

        public SpecialtyCategoryEntity Category { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        public MediaEntity Media { get; set; }
        [ForeignKey(nameof(Media))]
        public Guid MediaId { get; set; }
    }
}
