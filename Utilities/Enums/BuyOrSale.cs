using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Enums
{
    public enum BuyOrSale:byte
    {
        /// <summary>
        /// BUY
        /// </summary>
        [Display(Name ="Buy")]
        Buy=1,
        /// <summary>
        /// SALE
        /// </summary>
        [Display(Name = "Sale")]
        Sale =2
    }
}
