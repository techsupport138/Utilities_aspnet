using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Language.Entities;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Base;

public class BaseContentEntity : BaseEntity {
    public LanguageEntity Language { get; set; } = new LanguageEntity() {
        Symbol = "fa-IR",
        LanguageName = "فارسی"
    };


    [StringLength(500)]
    [Column(TypeName = "NVARCHAR")]
    [Required]
    public string Title { get; set; }

    public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;
}