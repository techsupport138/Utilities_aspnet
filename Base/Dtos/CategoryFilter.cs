using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Base.Dtos
{
    public class CategoryFilter
    {
        public string? Language { get; set; } = "fa-IR";
        public CategoryForEnum CategoryFor { get; set; }
    }
}
