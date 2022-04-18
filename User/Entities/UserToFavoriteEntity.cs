using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.User.Entities
{
    [Table("UserToFavorite")]
    public class UserToFavoriteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public FavoriteEntity Favorite { get; set; }

        [Required]
        [ForeignKey(nameof(Favorite))]
        public Guid FavoriteId { get; set; }


        public UserEntity User { get; set; }

        [Required]
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
    }
}
