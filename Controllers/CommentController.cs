namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : BaseApiController {
	private readonly ICommentRepository _repository;

	public CommentController(ICommentRepository commentRepository) => _repository = commentRepository;

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GenericResponse<CommentReadDto>>> Read(Guid id) => Result(await _repository.Read(id));

	[HttpGet("ReadByProductId/{id:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<CommentReadDto>?>>> ReadByProductId(Guid id) => Result(await _repository.ReadByProductId(id));

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Create(CommentCreateUpdateDto parameter) => Result(await _repository.Create(parameter));
	
	[HttpPost("ToggleLikeComment/{commentId:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<CommentReadDto?>>> ToggleLikeComment(Guid commentId)
		=> Result(await _repository.ToggleLikeComment(commentId));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Update(Guid id, CommentCreateUpdateDto parameter) => Result(await _repository.Update(id, parameter));

	[HttpDelete]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Delete(Guid id) => Result(await _repository.Delete(id));
}