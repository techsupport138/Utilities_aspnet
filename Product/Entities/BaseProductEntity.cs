using Utilities_aspnet.Tag.Entities;

namespace Utilities_aspnet.Product.Entities;

public abstract class BasePEntity : BaseEntity {
    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? SubTitle { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [DefaultValue(0)]
    public decimal Price { get; set; } = 0;

    [DefaultValue(false)]
    public bool IsForSale { get; set; } = false;

    [DefaultValue(false)]
    public bool Enabled { get; set; } = false;

    public UserEntity? UserEntity { get; set; }
    public string? UserId { get; set; }

    public LocationEntity? Location { get; set; }
    public Guid? LocationId { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryEntity>? Category { get; set; }
    public IEnumerable<SpecialtyEntity>? Specialty { get; set; }
    public IEnumerable<TagEntity>? Tag { get; set; }
    public IEnumerable<UserEntity> Team { get; set; }
    public IEnumerable<VoteFieldEntity> VoteFields { get; set; }
}