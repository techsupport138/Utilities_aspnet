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
    public class LearnCategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LearnCategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [ForeignKey(nameof(MediaId))]
        public MediaEntity? Media { get; set; }
        public Guid? MediaId { get; set; }
    }
}
