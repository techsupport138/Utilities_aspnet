using Utilities_aspnet.Category;
using Utilities_aspnet.User;

namespace Utilities_aspnet.FollowBookmark;

[Table("Follows")]
public class FollowEntity : BaseEntity {
    public string? SourceUserId { get; set; }
    public UserEntity? SourceUser { get; set; }

    public string? TargetUserId { get; set; }
    public UserEntity? TargetUser { get; set; }
}

[Table("Bookmarks")]
public class BookmarkEntity : BaseEntity {
    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public ProductEntity? Product { get; set; }
    public Guid? ProductId { get; set; }

    public CategoryEntity? Category { get; set; }
    public Guid? CategoryId { get; set; }
}

public class BookmarkCreateDto {
    public Guid? ProductId { get; set; }
    public Guid? CategoryId { get; set; }
}

public class BookmarkReadDto {
    public IEnumerable<UserReadDto>? Users { get; set; }
    public IEnumerable<ProductReadDto>? Products { get; set; }
    public IEnumerable<ProductReadDto>? Categories { get; set; }
}