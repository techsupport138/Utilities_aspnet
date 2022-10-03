namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowBookmarkController : BaseApiController {
	private readonly IFollowBookmarkRepository _repository;

	public FollowBookmarkController(IFollowBookmarkRepository repository) => _repository = repository;

	[HttpPost("ReadFollowers/{userId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public ActionResult<GenericResponse<IEnumerable<UserReadDto>>> ReadFollowers(string userId) => Result(_repository.GetFollowers(userId));

	[HttpPost("ReadFollowings/{userId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public ActionResult<GenericResponse<IQueryable<UserReadDto>>> ReadFollowings(string userId) => Result(_repository.GetFollowing(userId));

	[HttpPost("ToggleFolllow")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> ToggleFollow(FollowCreateDto dto) => Result(await _repository.ToggleFollow(User.Identity?.Name!, dto));

	[HttpPost("RemoveFollowing")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> RemoveFollowing(FollowCreateDto dto)
		=> Result(await _repository.RemoveFollowings(User.Identity?.Name!, dto));

	[HttpPost("ToggleBookmark")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IActionResult> ToggleBookmark(BookmarkCreateDto dto) => Result(await _repository.ToggleBookmark(dto));

	[HttpPost("ReadBookmarks")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public ActionResult<GenericResponse<IEnumerable<BookmarkReadDto>?>> ReadBookmarks() => Result(_repository.ReadBookmarks());
}