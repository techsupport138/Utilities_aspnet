using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.Base.Dtos
{
    public class CategoryFilter
    {
        public string? LanguageId { get; set; } = "fa-IR";
        public IdTitleUseCase CategoryFor { get; set; }
        public Guid? CategoryId { get; set; }
        public bool OnlyParent { get; set; } = false;
    }
}
