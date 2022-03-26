using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.Models.Dto;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Data {
    public interface IUploadRepository {
        Task<GenericResponse> UploadMedia(UploadDto model);
        Task<GenericResponse> DeleteMedia(string id);
    }

    public class UploadRepository : IUploadRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMediaRepository _mediarepository;
        private readonly DbContext _context;

        public UploadRepository(DbContext context, IWebHostEnvironment env, IMediaRepository mediaRepository) {
            _env = env;
            _mediarepository = mediaRepository;
            _context = context;
        }

        public async Task<GenericResponse> UploadMedia(UploadDto model)
        {
            if (model.Files.Count < 1)
            {
                return new GenericResponse(UtilitiesStatusCodes.BadRequest, "File not uploaded");
            }
            foreach (var file in model.Files)
            {
                FileTypes fileType = FileTypes.Image;
                if (file.ContentType.Contains("svg"))
                {
                    fileType = FileTypes.Svg;
                }

                if (file.ContentType.Contains("video"))
                {
                    fileType = FileTypes.Video;
                }

                if (file.ContentType.Contains("pdf"))
                {
                    fileType = FileTypes.Pdf;
                }
                if (file.ContentType.Contains("Voice"))
                {
                    fileType = FileTypes.Voice;
                }
                if (file.ContentType.Contains("Gif"))
                {
                    fileType = FileTypes.Gif;
                }

                var folder = "";
                if (model.UserId != null)
                {
                    folder = "Users";
                    var userMedia = _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
                    if (userMedia.Count > 0)
                    {
                        _context.Set<MediaEntity>().RemoveRange(userMedia);
                        _context.SaveChanges();
                    }
                }

                var name = _mediarepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
                var url = _mediarepository.GetFileUrl(name, folder: folder);
                var media = new MediaEntity
                {
                    FileName = url,
                    FileType = fileType,
                    UserId = model.UserId
                };
                await _context.Set<MediaEntity>().AddAsync(media);
                await _context.SaveChangesAsync();
                _mediarepository.SaveMedia(file, name, folder);
            }

            return new GenericResponse(UtilitiesStatusCodes.Success, "File uploaded");
        }

        public async Task<GenericResponse> DeleteMedia(string id)
        {
            var media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (media == null)
            {
                return new GenericResponse(UtilitiesStatusCodes.NotFound, "File not Found");
            }
            _context.Set<MediaEntity>().Remove(media);
            await _context.SaveChangesAsync();

            return new GenericResponse(UtilitiesStatusCodes.Success, "Success");
        }

    }
}