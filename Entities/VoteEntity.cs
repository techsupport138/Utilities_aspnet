namespace Utilities_aspnet.Entities;

[Table("Votes")]
public class VoteEntity : BaseEntity {
	public double? Score { get; set; } = 0;

	public UserEntity? User { get; set; }
	public string? UserId { get; set; }

	public VoteFieldEntity? VoteField { get; set; }
	public Guid? VoteFieldId { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }

	[NotMapped]
	public string? Title { get; set; }
}

[Table("VoteFields")]
public class VoteFieldEntity : BaseEntity {
	public string? Title { get; set; }

	public IEnumerable<VoteEntity>? Votes { get; set; }

	public ProductEntity? Product { get; set; }
	public Guid? ProductId { get; set; }
}

public class VoteFieldCreateUpdateDto {
	public List<VoteFieldDto>? VoteFields { get; set; }
	public Guid? ProductId { get; set; }
}

public class VoteFieldDto {
	public Guid? Id { get; set; }
	public string? Title { get; set; }
}

public class VoteCreateUpdateDto {
	public Guid? ProductId { get; set; }
	public List<VoteDto>? Votes { get; set; }
}

public class VoteDto {
	public double? Score { get; set; }
	public Guid? VoteFieldId { get; set; }
}