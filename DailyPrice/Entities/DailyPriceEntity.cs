using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.DailyPrice.Entities
{
    public class DailyPriceEntity: BaseEntity
    {
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }

        public decimal Amount { get; set; }
    }
}
