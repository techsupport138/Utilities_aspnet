using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.learn.Entities
{
    public class LearnEntity : BaseEntity
    {
        public LearnCategoryEntity LearnCategory { get; set; }
        [Display(Name = "دسته بندی")]
        public string LearnCategoryId { get; set; }

        [Display(Name = "فعال")]
        public bool Enable { get; set; }


        [StringLength(200)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [StringLength(200)]
        [Display(Name = "ناشر")]
        public string Publisher { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "تائیدیه ها")]
        public string Confirmations { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "افتخارات")]
        public string Honors { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "خلاصه")]
        public string Lid { get; set; }

        [Display(Name = "قیمت")]
        public int? Amount { get; set; } = null;

        public ICollection<MediaEntity>? Media { get; set; }
    }
}
