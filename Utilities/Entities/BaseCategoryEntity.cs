//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Utilities_aspnet.Base;

//namespace Utilities_aspnet.Utilities.Entities;

//public class BaseCategoryEntity : BaseEntity {
//    [StringLength(100)]
//    public string Title { get; set; } = null!;
//    public Guid? ParentId { get; set; }
//    [ForeignKey(nameof(ParentId))]
//    public BaseCategoryEntity? Parent { get; set; }
//    public ICollection<MediaEntity>? Media { get; set; }
//}