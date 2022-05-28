namespace Utilities_aspnet.Utilities.Dtos;

public class KVPCategoryVM {
    public KVPCategoryVM() {
        Childs = new List<KVPCategoryVM>();
    }

    public Guid Key { get; set; }

    public string Value { get; set; }

    public string? Image { get; set; }
    public List<KVPCategoryVM> Childs { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentTitle { get; set; }
    public string LanguageId { get; set; } = "fa-IR";
    public IdTitleUseCase CategoryFor { get; set; }
}