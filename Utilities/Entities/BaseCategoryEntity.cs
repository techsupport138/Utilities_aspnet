using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.Utilities.Entities;

public class BaseCategoryEntity : BaseEntity {
    [StringLength(100)]
    public string Title { get; set; } = null!;
    public BaseCategoryEntity? ParentId { get; set; }
    public ICollection<MediaEntity>? Media { get; set; }
}