using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Ads.Dtos
{
    public class AdsDto
    {
        public AdsDto()
        {
            files = new List<IFormFile>();
        }
        public Guid? Id { get; set; } = null;
        public string LanguageId { get; set; } = "fa-IR";
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string? Lid { get; set; } = null;
        public string? Body { get; set; } = null;
        public decimal Amount { get; set; } = 0;
        public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;
        public DateTime? ExpireDateTime { get; set; }
        public DateTime? SpecialExpireDateTime { get; set; }
        public DateTime? HomePageExpireDateTime { get; set; }
        public bool Publish { get; set; } = false;
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int? NumberOfLikes { get; set; } = 0;

        public List<IFormFile> files { get; set; }
    }
}
