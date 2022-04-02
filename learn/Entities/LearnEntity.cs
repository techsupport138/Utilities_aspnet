using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.learn.Entities
{
    public class LearnEntity:BaseEntity
    {
        public LearnCategoryEntity LearnCategory { get; set; }
        public string LearnCategoryId { get; set; }

        public bool Enable { get; set; }


        [StringLength(50)]
        public string Title { get; set; }
        

        [Column(TypeName = "ntext")]
        [Display(Name = "خلاصه")]
        public string Lid { get; set; }

        public int? Amount { get; set; } = null;

        public ICollection<MediaEntity>? Media { get; set; }
    }
}
