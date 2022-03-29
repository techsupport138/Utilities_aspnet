using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Entities;
using Utilities_aspnet.Language.Entities;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Language.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Entities;

public class BaseContentEntity: BaseEntity
{
    public LanguageEntity Language { get; set; } = new LanguageEntity()
    {
        Symbol = "fa-IR",
        LanguageName = "فارسی"
    };


    [StringLength(500)]
    [Column(TypeName = "NVARCHAR")]
    [Required]
    public string Title { get; set; }
    
    public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;
}
