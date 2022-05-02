using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Language.Entities;

namespace Utilities_aspnet.Base
{
    public class CategoryEntity
    {

        public CategoryEntity()
        {
            LanguageNavigation = new LanguageEntity();
            InverseParent = new HashSet<CategoryEntity>();
            Parent = null;

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        public Guid? ParentId { get; set; }


        [ForeignKey(nameof(MediaId))]
        public virtual MediaEntity? Media { get; set; }
        public Guid? MediaId { get; set; }

        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("Categories")]
        public virtual LanguageEntity? LanguageNavigation { get; set; }
        public string LanguageId { get; set; } = "fa-IR";

        public CategoryForEnum CategoryFor { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(CategoryEntity.InverseParent))]
        public virtual CategoryEntity Parent { get; set; }
        [InverseProperty(nameof(CategoryEntity.Parent))]
        public virtual ICollection<CategoryEntity> InverseParent { get; set; }
    }
}
