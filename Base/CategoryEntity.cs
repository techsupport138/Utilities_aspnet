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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        public Guid? ParentId { get; set; } = null;


        [ForeignKey(nameof(MediaId))]
        public MediaEntity? Media { get; set; }
        public Guid? MediaId { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public LanguageEntity Language { get; set; }
        [StringLength(5)]
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
