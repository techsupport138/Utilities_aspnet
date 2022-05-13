using Utilities_aspnet.IdTitle;
using Utilities_aspnet.Product;

namespace Utilities_aspnet.Utilities.Entities;

public class MediaEntity : BaseEntity {
    [Required]
    public string FileName { get; set; }

    [Required]
    public FileTypes FileType { get; set; }

    public string UseCase { get; set; } = "--";

    public ContentEntity? Content { get; set; }
    public Guid? ContentId { get; set; }

    public ContactInfoItemEntity? ContactInfoItem { get; set; }
    public Guid? ContactInfoItemId { get; set; }

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }
    
    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public ProjectEntity? Project { get; set; }
    public Guid? ProjectId { get; set; }

    public TutorialEntity? Tutorial { get; set; }
    public Guid? TutorialId { get; set; }

    public EventEntity? Event { get; set; }
    public Guid? EventId { get; set; }

    public AdEntity? Ad { get; set; }
    public Guid? AdId { get; set; }

    public CompanyEntity? Company { get; set; }
    public Guid? CompanyId { get; set; }

    public TenderEntity? Tender { get; set; }
    public Guid? TenderId { get; set; }

    public ServiceEntity? Service { get; set; }
    public Guid? ServiceId { get; set; }

    public MagazineEntity? Magazine { get; set; }
    public Guid? MagazineId { get; set; }
    
    public TagEntity? Tag { get; set; }
    public Guid? TagId { get; set; }

    public SpecialityEntity? Speciality { get; set; }
    public Guid? SpecialityId { get; set; }
    
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

    public static IEnumerable<MediaDto> MapEnumarableDto(IEnumerable<MediaEntity>? e) {
        IEnumerable<MediaDto> dto = new List<MediaDto>(e?.Select(MapMediaDto) ?? Array.Empty<MediaDto>());
        return dto;
    }
}