using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class VoteEntity : BaseEntity {

    public double Score { get; set; } = 0;
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public PostEntity? Post { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }
    public Guid VoteFieldId { get; set; }
    [ForeignKey(nameof(VoteFieldId))]
    public VoteFieldEntity? VotingField { get; set; }
}