using Microsoft.Net.Http.Headers;

namespace Utilities_aspnet.Utilities.Data;

public interface IUploadRepository
{
    Task<GenericResponse> UploadMedia(UploadDto model);
    Task<GenericResponse> DeleteMedia(Guid id);
}

public class UploadRepository : IUploadRepository
{
    private readonly DbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMediaRepository _mediaRepository;

    public UploadRepository(DbContext context, IWebHostEnvironment env, IMediaRepository mediaRepository)
    {
        _env = env;
        _mediaRepository = mediaRepository;
        _context = context;
    }

    public async Task<GenericResponse> UploadMedia(UploadDto model)
    {
        if (model.Files.Count < 1) return new GenericResponse(UtilitiesStatusCodes.BadRequest, "File not uploaded");
        List<Guid>? ids = new();
        foreach (var file in model.Files)
        {
            var fileType = FileTypes.Image;
            if (file.ContentType.Contains("svg")) fileType = FileTypes.Svg;

            if (file.ContentType.Contains("video")) fileType = FileTypes.Video;

            if (file.ContentType.Contains("pdf")) fileType = FileTypes.Pdf;

            if (file.ContentType.Contains("Voice")) fileType = FileTypes.Voice;

            if (file.ContentType.Contains("Gif")) fileType = FileTypes.Gif;

            var folder = "";
            if (model.UserId != null)
            {
                folder = "Users";
                var userMedia =
                    _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
                if (userMedia.Count > 0)
                {
                    _context.Set<MediaEntity>().RemoveRange(userMedia);
                    await _context.SaveChangesAsync();
                }
            }

            var name = _mediaRepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
            var url = _mediaRepository.GetFileUrl(name, folder);
            MediaEntity media = new()
            {
                FileName = url,
                FileType = fileType,
                UserId = model.UserId,
                ProductId = model.ProductId,
                AdId = model.AdsId,
                CompanyId = model.CompanyId,
                EventId = model.EventId,
                MagazineId = model.MagazineId,
                ProjectId = model.ProjectId,
                ServiceId = model.ServiceId,
                TutorialId = model.TutorialId,
                TenderId = model.TenderId,
                CreatedAt = DateTime.Now,
            };
            await _context.Set<MediaEntity>().AddAsync(media);
            await _context.SaveChangesAsync();
            ids.Add(media.Id);
            _mediaRepository.SaveMedia(file, name, folder);
        }

        return new GenericResponse(UtilitiesStatusCodes.Success, "File uploaded", ids);
    }

    //public async Task<GenericResponse> UploadChunk(UploadDto parameter)
    //{
    //    if (parameter.Files.Count < 1) return new GenericResponse(UtilitiesStatusCodes.BadRequest, "File not uploaded");

    //    try
    //    {
    //        var fileType = FileTypes.Image;

    //        foreach (var file in parameter.Files)
    //        {
    //            var filename = ContentDispositionHeaderValue
    //                .Parse(file.ContentDisposition)
    //                .FileName
    //                .Trim()
    //                .Value;

    //            if (file.ContentType.Contains("svg")) fileType = FileTypes.Svg;

    //            if (file.ContentType.Contains("video")) fileType = FileTypes.Video;

    //            if (file.ContentType.Contains("pdf")) fileType = FileTypes.Pdf;

    //            if (file.ContentType.Contains("Voice")) fileType = FileTypes.Voice;

    //            if (file.ContentType.Contains("Gif")) fileType = FileTypes.Gif;


    //            filename = _env.WebRootPath + $@"\{filename}";
    //            if (!File.Exists(filename))
    //            {
    //                await using var fs = File.Create(filename);
    //                await file.CopyToAsync(fs);
    //                fs.Flush();
    //            }
    //            else
    //            {
    //                await using var fs = File.Open(filename, FileMode.Append);
    //                await file.CopyToAsync(fs);
    //                fs.Flush();
    //            }
    //        }

    //        var folder = "";
    //        if (parameter.UserId != null)
    //        {
    //            folder = "Users";
    //            var userMedia =
    //                _context.Set<MediaEntity>().Where(x => x.UserId == parameter.UserId).ToList();
    //            if (userMedia.Count > 0)
    //            {
    //                _context.Set<MediaEntity>().RemoveRange(userMedia);
    //                await _context.SaveChangesAsync();
    //            }
    //        }

    //        return new GenericResponse(UtilitiesStatusCodes.Success, "File uploaded", ids);
    //    }
    //    catch
    //    {
    //        return new GenericResponse(UtilitiesStatusCodes.BadRequest, "در آپلود فایل خطایی رخ داده است");
    //    }
    //}

    public async Task<GenericResponse> DeleteMedia(Guid id)
    {
        var media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (media == null) return new GenericResponse(UtilitiesStatusCodes.NotFound, "File not Found");

        _context.Set<MediaEntity>().Remove(media);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Success");
    }
}