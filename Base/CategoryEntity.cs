
using Utilities_aspnet.Product;

namespace Utilities_aspnet.Base
{
    public class CategoryEntity
    {

        public CategoryEntity()
        {
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

        public CategoryForEnum CategoryFor { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(CategoryEntity.InverseParent))]
        public virtual CategoryEntity Parent { get; set; }
        [InverseProperty(nameof(CategoryEntity.Parent))]
        public virtual ICollection<CategoryEntity> InverseParent { get; set; }

        public IEnumerable<ProductEntity> Product { get; set; }
    }
}
