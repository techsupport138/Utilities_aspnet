using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Statistic.Entities
{
    [Table("DimDate")]
    public class DimDate
    {
        public DimDate()
        {
            Statistics = new HashSet<StatisticEntity>();
            Visitors = new HashSet<VisitorEntity>();
        }

        [Key]
        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; }
        public int? PersianDateKey { get; set; }
        [StringLength(10)]
        public string PersianDateValue { get; set; }
        public int? PersianYear { get; set; }
        public int? PersianMonth { get; set; }
        public int? PersianDayOfMonth { get; set; }
        [StringLength(15)]
        public string PersianMonthYear { get; set; }
        public int? PersianSeason { get; set; }
        public int? PersianDayOfWeek { get; set; }
        [StringLength(10)]
        public string PersianDayOfWeekName { get; set; }
        [StringLength(10)]
        public string PersianSeasonName { get; set; }
        [StringLength(10)]
        public string PersianMonthName { get; set; }
        public int? PersianMonthOfSeason { get; set; }
        public int? PersianDayOfYear { get; set; }
        public int? PersianWeekOfYear { get; set; }
        public int? PersianWeekRemainedFromYear { get; set; }
        [Column("persianDayRemainedFromyear")]
        public int? PersianDayRemainedFromyear { get; set; }

        [InverseProperty(nameof(StatisticEntity.DateNavigation))]
        public virtual ICollection<StatisticEntity> Statistics { get; set; }
        [InverseProperty(nameof(VisitorEntity.DateNavigation))]
        public virtual ICollection<VisitorEntity> Visitors { get; set; }
    }
}
