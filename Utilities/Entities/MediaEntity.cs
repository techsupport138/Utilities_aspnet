using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.ServiceProvider.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities {
    public class MediaEntity : BaseEntity {
        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public FileTypes FileType { get; set; }

        public string UseCase { get; set; } = "--";

        public ContentEntity? Content { get; set; }
        [ForeignKey("Content")]
        public long? ContentId { get; set; }

        public ContactInfoItemEntity? ContactInfoItem { get; set; }

        [ForeignKey("ContactInfoItem")]
        public long? ContactInfoItemId { get; set; }

        public virtual ServiceProviderEntity? ServiceProvider { get; set; }

        [ForeignKey("ServiceProvider")]
        public long? ServiceProviderId { get; set; }
    }
}