namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : BaseApiController {
	private readonly IVoteRepository _voteRepository;

	public VoteController(IVoteRepository voteRepository) => _voteRepository = voteRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Create(VoteCreateUpdateDto dto) => Result(await _voteRepository.CreateUpdateVote(dto));

	[HttpPost("VoteField")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>>> CreateVoteFields(VoteFieldCreateUpdateDto dto)
		=> Result(await _voteRepository.CreateUpdateVoteFields(dto));

	[HttpGet("VoteField/{id:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>>> ReadVoteFields(Guid id) => Result(await _voteRepository.ReadVoteFields(id));

	[HttpGet("ReadProductVote/{productId:guid}/{userId}")]
	public ActionResult<GenericResponse<IEnumerable<VoteFieldEntity>?>> ReadProductVote(Guid productId, string userId)
		=> Result(_voteRepository.ReadProductVote(productId, userId));
}