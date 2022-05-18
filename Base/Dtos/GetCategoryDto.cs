namespace Utilities_aspnet.Base.Dtos;

public class GetCategoryDto {
    public Guid? CategoryId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? MediaId { get; set; }
    public string LanguageId { get; set; } = "fa-IR";
    public IdTitleUseCase CategoryFor { get; set; }
    public string Title { get; set; }
}

public class NewCategoryDto {
    public Guid? CategoryId { get; set; }
    public Guid? ParentId { get; set; }
    public string LanguageId { get; set; } = "fa-IR";
    public IdTitleUseCase CategoryFor { get; set; }
    public string Title { get; set; }
    public string? UserId { get; set; }
    public IFormFile? File { get; set; }
    public Guid? MediaId { get; set; }
    public string? FileName { get; set; }
}

public class CategoryFilter {
    public string? LanguageId { get; set; } = "fa-IR";
    public IdTitleUseCase CategoryFor { get; set; }
    public Guid? CategoryId { get; set; }
    public bool OnlyParent { get; set; } = false;
}