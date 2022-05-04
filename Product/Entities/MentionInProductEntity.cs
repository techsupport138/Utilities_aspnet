namespace Utilities_aspnet.Product.Entities; 

public class MentionInProductEntity {
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }

    public string UserId { get; set; }
    public UserEntity User { get; set; }
}