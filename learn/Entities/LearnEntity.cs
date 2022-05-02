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
    public class LearnEntity : BaseContentEntity
    {

        public LearnEntity()
        {
            this.UseCase = Utilities.Enums.ContentUseCase.Learn;
            Id=Guid.NewGuid();

        }

        [StringLength(200)]
        [Display(Name = "ناشر")]
        public string Publisher { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "تائیدیه ها")]
        public string Confirmations { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "افتخارات")]
        public string Honors { get; set; }

        

        [Display(Name = "قیمت")]
        public int? Amount { get; set; }

        
    }
}
