using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Statistic.Entities
{
    [Table("Visitor")]
    public class VisitorEntity
    {
        [Key]
        public long VisitorId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        [Column("IP")]
        [StringLength(50)]
        public string Ip { get; set; }
        [StringLength(500)]
        public string LandingPage { get; set; }

        [ForeignKey(nameof(Date))]
        [InverseProperty(nameof(DimDate.Visitors))]
        public virtual DimDate DateNavigation { get; set; }
    }
}
