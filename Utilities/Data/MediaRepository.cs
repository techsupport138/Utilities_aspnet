using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Utilities_aspnet.Data {
    public interface IMediaRepository {
        bool SaveMedia(IFormFile image, string name);
        bool SaveMedia(IFormFile image, string name, string folder);
        string GetFileName(Guid guid, string ext = ".png");
        string GetFileName(Guid guid, string folder = "", string ext = ".png");
        string GetFileUrl(string name, string folder);
    }

    public class MediaRepository : IMediaRepository {
        private readonly IWebHostEnvironment _env;

        public MediaRepository(IWebHostEnvironment env) {
            _env = env;
        }

        public string GetFileName(Guid guid, string folder = "", string ext = ".png")
        {
            return folder + (guid.ToString("N")) + ext;
        }
        public string GetFileName(Guid guid, string ext = ".png") { return guid.ToString("N") + ext; }
        public string GetFileUrl(string name, string folder) { return Path.Combine(folder, name); }

        public bool SaveMedia(IFormFile image, string name, string folder = "") {
            try {
                var webRoot = _env.WebRootPath;
                var nullPath = Path.Combine(webRoot, "Medias", "null.png");
                string path = Path.Combine(webRoot, "Medias", folder, name);
                try {
                    try {
                        File.Delete(path);
                    }
                    catch (Exception) {
                        // ignored
                    }

                    using var stream = new FileStream(path, FileMode.Create);
                    image.CopyTo(stream);
                    return true;
                }
                catch {
                    File.Copy(nullPath, path);
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        public bool SaveMedia(IFormFile image, string name) => SaveMedia(image, name, "");
    }
}