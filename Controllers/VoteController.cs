namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : BaseApiController {
	private readonly IVoteRepository _repository;

	public VoteController(IVoteRepository repository) => _repository = repository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> Create(VoteCreateUpdateDto dto) => Result(await _repository.CreateUpdateVote(dto));

	[HttpPost("VoteField")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>>> CreateVoteFields(VoteFieldCreateUpdateDto dto)
		=> Result(await _repository.CreateUpdateVoteFields(dto));

	[HttpGet("VoteField/{id:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>>> ReadVoteFields(Guid id) => Result(await _repository.ReadVoteFields(id));

	[HttpGet("ReadProductVote/{productId:guid}/{userId}")]
	public ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>> ReadProductVote(Guid productId, string userId)
		=> Result(_repository.ReadProductVote(productId, userId));
}