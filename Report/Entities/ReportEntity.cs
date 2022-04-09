using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Report.Entities
{
    [Table("Report")]
    public class ReportEntity:BaseEntity
    {
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(ReportTypeId))]
        public ReportTypeEntity ReportTypeEntity { get; set; }
        public int ReportTypeId { get; set; }

        public string? Title { get; set; } = null;
        public string? Body { get; set; } = null;
        public bool AdminRead { get; set; } = false;



        public Guid? AdsId { get; set; }
        [ForeignKey(nameof(AdsId))]
        public AdsEntity? AdsEntity { get; set; }

        public Guid? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity? ProductEntity { get; set; }

        public Guid? TenderId { get; set; }
        [ForeignKey(nameof(TenderId))]
        public TenderEntity? TenderEntity { get; set; }


    }
}
