namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]/[action]")]
public class FollowBookmarkController : BaseApiController {
	private readonly IFollowBookmarkRepository _repository;

	public FollowBookmarkController(IFollowBookmarkRepository repository) {
		_repository = repository;
	}

	[HttpPost("ReadFollowers")]
	public async Task<ActionResult<GenericResponse<FollowReadDto>>> ReadFollowers() {
		GenericResponse<FollowReadDto> result = await _repository.GetFollowers(User.Identity?.Name!);
		return Result(result);
	}

	[HttpPost("ReadFollowings")]
	public async Task<ActionResult<GenericResponse<FollowReadDto>>> ReadFollowings() {
		GenericResponse<FollowingReadDto> result = await _repository.GetFollowing(User.Identity?.Name!);
		return Result(result);
	}

	[HttpPost("ToggleFolllow")]
	public async Task<ActionResult<GenericResponse>> ToggleFollow(FollowWriteDto dto) {
		GenericResponse result = await _repository.ToggleFollow(User.Identity?.Name!, dto);
		return Result(result);
	}

	[HttpPost("RemoveFollowing")]
	public async Task<ActionResult<GenericResponse>> RemoveFollowing(FollowWriteDto dto) {
		GenericResponse result = await _repository.RemoveFollowings(User?.Identity?.Name!, dto);
		return Result(result);
	}

	[HttpPost("ToggleBookmark")]
	public async Task<IActionResult> ToggleBookmark(BookmarkCreateDto dto) {
		GenericResponse i = await _repository.ToggleBookmark(dto);
		return Result(i);
	}

	[HttpPost("ReadBookmarks")]
	public async Task<IActionResult> ReadBookmarks() {
		GenericResponse i = await _repository.ReadBookmarks();
		return Result(i);
	}
}