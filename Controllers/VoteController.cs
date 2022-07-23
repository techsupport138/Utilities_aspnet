namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : BaseApiController {
	private readonly IVoteRepository _voteRepository;

	public VoteController(IVoteRepository voteRepository) => _voteRepository = voteRepository;

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse>> Create(VoteCreateUpdateDto dto)
		=> Result(await _voteRepository.CreateUpdateVote(dto));

	[HttpPost("VoteField")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteReadDto>?>>> CreateVoteFields(VoteFieldCreateUpdateDto dto)
		=> Result(await _voteRepository.CreateUpdateVoteFields(dto));

	[HttpGet("VoteField/{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
	public async Task<ActionResult<GenericResponse<IEnumerable<VoteReadDto>?>>> ReadVoteFields(Guid id)
		=> Result(await _voteRepository.ReadVoteFields(id));
}