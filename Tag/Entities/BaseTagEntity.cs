using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Tag.Entities {
    public class BaseTagEntity : BaseEntity {
        [Required]
        public string Title { get; set; } 

        public string? Link { get; set; }
        public ICollection<MediaEntity>? Media { get; set; }
    }
}