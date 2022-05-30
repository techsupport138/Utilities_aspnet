using Microsoft.AspNetCore.Authorization;
using Utilities_aspnet.FollowBookmark;

namespace Utilities_aspnet.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class BookmarkController : BaseApiController {
    private readonly IFollowBookmarkRepository _repository;

    public BookmarkController(IFollowBookmarkRepository repository) {
        _repository = repository;
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> ToggleBookmark(Guid id) {
        GenericResponse i = await _repository.ToggleBookmark(id);
        return Result(i);
    }
}