using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Report.Entities
{
    [Table("ReportType")]
    public class ReportTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شناسه")]
        public int ReportTypeId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "نوع تخلف")]
        public string ReportTypeName { get; set; }
    }
}
