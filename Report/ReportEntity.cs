namespace Utilities_aspnet.Report;

public class ReportEntity : BaseEntity {
    public string? Title { get; set; }
    public string? Description { get; set; }

    public string? UserId { get; set; }
    public UserEntity? User { get; set; }

    public Guid? ProductId { get; set; }
    public ProductEntity? Product { get; set; }

    public Guid? ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }

    public Guid? TutorialId { get; set; }
    public TutorialEntity? Tutorial { get; set; }

    public Guid? EventId { get; set; }
    public EventEntity? Event { get; set; }

    public Guid? AdId { get; set; }
    public AdEntity? Ad { get; set; }

    public Guid? CompanyId { get; set; }
    public CompanyEntity? Company { get; set; }

    public Guid? TenderId { get; set; }
    public TenderEntity? Tender { get; set; }

    public Guid? ServiceId { get; set; }
    public ServiceEntity? Service { get; set; }

    public Guid? MagazineId { get; set; }
    public MagazineEntity? Magazine { get; set; }
}

public class ReportCreateDto : BaseReadDto {
    public string? Title { get; set; }
    public string? Description { get; set; }

    public Guid? ProductId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }
}