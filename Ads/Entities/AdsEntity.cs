using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Geo.Entity;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Ads.Entities
{
    public class AdsEntity : BaseEntity
    {
        public AdsEntity()
        {
            UserEntity = new UserEntity();
            Category = new CategoryEntity();
            City = new City();
            Country = new Country();
            Province = new Province();
            Id = new Guid();
        }



        
        public string LanguageId { get; set; } = "fa-IR";
        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("Ads")]
        public virtual LanguageEntity? LanguageNavigation { get; set; }


        [ForeignKey(nameof(UserId))]
        public UserEntity? UserEntity { get; set; }
        public string? UserId { get; set; } = null;


        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(500)]
        [Column(TypeName = "NVARCHAR")]
        [Required]
        public string Title { get; set; }

        [StringLength(10)]
        public string TinyURL { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "خلاصه")]
        public string? Lid { get; set; } = null;

        [Column(TypeName = "ntext")]
        [Display(Name = "متن")]
        public string? Body { get; set; } = null;

        [Column(TypeName = "money")]
        [Display(Name = "قیمت")]
        public decimal Amount { get; set; }

        public int NumberOfLikes { get; set; } = 0;

        public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;

        public ICollection<MediaEntity>? MediaList { get; set; }



        [Column(TypeName = "datetime")]
        public DateTime? ExpireDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SpecialExpireDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HomePageExpireDateTime { get; set; }

        [Display(Name = "انتشار")]
        public bool Publish { get; set; } = false;


        [Display(Name = "کشور")]
        public int CountryId { get; set; }
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
        [Display(Name = "شهر")]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Ads")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Ads")]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        [InverseProperty("Ads")]
        public virtual Province Province { get; set; }
    }
}
