namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : BaseApiController {
	private readonly ICommentRepository _repository;

	public CommentController(ICommentRepository commentRepository) {
		_repository = commentRepository;
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<CommentReadDto>>> Read(Guid id) {
		GenericResponse<CommentReadDto?> result = await _repository.Read(id);
		return Result(result);
	}

	[HttpGet("ReadByProductId/{id:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<CommentReadDto>?>>> ReadByProductId(Guid id) {
		GenericResponse<IEnumerable<CommentReadDto>?> result = await _repository.ReadByProductId(id);
		return Result(result);
	}

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Create(CommentCreateUpdateDto parameter) {
		GenericResponse result = await _repository.Create(parameter);
		return Result(result);
	}

	[HttpPost("ToggleLikeComment/{commentId:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<CommentReadDto?>>> ToggleLikeComment(Guid commentId) {
		GenericResponse<CommentReadDto?> result = await _repository.ToggleLikeComment(commentId);
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