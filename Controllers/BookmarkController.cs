using Microsoft.AspNetCore.Authorization;
using Utilities_aspnet.FollowBookmark;

namespace Utilities_aspnet.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class BookmarkController : BaseApiController {
    private readonly IFollowBookmarkRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookmarkController(IFollowBookmarkRepository repository, IHttpContextAccessor httpContextAccessor) {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("ToggleBookmark/{id}")]
    public async Task<IActionResult> ToggleBookmark(Guid id) {
        GenericResponse i = await _repository.ToggleBookmark(_httpContextAccessor.HttpContext!.User.Identity!.Name!, id);
        return Result(i);
    }
}