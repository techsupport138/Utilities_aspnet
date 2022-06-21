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
}

public class CommentCreateUpdateDto {
	public Guid? ParentId { get; set; }
	public double? Score { get; set; }
	public string? Comment { get; set; }
	public string? UserId { get; set; }
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
}