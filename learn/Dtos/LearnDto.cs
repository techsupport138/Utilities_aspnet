using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Learn.Dtos
{
    public class LearnDto
    {
        public LearnDto()
        {
            files = new List<IFormFile>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Enable { get; set; }
        public string LanguageId { get; set; } = "fa-IR";
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string TinyURL { get; set; }
        //public string? Lid { get; set; } = null;
        //public string? Description { get; set; } = null;
        public string? UserId { get; set; } = null;
        public string Publisher { get; set; }


        public string Confirmations { get; set; }
        public string Honors { get; set; }
        public int? Amount { get; set; } = null;

        public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;


        public List<IFormFile> files { get; set; }
    }
}
