using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class VoteFieldEntity : BaseEntity {

    public string Title { get; set; } 
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public ProductEntity? Post { get; set; }
}