namespace Utilities_aspnet.Comment;

public class CommentDto
{
    public Guid? ParentId { get; set; }
    public double? Score { get; set; }
    public string? Comment { get; set; }
    public string? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? DailyPriceId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }
}