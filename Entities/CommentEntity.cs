namespace Utilities_aspnet.Entities;

[Table("Comment")]
public class CommentEntity : BaseEntity {
	public double? Score { get; set; } = 0;

	[StringLength(2000)]
	public string? Comment { get; set; }

	public ChatStatus? Status { get; set; }
	
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
	
	[NotMapped]
	public bool IsLiked { get; set; }
}

[Table("LikeComment")]
public class LikeCommentEntity : BaseEntity {
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
	public ChatStatus? Status { get; set; }
}

public class CommentFilterDto {
	public string? UserId { get; set; }
	public Guid? ProductId { get; set; }
	public Guid? CategoryId { get; set; }
	public bool? ShowProducts { get; set; }
	public ChatStatus? Status { get; set; }
    public bool ShowDeleted { get; set; }
}
