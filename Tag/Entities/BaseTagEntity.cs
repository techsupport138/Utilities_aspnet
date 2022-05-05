using Utilities_aspnet.Product;

namespace Utilities_aspnet.Tag.Entities;

public abstract class BaseTagEntity : BaseEntity {
    [StringLength(100)]
    public string? Title { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }
}