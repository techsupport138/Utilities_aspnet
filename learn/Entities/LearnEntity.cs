namespace Utilities_aspnet.learn.Entities;

public class LearnEntity : BaseContentEntity {
    public string? Publisher { get; set; }
    public string? Confirmations { get; set; }
    public string? Honors { get; set; }
    public int? Amount { get; set; }
}