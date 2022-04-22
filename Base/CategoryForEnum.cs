using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Base
{
    public enum CategoryForEnum : int
    {
        [Display(Name = "تبلیغات")]
        Ads = 1,
        [Display(Name = "قیمت روزانه")]
        DP = 2,
        [Display(Name = "رویداد ها")]
        Event = 3,
        [Display(Name = "مشاغل")]
        Job = 4,
        [Display(Name = "آموزش ها")]
        Learn = 5,
        [Display(Name = "محصولات")]
        product = 6,
        [Display(Name = "پروژه ها")]
        Project = 7,
        [Display(Name = "مناقصه و مزایده")]
        Tender = 8,

    }
}
