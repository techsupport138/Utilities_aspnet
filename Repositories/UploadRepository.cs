namespace Utilities_aspnet.Repositories;

public interface IUploadRepository
{
    Task<GenericResponse<IEnumerable<MediaDto>?>> Upload(UploadDto model);
    Task<GenericResponse> Delete(Guid id);
}

public class UploadRepository : IUploadRepository
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediaRepository _mediaRepository;

    public UploadRepository(DbContext context, IMediaRepository mediaRepository, IMapper mapper)
    {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<GenericResponse<IEnumerable<MediaDto>?>> Upload(UploadDto model)
    {
        List<MediaEntity> medias = new();
        if (model.Files != null)
            foreach (IFormFile file in model.Files)
            {
                string folder = "";
                if (model.UserId != null)
                {
                    folder = "Users";
                    List<MediaEntity> userMedia = _context.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
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
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    ContentId = model.ContentId,
                    CategoryId = model.CategoryId,
                    ChatId = model.ChatId,
                    CommentId = model.CommentId,
                    CreatedAt = DateTime.Now,
                    UseCase = model.UseCase,
                    Visibility = model.Visibility,
                    Title = model.Title,
                    Size = model.Size,
                    NotificationId = model.NotificationId
                };
                await _context.Set<MediaEntity>().AddAsync(media);
                await _context.SaveChangesAsync();
                medias.Add(media);
                _mediaRepository.SaveMedia(file, name, folder);
            }

        if (model.Links != null)
            foreach (MediaEntity media in model.Links.Select(link => new MediaEntity
            {
                Link = link,
                UserId = model.UserId,
                ProductId = model.ProductId,
                ContentId = model.ContentId,
                CategoryId = model.CategoryId,
                ChatId = model.ChatId,
                CommentId = model.CommentId,
                CreatedAt = DateTime.Now,
                UseCase = model.UseCase,
                Visibility = model.Visibility,
                Title = model.Title,
                Size = model.Size,
                NotificationId = model.NotificationId
            }))
            {
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

    public async Task<GenericResponse> Delete(Guid id)
    {
        MediaEntity? media = await _context.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (media == null) return new GenericResponse(UtilitiesStatusCodes.NotFound, "File not Found");

        _context.Set<MediaEntity>().Remove(media);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Success");
    }
}