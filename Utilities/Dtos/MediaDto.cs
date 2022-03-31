using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Dtos {
    public class MediaDto {
        public string Id { get; set; }
        public FileTypes Type { get; set; }
        public string UseCase { get; set; } = null!;
        public string? Link { get; set; }
    }
}