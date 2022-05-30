using Microsoft.AspNetCore.Authorization;
using Utilities_aspnet.FollowBookmark;

namespace Utilities_aspnet.Controllers;

[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class BookmarkController : BaseApiController
{
    private readonly IFollowBookmarkRepository _repository;

    public BookmarkController(IFollowBookmarkRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> ToggleBookmark(BookmarkWriteDto parameters)
    {
        GenericResponse i = await _repository.ToggleBookmark(parameters);
        return Result(i);
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<BookmarkEntity>>>> GetBookMarks()
    {
        var result = await _repository.GetBookMarks();

        return Result(result);
    }
}