using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Ads.Entities
{
    public class AdsCategoryEntity
    {
        public AdsCategoryEntity()
        {
            InverseParent = new HashSet<AdsCategoryEntity>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdsCategoryId { get; set; }
        public int? ParentId { get; set; } = null;

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public bool Enable { get; set; }

        


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(AdsCategoryEntity.InverseParent))]
        public virtual AdsCategoryEntity Parent { get; set; }
        [InverseProperty(nameof(AdsCategoryEntity.Parent))]
        public virtual ICollection<AdsCategoryEntity> InverseParent { get; set; }
    }
}
