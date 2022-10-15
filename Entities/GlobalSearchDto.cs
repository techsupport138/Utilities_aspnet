namespace Utilities_aspnet.Entities;

public class GlobalSearchDto {
	public IEnumerable<UserReadDto>? Users { get; set; }
	public IEnumerable<ProductReadDto>? Products { get; set; }
	public IEnumerable<CategoryEntity>? Categories { get; set; }
}

public class GlobalSearchParams {
	public string Title { get; set; } = "";
	public string Query { get; set; } = "";
	public IEnumerable<Guid>? Categories { get; set; }
	public int PageSize { get; set; } = 1000;
	public int PageNumber { get; set; } = 1;
	public bool Newest { get; set; } = false;
	public bool Oldest { get; set; } = false;
	public bool Reputation { get; set; } = false;
	public bool User { get; set; } = true;
	public bool Product { get; set; } = true;
	public bool Category { get; set; } = true;
	public bool IsBookmark { get; set; } = false;
	public bool IsFollowing { get; set; } = false;
	public bool IsMine { get; set; } = false;
	public bool Minimal { get; set; } = false;
}