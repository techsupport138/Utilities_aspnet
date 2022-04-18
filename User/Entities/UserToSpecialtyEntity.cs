using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.User.Entities
{
    [Table("UserToSpecialty")]
    public class UserToSpecialtyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public SpecialtyEntity Specialty { get; set; }
        [ForeignKey(nameof(Specialty))]
        public Guid SpecialtyId { get; set; }


        public UserEntity User { get; set; }
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
    }
}
