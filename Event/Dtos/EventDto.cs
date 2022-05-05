using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Event.Dtos
{
    public class EventDto
    {
        public EventDto()
        {
            files = new List<IFormFile>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EventDate { get; set; }
        public string EventLocation { get; set; }
        public string LanguageId { get; set; } = "fa-IR";
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string TinyURL { get; set; }
        public string? Lid { get; set; }
        public string? Body { get; set; }
        public string? UserId { get; set; }
        public int? NumberOfLikes { get; set; } = 0;
        public string? Author { get; set; }
        public ContentUseCase UseCase { get; set; }
        public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;

        //public int CountryId { get; set; }
        //public int ProvinceId { get; set; }
        //public int CityId { get; set; }
        public int LocationId { get; set; }

        public List<IFormFile> files { get; set; }
    }
}
