namespace Utilities_aspnet.Product.Entities;

[Table("Votes")]
public class VoteEntity : BaseEntity {

    public double? Score { get; set; } = 0;

    public UserEntity? User { get; set; }
    public string? UserId { get; set; }
    
    public VoteFieldEntity? VotingField { get; set; }
    public Guid? VoteFieldId { get; set; }
    
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
}

[Table("VoteFields")]
public class VoteFieldEntity : BaseEntity {
    public string? Title { get; set; }

    public IEnumerable<VoteEntity>? Vote { get; set; }
    
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
}

public class VoteFieldCreateDto {
    public string? Title { get; set; }
}

public class VoteReadDto {
    public string? Title { get; set; }
    public double? Point { get; set; }
}