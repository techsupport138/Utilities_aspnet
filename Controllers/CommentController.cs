using Utilities_aspnet.Comment;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CommentController : BaseApiController
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<CommentEntity>>> ShowComment(Guid id)
    {
        var result = await _commentRepository.ShowComment(id);

        return Result(result);
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse>> AddComment(CommentDto parameter)
    {
        var result = await _commentRepository.AddComment(parameter);

        return Result(result);
    }

    [HttpPut]
    public async Task<ActionResult<GenericResponse>> UpdateComment(Guid id, CommentDto parameter)
    {
        var result = await _commentRepository.UpdateComment(id, parameter);

        return Result(result);
    }

    [HttpDelete]
    public async Task<ActionResult<GenericResponse>> DeleteComment(Guid id)
    {
        var result = await _commentRepository.DeleteComment(id);

        return Result(result);
    }
}