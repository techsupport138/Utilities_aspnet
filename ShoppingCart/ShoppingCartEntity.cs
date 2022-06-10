using Utilities_aspnet.User;

namespace Utilities_aspnet.ShoppingCart;

[Table("ShoppingCart")]
public class ShoppingCartEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }

    public List<ShoppingCartItemEntity>? ShoppingCartItems { get; set; }    
}

[Table("ShoppingCartItem")]
public class ShoppingCartItemEntity : BaseEntity
{
    public int Quantity { get; set; }

    public Guid? ShoppingCartId { get; set; }
    public ShoppingCartEntity? ShoppingCart { get; set; }  

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

public class ShoppingCartReadDto : BaseReadDto
{
    public ShoppingCartReadDto()
    {
        ShoppingCartItems = new List<ShoppingCartItemReadDto>();
    }

    public Guid? UserId { get; set; }
    public List<ShoppingCartItemReadDto> ShoppingCartItems { get; set; }
}

public class ShoppingCartItemReadDto : BaseReadDto
{
    public int Quantity { get; set; }

    public Guid? ShoppingCartId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? DailyPriceId { get; set; }

    public Guid? TutorialId { get; set; }

    public Guid? EventId { get; set; }

    public Guid? AdId { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? TenderId { get; set; }

    public Guid? ServiceId { get; set; }

    public Guid? MagazineId { get; set; }
}

public class ShoppingCartCreateDto
{
    public ShoppingCartCreateDto()
    {
        ShoppingCartItems = new List<ShoppingCartItemCreateDto>();
    }

    public Guid? UserId { get; set; }
    public List<ShoppingCartItemCreateDto> ShoppingCartItems { get; set; }
}

public class ShoppingCartItemCreateDto
{
    public int Quantity { get; set; }

    public Guid? ShoppingCartId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? DailyPriceId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }
}

public class ShoppingCartUpdateDto
{
    public Guid? ShoppingCartId { get; set; }
    public Guid? ShoppingCartItemId { get; set; }
    public int Quantity { get; set; }
}