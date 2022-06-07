namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]/[action]")]
public class FollowBookmarkController : BaseApiController
{
    private readonly IFollowBookmarkRepository _followBookmarkRepository;

    public FollowBookmarkController(IFollowBookmarkRepository followBookmarkRepository)
    {
        _followBookmarkRepository = followBookmarkRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<FollowReadDto>>> GetFollowers()
    {
        var result = await _followBookmarkRepository.GetFollowers(User.Identity?.Name!);

        return Result(result);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<FollowReadDto>>> GetFollowings()
    {
        var result = await _followBookmarkRepository.GetFollowing(User.Identity?.Name!);

        return Result(result);
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse>> ToggleFollow(FollowWriteDto parameter)
    {
        var result = await _followBookmarkRepository.ToggleFollow(User.Identity?.Name!, parameter);

        return Result(result);
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse>> RemoveFollowings(FollowWriteDto parameter)
    {
        var result = await _followBookmarkRepository.RemoveFollowings(User?.Identity?.Name!, parameter);

        return Result(result);
    }
}