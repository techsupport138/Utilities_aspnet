using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Entities
{
    [Table("Favorite")]
    public class FavoriteEntity : BaseEntity
    {
        [StringLength(100)]
        public string Title { get; set; }
    }
}
