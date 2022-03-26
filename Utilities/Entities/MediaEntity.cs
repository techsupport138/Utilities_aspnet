using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Extensions;
using Utilities_aspnet.ServiceProvider.Entities;
using Utilities_aspnet.User.Entities;
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
        public string? ContentId { get; set; }

        public ContactInfoItemEntity? ContactInfoItem { get; set; }

        [ForeignKey("ContactInfoItem")]
        public string? ContactInfoItemId { get; set; }

        public virtual ServiceProviderEntity? ServiceProvider { get; set; }

        [ForeignKey("ServiceProvider")]
        public string? ServiceProviderId { get; set; }

        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [NotMapped]
        public string Link => $"{NetworkUtil.ServerAddress}/Medias/{FileName}";
    }
}