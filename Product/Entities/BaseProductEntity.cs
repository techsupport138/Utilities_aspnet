using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class BasePEntity : BaseEntity {
    public BasePEntity() {
        Media = new List<MediaEntity>();
    }

    public bool Publish { get; set; } = false;
    public bool Enable { get; set; } = false;

    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? SubTitle { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(LanguageId))]
    public LanguageEntity LanguageEntity { get; set; }

    [DefaultValue("fa-IR")]
    public string LanguageId { get; set; } = "fa-IR";

    [ForeignKey(nameof(UserId))]
    public UserEntity? UserEntity { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity Category { get; set; }

    public Guid CategoryId { get; set; }

    public IEnumerable<MediaEntity>? Media { get; set; }

    public decimal Price { get; set; } = 0;
    public Guid? LocationId { get; set; }

    [ForeignKey(nameof(LocationId))]
    public LocationEntity? Location { get; set; }
}