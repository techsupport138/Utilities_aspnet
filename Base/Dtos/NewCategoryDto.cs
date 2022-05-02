using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Base.Dtos
{
    public class NewCategoryDto
    {
        public Guid? CategoryId { get; set; }
        public Guid? ParentId { get; set; }
        public string LanguageId { get; set; } = "fa-IR";
        public CategoryForEnum CategoryFor { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public IFormFile? File { get; set; }
        public Guid? MediaId { get; set; }
        public string? FileName { get; set; }
    }
}
