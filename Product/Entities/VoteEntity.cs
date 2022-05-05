namespace Utilities_aspnet.Product.Entities;

[Table("Votes")]
public class VoteEntity : BaseEntity {

    public double Score { get; set; } = 0;
    
    public ProductEntity? Post { get; set; }
    public Guid PostId { get; set; }
    
    public UserEntity? User { get; set; }
    public Guid UserId { get; set; }
    
    public VoteFieldEntity? VotingField { get; set; }
    public Guid VoteFieldId { get; set; }
}

[Table("VoteFields")]
public class VoteFieldEntity : BaseEntity {
    public string? Title { get; set; }
    
    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public IEnumerable<VoteEntity> Vote { get; set; }
}