using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Ads.Entities;
using Utilities_aspnet.Tender.Entities;
using Utilities_aspnet.DailyPrice.Entities;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.BookMark.Entities
{
    public class BookMarkEntity: BaseEntity
    {
        public UserEntity UserEntity { get; set; }
        public string UserId { get; set; }


        public AdsEntity? Ads { get; set; } = null!;
        public Guid? AdsId { get; set; } = null!;

        public ProjectEntity? Project { get; set; } = null!;
        public Guid? ProjectId { get; set; } = null!;

        public TenderEntity? Tender { get; set; } = null!;
        public Guid? TenderId { get; set; } = null!;

        public DPProductEntity? DailyPrice { get; set; } = null!;
        public Guid? DPProductId { get; set; } = null!;
        

    }
}
