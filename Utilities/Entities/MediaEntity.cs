using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities
{
    public class MediaEntity : BaseEntity
    {
        [Required]
        public string FileName { get; set; } = null!;
        [Required]
        public FileTypes FileType { get; set; }
        public string UseCase { get; set; } = "--";
    }
}
