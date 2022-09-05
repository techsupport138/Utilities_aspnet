namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowBookmarkController : BaseApiController {
	private readonly IFollowBookmarkRepository _repository;

	public FollowBookmarkController(IFollowBookmarkRepository repository) => _repository = repository;

	[HttpPost("ReadFollowers/{userId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> ReadFollowers(string userId) {
		GenericResponse<IEnumerable<UserReadDto>> result = await _repository.GetFollowers(userId);
		return Result(result);
	}

	[HttpPost("ReadFollowings/{userId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> ReadFollowings(string userId) {
		GenericResponse<IEnumerable<UserReadDto>> result = await _repository.GetFollowing(userId);
		return Result(result);
	}

	[HttpPost("ToggleFolllow")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> ToggleFollow(FollowCreateDto dto) {
		GenericResponse result = await _repository.ToggleFollow(User.Identity?.Name!, dto);
		return Result(result);
	}

	[HttpPost("RemoveFollowing")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> RemoveFollowing(FollowCreateDto dto) {
		GenericResponse result = await _repository.RemoveFollowings(User.Identity?.Name!, dto);
		return Result(result);
	}

	[HttpPost("ToggleBookmark")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> ToggleBookmark(BookmarkCreateDto dto) {
		GenericResponse i = await _repository.ToggleBookmark(dto);
		return Result(i);
	}

	[HttpPost("ReadBookmarks")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<BookmarkReadDto>?>>> ReadBookmarks() {
		GenericResponse<IEnumerable<BookmarkReadDto>?> i = await _repository.ReadBookmarks();
		return Result(i);
	}
}