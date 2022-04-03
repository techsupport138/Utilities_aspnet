using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class VoteFieldEntity : BaseEntity {

    public string Title { get; set; } = null!;
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public PostEntity? Post { get; set; }
}