using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.Tag.Dtos {
    public class CreateTagDto {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } 

        public string? Link { get; set; }
    }

    public class GetTagDto {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string? Link { get; set; }
    }

    public class UpdateTagDto {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } 

        public string? Link { get; set; }
    }
}