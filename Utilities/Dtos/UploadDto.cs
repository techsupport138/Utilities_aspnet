using Microsoft.AspNetCore.Http;

namespace Utilities_aspnet.Utilities.Dtos
{
    public class UploadDto
    {
        public string? UserId { get; set; }
        public List<IFormFile> Files { get; set; }
        public Guid? AdsId { get; set; } = null;
        public Guid? JobId { get; set; } = null;
        public Guid? LearnId { get; set; } = null;
        public Guid? PostId { get; set; } = null;
        public Guid? ProductId { get; set; } = null;
        public Guid? TenderId { get; set; } = null;
    }
}