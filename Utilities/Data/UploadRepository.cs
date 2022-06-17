namespace Utilities_aspnet.Utilities.Data;

public interface IUploadRepository {
    Task<GenericResponse<IEnumerable<MediaDto>?>> Upload(UploadDto model);
    Task<GenericResponse> Delete(Guid id);
}

public class UploadRepository : IUploadRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediaRepository _mediaRepository;

    public UploadRepository(
        DbContext context,
        IMediaRepository mediaRepository,
        IMapper mapper) {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<GenericResponse<IEnumerable<MediaDto>?>> Upload(UploadDto model) {
        List<MediaEntity> medias = new();
        if (model.Files != null)
            foreach (IFormFile file in model.Files) {
                FileTypes fileType = FileTypes.Image;

                if (file.ContentType.ToLower().Contains("svg")) fileType = FileTypes.Svg;
                if (file.ContentType.ToLower().Contains("video")) fileType = FileTypes.Video;
                if (file.ContentType.ToLower().Contains("pdf")) fileType = FileTypes.Pdf;
                if (file.ContentType.ToLower().Contains("Voice")) fileType = FileTypes.Voice;
                if (file.ContentType.ToLower().Contains("Gif")) fileType = FileTypes.Gif;

                string folder = "";
                if (model.UserId != null) {
                    folder = "Users";
                    List<MediaEntity> userMedia = _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
                    if (userMedia.Count > 0) {
                        _context.Set<MediaEntity>().RemoveRange(userMedia);
                        await _context.SaveChangesAsync();
                    }
                }

                string name = _mediaRepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
                MediaEntity media = new() {
                    FileName = _mediaRepository.GetFileUrl(name, folder),
                    FileType = fileType,
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    ContentId = model.ContentId,
                    CategoryId = model.CategoryId,
                    CreatedAt = DateTime.Now,
                    UseCase = model.UseCase,
                    Visibility = model.Visibility,
                    Title = model.Title,
                    NotificationId = model.NotificationId
                };
                await _context.Set<MediaEntity>().AddAsync(media);
                await _context.SaveChangesAsync();
                medias.Add(media);
                _mediaRepository.SaveMedia(file, name, folder);
            }

        if (model.Links != null)
            foreach (MediaEntity media in model.Links.Select(link => new MediaEntity {
                         FileType = FileTypes.Link,
                         Link = link,
                         UserId = model.UserId,
                         ProductId = model.ProductId,
                         ContentId = model.ContentId,
                         CategoryId = model.CategoryId,
                         CreatedAt = DateTime.Now,
                         UseCase = model.UseCase,
                         Visibility = model.Visibility,
                         Title = model.Title,
                         NotificationId = model.NotificationId
                     })) {
                await _context.Set<MediaEntity>().AddAsync(media);
                await _context.SaveChangesAsync();
                medias.Add(media);
            }

        return new GenericResponse<IEnumerable<MediaDto>?>(
            _mapper.Map<IEnumerable<MediaDto>>(medias),
            UtilitiesStatusCodes.Success,
            "File uploaded"
        );
    }

    public async Task<GenericResponse> Delete(Guid id) {
        MediaEntity? media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (media == null) return new GenericResponse(UtilitiesStatusCodes.NotFound, "File not Found");

        _context.Set<MediaEntity>().Remove(media);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Success");
    }
}