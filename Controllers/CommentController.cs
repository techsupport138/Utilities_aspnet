using Utilities_aspnet.Comment;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController : BaseApiController {
    private readonly ICommentRepository _repository;

    public CommentController(ICommentRepository commentRepository) {
        _repository = commentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<CommentEntity>>> Read(Guid id) {
        GenericResponse<CommentEntity?> result = await _repository.Read(id);
        return Result(result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Create(CommentCreateUpdateDto parameter) {
        GenericResponse result = await _repository.Create(parameter);
        return Result(result);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Update(Guid id, CommentCreateUpdateDto parameter) {
        GenericResponse result = await _repository.Update(id, parameter);
        return Result(result);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Delete(Guid id) {
        GenericResponse result = await _repository.Delete(id);
        return Result(result);
    }
}