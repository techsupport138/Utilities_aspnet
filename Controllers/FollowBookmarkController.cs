namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class FollowBookmarkController : BaseApiController {
	private readonly IFollowBookmarkRepository _repository;

	public FollowBookmarkController(IFollowBookmarkRepository repository) => _repository = repository;

	[HttpPost("ReadFollowers")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> ReadFollowers() {
		GenericResponse<IEnumerable<UserReadDto>> result = await _repository.GetFollowers(User.Identity?.Name!);
		return Result(result);
	}

	[HttpPost("ReadFollowings")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> ReadFollowings() {
		GenericResponse<IEnumerable<UserReadDto>> result = await _repository.GetFollowing(User.Identity?.Name!);
		return Result(result);
	}

	[HttpPost("ToggleFolllow")]
	public async Task<ActionResult<GenericResponse>> ToggleFollow(FollowCreateDto dto) {
		GenericResponse result = await _repository.ToggleFollow(User.Identity?.Name!, dto);
		return Result(result);
	}

	//[HttpPost("BookmarkFolder")]
	//public async Task<ActionResult<GenericResponse>> CreateUpdateBookmarkFolder(BookmarkFolderCreateUpdateDto dto) {
	//	GenericResponse result = await _repository.CreateUpdateBookmarkFolder(dto);
	//	return Result(result);
	//}

	//[HttpGet("BookmarkFolder")]
	//public async Task<ActionResult<GenericResponse<IEnumerable<BookmarkFolderReadDto>?>>> ReadBookmarkFolders() {
	//	GenericResponse<IEnumerable<BookmarkFolderReadDto>?> result = await _repository.ReadBookmarkFolders();
	//	return Result(result);
	//}

	//[HttpDelete("BookmarkFolder/{id:guid}")]
	//public async Task<ActionResult<GenericResponse>> DeleteBookmarkFolders(Guid id) {
	//	GenericResponse result = await _repository.DeleteBookmarkFolder(id);
	//	return Result(result);
	//}

	[HttpPost("RemoveFollowing")]
	public async Task<ActionResult<GenericResponse>> RemoveFollowing(FollowCreateDto dto) {
		GenericResponse result = await _repository.RemoveFollowings(User?.Identity?.Name!, dto);
		return Result(result);
	}

	[HttpPost("ToggleBookmark")]
	public async Task<IActionResult> ToggleBookmark(BookmarkCreateDto dto) {
		GenericResponse i = await _repository.ToggleBookmark(dto);
		return Result(i);
	}

	[HttpPost("ReadBookmarks")]
	public async Task<ActionResult<GenericResponse<IEnumerable<BookmarkReadDto>?>>> ReadBookmarks() {
		GenericResponse<IEnumerable<BookmarkReadDto>?> i = await _repository.ReadBookmarks();
		return Result(i);
	}
}