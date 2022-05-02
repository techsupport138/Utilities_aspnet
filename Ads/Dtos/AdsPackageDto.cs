using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Ads.Dtos
{
    public class AdsPackageDto
    {
        public int? AdsPackageId { get; set; }
        public string LanguageId { get; set; } = "fa-IR";
        public bool Enable { get; set; }
        public string Title { get; set; }
        public string? Details { get; set; }
        public decimal Amount { get; set; }
        public int SpecialExpireDate { get; set; }
        public int HomePageExpireDate { get; set; }
        public string UserId { get; set; }
    }
}
