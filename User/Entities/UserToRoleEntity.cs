using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.User.Entities
{
    public  class UserToRoleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public UserRoleEntity Role { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleID { get; set; } 


        public UserEntity User { get; set; }

        [Required]
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
    }
}
