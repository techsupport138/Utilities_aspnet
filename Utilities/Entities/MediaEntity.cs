using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities {
    public class MediaEntity : BaseEntity {
        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public FileTypes FileType { get; set; }

        public string UseCase { get; set; } = "--";

        public ContentEntity? Content { get; set; }
        public int? ContentId { get; set; }

        public ContactInfoItemEntity? ContactInfoItem { get; set; }
        public int? ContactInfoItemId { get; set; }
    }
}