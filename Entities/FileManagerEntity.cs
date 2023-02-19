using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utilities_aspnet.Entities
{
    [Table("FileManager")]
    public class FileManagerEntity : BaseEntity
    {
        public Guid FkId { get; set; }
        public ReferenceIdType Procedure { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class RequestSaveFileDto
    {
        public Guid FkId { get; set; }
        public ReferenceIdType Procedure { get; set; }
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class ResponseSaveFileDto
    {
        public Guid FileId { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
