namespace Utilities_aspnet.Utilities.Entities; 

public class MediaEntity : BaseEntity {
    [Required]
    public string FileName { get; set; }

    [Required]
    public FileTypes FileType { get; set; }

    public string UseCase { get; set; } = "--";

    public ContentEntity? Content { get; set; }
    public Guid? ContentId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public virtual JobEntity? Job { get; set; }
    public Guid? JobId { get; set; }

    public virtual TenderEntity? Tender { get; set; }
    public Guid? TenderId { get; set; }

    public virtual LearnEntity? LearnEntity { get; set; }
    public Guid? LearnId { get; set; }


    public ContactInfoItemEntity? ContactInfoItem { get; set; }
    public Guid? ContactInfoItemId { get; set; }

    public virtual ServiceProviderEntity? ServiceProvider { get; set; }
    public Guid? ServiceProviderId { get; set; }

    public virtual AdsEntity? Ads { get; set; }
    public Guid? AdsId { get; set; }

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    [NotMapped]
    public string Link => $"{Server.ServerAddress}/Medias/{FileName}";

    public static MediaDto MapMediaDto(MediaEntity e) {
        MediaDto dto = new MediaDto {
            Id = e.Id.ToString(),
            Link = e.Link,
            Type = e.FileType,
            UseCase = e.UseCase
        };

        return dto;
    }

    public static IEnumerable<MediaDto> MapMediaEnumarableDto(IEnumerable<MediaEntity>? e) {
        IEnumerable<MediaDto> dto = new List<MediaDto>(e?.Select(MapMediaDto) ?? Array.Empty<MediaDto>());
        return dto;
    }
}