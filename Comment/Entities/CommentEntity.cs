using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Comment.Entities;

public class CommentEntity : BaseEntity {
    public double Point { get; set; } = 0;

    public UserEntity UserEntity { get; set; } = null!;
    public string UserId { get; set; } = null!;
}