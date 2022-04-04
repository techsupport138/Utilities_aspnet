//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities_aspnet.Base;
//using Utilities_aspnet.Utilities.Entities;

//namespace Utilities_aspnet.Product.Entities
//{
//    public class ProductCategoryEntity
//    {

//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int ProductCategoryId { get; set; }
//        public int? ParentId { get; set; } = null;


//        [ForeignKey(nameof(MediaId))]
//        public MediaEntity? Media { get; set; }
//        public Guid? MediaId { get; set; }


//        [Required]
//        [StringLength(200)]
//        public string Title { get; set; }


//        [ForeignKey(nameof(ParentId))]
//        [InverseProperty(nameof(ProductCategoryEntity.InverseParent))]
//        public virtual ProductCategoryEntity Parent { get; set; }
//        [InverseProperty(nameof(ProductCategoryEntity.Parent))]
//        public virtual ICollection<ProductCategoryEntity> InverseParent { get; set; }
//    }
//}
