namespace Utilities_aspnet.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class BookmarkController : BaseApiController {
    private readonly IFollowBookmarkRepository _repository;

    public BookmarkController(IFollowBookmarkRepository repository) {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> ToggleBookmark(BookmarkWriteDto parameters) {
        GenericResponse i = await _repository.ToggleBookmark(parameters);
        return Result(i);
    }
}