using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities
{
    public class ProjectCategoryEntity: BaseEntity
    {

        public Guid MediaId { get; set; }
        [ForeignKey(nameof(MediaId))]
        public MediaEntity Media { get; set; }
        [StringLength(200)]
        public string ProjectCategoryTitle { get; set; }
    }
}
