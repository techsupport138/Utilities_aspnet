using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Utilities.Data
{
    public interface IUploadRepository
    {
        Task<GenericResponse> UploadMedia(UploadDto model);
        Task<GenericResponse> DeleteMedia(Guid id);
    }

    public class UploadRepository : IUploadRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMediaRepository _mediaRepository;
        private readonly DbContext _context;

        public UploadRepository(DbContext context, IWebHostEnvironment env, IMediaRepository mediaRepository)
        {
            _env = env;
            _mediaRepository = mediaRepository;
            _context = context;
        }

        public async Task<GenericResponse> UploadMedia(UploadDto model)
        {
            if (model.Files.Count < 1)
            {
                return new GenericResponse(UtilitiesStatusCodes.BadRequest, "File not uploaded");
            }
            List<Guid>? ids = new List<Guid>();
            foreach (IFormFile file in model.Files)
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

                string folder = "";
                if (model.UserId != null)
                {
                    folder = "Users";
                    List<MediaEntity> userMedia =
                        _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
                    if (userMedia.Count > 0)
                    {
                        _context.Set<MediaEntity>().RemoveRange(userMedia);
                        _context.SaveChanges();
                    }
                }

                string name = _mediaRepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
                string url = _mediaRepository.GetFileUrl(name, folder: folder);
                MediaEntity media = new MediaEntity
                {
                    FileName = url,
                    FileType = fileType,
                    UserId = model.UserId,
                    ProductId = model.ProductId,

                };
                await _context.Set<MediaEntity>().AddAsync(media);
                await _context.SaveChangesAsync();
                ids.Add(media.Id);
                _mediaRepository.SaveMedia(file, name, folder);
            }

            return new GenericResponse(UtilitiesStatusCodes.Success, "File uploaded", ids);
        }

        public async Task<GenericResponse> DeleteMedia(Guid id)
        {
            MediaEntity? media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
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