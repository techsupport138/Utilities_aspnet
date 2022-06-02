namespace Utilities_aspnet.Comment;

[Table("Comment")]
public class CommentEntity : BaseEntity {
    public double? Score { get; set; } = 0;
    public string? Comment { get; set; }

    public Guid? ParentId { get; set; }
    public CommentEntity? Parent { get; set; }

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public DailyPriceEntity? DailyPrice { get; set; }
    public Guid? DailyPriceId { get; set; }

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
}

public class CommentCreateUpdateDto {
    public Guid? ParentId { get; set; }
    public double? Score { get; set; }
    public string? Comment { get; set; }
    public string? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? DailyPriceId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }
}

public class CommentReadDto : BaseReadDto {
    public double? Score { get; set; } = 0;
    public string? Comment { get; set; }
    public Guid? ParentId { get; set; }
    public string? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? DailyPriceId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }
}