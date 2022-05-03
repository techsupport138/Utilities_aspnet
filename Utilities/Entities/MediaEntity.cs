using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;

using Utilities_aspnet.Job.Entities;
using Utilities_aspnet.learn.Entities;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Tender.Entities;
using Utilities_aspnet.ServiceProvider.Entities;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.DailyPrice.Entities;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Utilities.Entities {
    public class MediaEntity : BaseEntity {
        [Required]
        public string FileName { get; set; } 

        [Required]
        public FileTypes FileType { get; set; }

        public string UseCase { get; set; } = "--";

        public ContentEntity? Content { get; set; }
        [ForeignKey("Content")]
        public Guid? ContentId { get; set; }

        public ProductEntity? Product { get; set; }
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        
        public virtual JobEntity? Job { get; set; }
        [ForeignKey("JobEntity")]
        public Guid? JobId { get; set; }

        public virtual TenderEntity? Tender { get; set; }
        [ForeignKey("TenderEntity")]
        public Guid? TenderId { get; set; }

        public virtual LearnEntity? LearnEntity { get; set; }

        [ForeignKey("LearnEntity")]
        public Guid? LearnId { get; set; }


        public ContactInfoItemEntity? ContactInfoItem { get; set; }

        [ForeignKey("ContactInfoItem")]
        public Guid? ContactInfoItemId { get; set; }

        public virtual ServiceProviderEntity? ServiceProvider { get; set; }
        [ForeignKey("ServiceProvider")]
        public Guid? ServiceProviderId { get; set; }
        
        public virtual AdsEntity? Ads { get; set; }
        [ForeignKey("AdsId")]
        public Guid? AdsId { get; set; }


        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [NotMapped]
        public string Link => $"{Server.ServerAddress}/Medias/{FileName}";
    }
}