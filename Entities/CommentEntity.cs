namespace Utilities_aspnet.Entities;

[Table("Comment")]
public class CommentEntity : BaseEntity {
	public double? Score { get; set; } = 0;
	public string? Comment { get; set; }

	public Guid? ParentId { get; set; }
	public CommentEntity? Parent { get; set; }

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
	[InverseProperty("Parent")]
	public IEnumerable<CommentEntity>? Children { get; set; }
	public IEnumerable<MediaEntity>? Media { get; set; }
	public IEnumerable<LikeCommentEntity>? LikeComments { get; set; }
}

[Table("LikeComment")]
public class LikeCommentEntity : BaseEntity
{
	public double? Score { get; set; } = 0;
	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public CommentEntity? Comment { get; set; }
	public Guid? CommentId { get; set; }
}

public class CommentCreateUpdateDto {
	public Guid? ParentId { get; set; }
	public double? Score { get; set; }
	public string? Comment { get; set; }
	public Guid? ProductId { get; set; }
}

public class CommentReadDto {
	public Guid Id { get; set; }
	public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
	public double? Score { get; set; } = 0;
	public string? Comment { get; set; }
	public Guid? ParentId { get; set; }
	public string? UserId { get; set; }
    public UserMinimalReadDto? User { get; set; }
	public IEnumerable<CommentReadDto>? Children { get; set; }
	public IEnumerable<MediaDto>? Media { get; set; }
}