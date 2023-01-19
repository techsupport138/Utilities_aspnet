namespace Utilities_aspnet.Repositories;

public interface IUploadRepository {
	Task<GenericResponse<IEnumerable<MediaEntity>?>> Upload(UploadDto model);
	Task<GenericResponse> Delete(Guid id);
}

public class UploadRepository : IUploadRepository {
	private readonly DbContext _dbContext;
	private readonly IMediaRepository _mediaRepository;

	public UploadRepository(DbContext dbContext, IMediaRepository mediaRepository) {
		_mediaRepository = mediaRepository;
		_dbContext = dbContext;
	}

	public async Task<GenericResponse<IEnumerable<MediaEntity>?>> Upload(UploadDto model) {
		List<MediaEntity> medias = new();

		if (model.Files != null) {
			foreach (IFormFile file in model.Files) {
				string folder = "";
				if (model.UserId != null) {
					folder = "Users";
					List<MediaEntity> userMedia = _dbContext.Set<MediaEntity>().Where(x => x.UserId == model.UserId).ToList();
					if (userMedia.Any()) {
						_dbContext.Set<MediaEntity>().RemoveRange(userMedia);
						await _dbContext.SaveChangesAsync();
					}
				}

				string name = _mediaRepository.GetFileName(Guid.NewGuid(), Path.GetExtension(file.FileName));
				
				List<string> allowedExtensions = new() {".png", ".gif", ".jpg", ".jpeg", ".mp4", ".mp3", ".pdf"};
				if (!allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower()))) {
					return new GenericResponse<IEnumerable<MediaEntity>?>(null, UtilitiesStatusCodes.BadRequest);
				}
				
				MediaEntity media = new() {
					FileName = _mediaRepository.GetFileUrl(name, folder),
					UserId = model.UserId,
					ProductId = model.ProductId,
					ContentId = model.ContentId,
					CategoryId = model.CategoryId,
					ChatId = model.ChatId,
					CommentId = model.CommentId,
					CreatedAt = DateTime.Now,
					UseCase = model.UseCase,
					Title = model.Title,
					Size = model.Size,
					NotificationId = model.NotificationId,
					GroupChatId = model.GroupChatId,
					GroupChatMessageId = model.GroupChatMessageId
				};
				await _dbContext.Set<MediaEntity>().AddAsync(media);
				await _dbContext.SaveChangesAsync();
				medias.Add(media);

				_mediaRepository.SaveMedia(file, name, folder);
			}
		}
		if (model.Links != null) {
			foreach (MediaEntity media in model.Links.Select(link => new MediaEntity {
				         Link = link,
				         UserId = model.UserId,
				         ProductId = model.ProductId,
				         ContentId = model.ContentId,
				         CategoryId = model.CategoryId,
				         ChatId = model.ChatId,
				         CommentId = model.CommentId,
				         CreatedAt = DateTime.Now,
				         UseCase = model.UseCase,
				         Title = model.Title,
				         Size = model.Size,
				         NotificationId = model.NotificationId
			         })) {
				await _dbContext.Set<MediaEntity>().AddAsync(media);
				await _dbContext.SaveChangesAsync();
				medias.Add(media);
			}
		}

		return new GenericResponse<IEnumerable<MediaEntity>?>(medias);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		MediaEntity? media = await _dbContext.Set<MediaEntity>().FirstOrDefaultAsync(x => x.Id == id);
		if (media == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);

		_dbContext.Set<MediaEntity>().Remove(media);
		await _dbContext.SaveChangesAsync();

		return new GenericResponse();
	}
}