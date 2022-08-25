namespace Utilities_aspnet.Entities;

[Table("Follows")]
public class FollowEntity : BaseEntity {
	public string? FollowerUserId { get; set; }
	public UserEntity? FollowerUser { get; set; }

	public string? FollowsUserId { get; set; }
	public UserEntity? FollowsUser { get; set; }
}

[Table("Bookmarks")]
public class BookmarkEntity : BaseEntity {
	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	[StringLength(500)]
	public string? FolderName { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }

	public CategoryEntity? Category { get; set; }
	public Guid? CategoryId { get; set; }
}

public class BookmarkCreateDto {
	public string? FolderName { get; set; }
	public Guid? ProductId { get; set; }
	public Guid? CategoryId { get; set; }
}

public class BookmarkReadDto {
	public string? FolderName { get; set; }
	public ProductReadDto? Product { get; set; }
	public ProductReadDto? Category { get; set; }
}

public class FollowCreateDto {
	public string UserId { get; set; } = null!;
}