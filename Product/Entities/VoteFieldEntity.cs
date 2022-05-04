namespace Utilities_aspnet.Product.Entities;

[Table("VoteFields")]
public class VoteFieldEntity : BaseEntity {
    public string? Title { get; set; }
    
    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public IEnumerable<VoteEntity> Vote { get; set; }
}