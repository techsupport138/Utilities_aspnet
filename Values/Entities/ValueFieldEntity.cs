using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Values.Entities
{
    [Table("ValueField")]
    public class ValueFieldEntity : BaseEntity
    {
        public ValueType TypeId { get; set; }

        [StringLength(100)]
        public string Lable { get; set; }
        public bool Required { get; set; }

        [StringLength(500)]
        public string OptionList { get; set; }

        public int OrderId { get; set; }

        public Guid? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity? Category { get; set; }


        public int? RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public UserRoleEntity? UserRole { get; set; }
        

    }
}
