using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.User.Entities
{
    public class UserToColorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ColorEntity Color { get; set; }

        [Required]
        [ForeignKey(nameof(Color))]
        public Guid ColorId { get; set; }


        public UserEntity User { get; set; }

        [Required]
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } 
    }
}
