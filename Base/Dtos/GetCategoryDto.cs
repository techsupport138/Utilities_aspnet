using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.Base.Dtos;

public class GetCategoryDto
{
    public Guid? CategoryId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? MediaId { get; set; }
    public string LanguageId { get; set; } = "fa-IR";
    public IdTitleUseCase CategoryFor { get; set; }
    public string Title { get; set; }
}