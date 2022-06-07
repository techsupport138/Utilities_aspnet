namespace Utilities_aspnet.ShoppingCart;

[Table("ShoppingCart")]
public class ShoppingCartEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }

    public Guid? ProductId { get; set; }
    public ProductEntity? Product { get; set; }

    public Guid? ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }

    public Guid? DailyPriceId { get; set; }
    public DailyPriceEntity? DailyPrice { get; set; }

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