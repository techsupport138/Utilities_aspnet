using Microsoft.AspNetCore.Http;

namespace Utilities_aspnet.Utilities.Dtos {
    public class UploadDto {
        public string? UserId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}