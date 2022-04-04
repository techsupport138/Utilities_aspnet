//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities_aspnet.Base;
//using Utilities_aspnet.Utilities.Entities;

//namespace Utilities_aspnet.Product.Entities
//{
//    public class ProjectCategoryEntity
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int ProjectCategoryId { get; set; }
//        public int? ParentId { get; set; } = null;


//        [ForeignKey(nameof(MediaId))]
//        public MediaEntity? Media { get; set; }
//        public Guid? MediaId { get; set; }


//        [Required]
//        [StringLength(200)]
//        public string Title { get; set; }


//        [ForeignKey(nameof(ParentId))]
//        [InverseProperty(nameof(ProjectCategoryEntity.InverseParent))]
//        public virtual ProjectCategoryEntity Parent { get; set; }
//        [InverseProperty(nameof(ProjectCategoryEntity.Parent))]
//        public virtual ICollection<ProjectCategoryEntity> InverseParent { get; set; }
//    }
//}
