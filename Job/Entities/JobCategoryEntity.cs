using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Job.Entities
{
    public class JobCategoryEntity
    {
        [Key]
        [Display(Name = "شناسه")]
        public int JobCategoryId { get; set; }
        [Display(Name = "والد")]
        public int? JobParentCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "دسته بندی شغل")]
        public string JobCategoryName { get; set; }

        public MediaEntity? Media { get; set; }
        public Guid? MediaId { get; set; }

        [ForeignKey(nameof(JobParentCategoryId))]
        [InverseProperty(nameof(JobCategoryEntity.InverseJobParentCategory))]
        public virtual JobCategoryEntity JobParentCategory { get; set; }
        [InverseProperty(nameof(JobCategoryEntity.JobParentCategory))]
        public virtual ICollection<JobCategoryEntity> InverseJobParentCategory { get; set; }
        [InverseProperty(nameof(JobEntity.JobCategory))]
        public virtual ICollection<JobEntity> Jobs { get; set; }
    }
}
