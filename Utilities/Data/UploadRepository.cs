using Microsoft.Net.Http.Headers;

namespace Utilities_aspnet.Utilities.Data;

public interface IUploadRepository
{
    Task<GenericResponse<List<MediaDto>?>> UploadMedia(UploadDto model);
    Task<GenericResponse> DeleteMedia(Guid id);
    Task<GenericResponse> UploadChunkMedia(UploadDto parameter);
}

public class UploadRepository : IUploadRepository
{
    private readonly DbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly IMediaRepository _mediaRepository;

    public UploadRepository(
        DbContext context,
        IWebHostEnvironment env,
        IMediaRepository mediaRepository,
        IMapper mapper)
    {
        _env = env;
        _mediaRepository = mediaRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<GenericResponse<List<MediaDto>?>> UploadMedia(UploadDto model)
    {
        List<MediaEntity> medias = new();
        foreach (IFormFile file in model.Files)
        {
            FileTypes fileType = FileTypes.Image;

            if (file.ContentType.ToLower().Contains("svg")) fileType = FileTypes.Svg;
            if (file.ContentType.ToLower().Contains("video")) fileType = FileTypes.Video;
            if (file.ContentType.ToLower().Contains("pdf")) fileType = FileTypes.Pdf;
            if (file.ContentType.ToLower().Contains("Voice")) fileType = FileTypes.Voice;
            if (file.ContentType.ToLower().Contains("Gif")) fileType = FileTypes.Gif;

            string folder = "";
            if (model.UserId != null)
            {
                folder = "Users";
                List<MediaEntity> userMedia =
                    _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
                if (userMedia.Count > 0)
                {
                    _context.Set<MediaEntity>().RemoveRange(userMedia);
                    await _context.SaveChangesAsync();
                }
            }

            string name = _mediaRepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
            MediaEntity media = new()
            {
                FileName = _mediaRepository.GetFileUrl(name, folder),
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
                ContentId = model.ContentId,
                CreatedAt = DateTime.Now,
                UseCase = model.UseCase,
                Visibility = model.Visibility,
                Title = model.Title
            };
            await _context.Set<MediaEntity>().AddAsync(media);
            await _context.SaveChangesAsync();
            medias.Add(media);
            _mediaRepository.SaveMedia(file, name, folder);
        }

        foreach (MediaEntity media in model.Links.Select(link => new MediaEntity
        {
            FileType = FileTypes.Link,
            Link = link,
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
            UseCase = model.UseCase,
            Visibility = model.Visibility,
            Title = model.Title
        }))
        {
            await _context.Set<MediaEntity>().AddAsync(media);
            await _context.SaveChangesAsync();
            medias.Add(media);
        }

        return new GenericResponse<List<MediaDto>?>(
            _mapper.Map<List<MediaDto>>(medias),
            UtilitiesStatusCodes.Success,
            "File uploaded"
        );
    }

    public async Task<GenericResponse> UploadChunkMedia(UploadDto parameter)
    {
        if (parameter.Files.Count < 1) return new GenericResponse(UtilitiesStatusCodes.BadRequest, "File not uploaded");

        try
        {
            List<Guid> ids = new();
            FileTypes fileType = FileTypes.Image;

            string? filename = ContentDispositionHeaderValue
                .Parse(parameter.Files.First().ContentDisposition)
                .FileName
                .Trim()
                .Value;
            string extension = Path.GetExtension(filename);

            Guid signature = Guid.NewGuid();

            foreach (IFormFile file in parameter.Files)
            {
                if (file.ContentType.Contains("svg")) fileType = FileTypes.Svg;
                if (file.ContentType.Contains("video")) fileType = FileTypes.Video;
                if (file.ContentType.Contains("pdf")) fileType = FileTypes.Pdf;
                if (file.ContentType.Contains("Voice")) fileType = FileTypes.Voice;
                if (file.ContentType.Contains("Gif")) fileType = FileTypes.Gif;

                filename = _env.WebRootPath + $@"\{signature}{extension}";
                if (!File.Exists(filename))
                {
                    await using FileStream fs = File.Create(filename);
                    await file.CopyToAsync(fs);
                    fs.Flush();
                }
                else
                {
                    await using FileStream fs = File.Open(filename, FileMode.Append);
                    await file.CopyToAsync(fs);
                    fs.Flush();
                }
            }

            string folder = "";
            if (parameter.UserId != null)
            {
                folder = "Users";
                List<MediaEntity> userMedia =
                    _context.Set<MediaEntity>().Where(x => x.UserId == parameter.UserId).ToList();
                if (userMedia.Count > 0)
                {
                    _context.Set<MediaEntity>().RemoveRange(userMedia);
                    await _context.SaveChangesAsync();
                }
            }

            string url = _mediaRepository.GetFileUrl(filename, folder);
            MediaEntity media = new()
            {
                FileName = url,
                FileType = fileType,
                UserId = parameter.UserId,
                ProductId = parameter.ProductId,
                AdId = parameter.AdsId,
                CompanyId = parameter.CompanyId,
                EventId = parameter.EventId,
                MagazineId = parameter.MagazineId,
                ProjectId = parameter.ProjectId,
                ServiceId = parameter.ServiceId,
                TutorialId = parameter.TutorialId,
                TenderId = parameter.TenderId,
                CreatedAt = DateTime.Now
            };
            await _context.Set<MediaEntity>().AddAsync(media);
            await _context.SaveChangesAsync();
            ids.Add(media.Id);
            return new GenericResponse(UtilitiesStatusCodes.Success, "File uploaded", ids);
        }
        catch
        {
            return new GenericResponse(UtilitiesStatusCodes.BadRequest, "Fail to upload");
        }
    }

    public async Task<GenericResponse> DeleteMedia(Guid id)
    {
        MediaEntity? media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (media == null) return new GenericResponse(UtilitiesStatusCodes.NotFound, "File not Found");

        _context.Set<MediaEntity>().Remove(media);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Success");
    }
}