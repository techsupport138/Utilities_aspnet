using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.DailyPrice.Entities
{
    public class DPProductCategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DPProductCategoryId { get; set; }
        public int? ParentId { get; set; } = null;


        [ForeignKey(nameof(MediaId))]
        public MediaEntity? Media { get; set; }
        public Guid? MediaId { get; set; }


        [Required]
        [StringLength(200)]
        public string Title { get; set; }


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(DPProductCategoryEntity.InverseParent))]
        public virtual DPProductCategoryEntity Parent { get; set; }
        [InverseProperty(nameof(DPProductCategoryEntity.Parent))]
        public virtual ICollection<DPProductCategoryEntity> InverseParent { get; set; }
    }
}
