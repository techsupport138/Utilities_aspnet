using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities
{
    public class PostEntity : BasePEntity
    {
        [StringLength(250)]
        public string? Title { get; set; }
        [StringLength(250)]
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public bool IsSell { get; set; }
        public decimal Price { get; set; } = 0;
        public Guid? LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public LocationEntity? Location { get; set; }        
        public ICollection<UserEntity>? TeamUsers { get; set; }
        //public ICollection<PostCategoryEntity>? PostCategories { get; set; }
    }
}
