using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.User.Dtos
{
    public class ShoppingDto
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public BuyOrSale BuyOrSale { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public MediaEntity Media { get; set; }
        public string OrderId { get; set; }
    }
}
