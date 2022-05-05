using Utilities_aspnet.Tag.Entities;

namespace Utilities_aspnet.Product.Entities;

public abstract class BasePEntity : BaseEntity {
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public int? VisitCount { get; set; }
    
    public UserEntity? UserEntity { get; set; }
    public string? UserId { get; set; }

    public LocationEntity? Location { get; set; }
    public int? LocationId { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryEntity>? Category { get; set; }
    public IEnumerable<SpecialtyEntity>? Specialty { get; set; }
    public IEnumerable<TagEntity>? Tag { get; set; }

    // public IEnumerable<UserEntity> Team { get; set; }
    // public IEnumerable<MentionInProductEntity> MP { get; set; }
    public IEnumerable<VoteFieldEntity> VoteFields { get; set; }
}