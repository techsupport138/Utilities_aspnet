using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class PostCategoryEntity : BaseCategoryEntity {

    public ICollection<UserEntity>? Users { get; set; }
    public ICollection<PostEntity>? Posts { get; set; }
}