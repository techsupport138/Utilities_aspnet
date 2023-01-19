namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
	private readonly IUserRepository _repository;

	public UserController(IUserRepository repository) => _repository = repository;

	[HttpPost("Register")]
	public async Task<ActionResult<GenericResponse>> Register(RegisterDto dto) => Result(await _repository.Register(dto));

	[HttpPost("LoginWithPassword")]
	public async Task<ActionResult<GenericResponse>> LoginWithPassword(LoginWithPasswordDto dto) => Result(await _repository.LoginWithPassword(dto));

	[HttpPost("CheckUserName/{userName}")]
	public async Task<ActionResult<GenericResponse>> CheckUserName(string userName) => Result(await _repository.CheckUserName(userName));

	[HttpPost("GetVerificationCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto)
		=> Result(await _repository.GetVerificationCodeForLogin(dto));

	[HttpPost("VerifyCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> VerifyCodeForLogin(VerifyMobileForLoginDto dto) => Result(await _repository.VerifyCodeForLogin(dto));

	[HttpPost("GetTokenForTest/{mobile}")]
	public async Task<ActionResult<GenericResponse>> GetTokenForTest(string mobile) => Result(await _repository.GetTokenForTest(mobile));

	[HttpDelete("Logout")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> Logout() => Result(await _repository.Logout());

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserEntity>>>> Filter(UserFilterDto dto) => Result(await _repository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<GenericResponse<UserEntity?>>> ReadById(string id, bool showVotes = false)
		=> Result(await _repository.ReadById(id, showVotes: showVotes));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse<UserEntity>>> Update(UserCreateUpdateDto dto) => Result(await _repository.Update(dto));

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> Delete(string id) => Result(await _repository.Delete(id));

	[HttpDelete("DeleteFromTeam/{teamId:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ClaimRequirement]
	public async Task<ActionResult<GenericResponse>> DeleteFromTeam(Guid teamId) => Result(await _repository.RemovalFromTeam(teamId));
	
	[HttpGet("ReadMyBlockList")]
	public ActionResult<GenericResponse<IQueryable<UserEntity>>> ReadMine() => Result(_repository.ReadMyBlockList());

	[HttpPost("ToggleBlock")]
	public async Task<ActionResult<GenericResponse>> Create(string userId) => Result(await _repository.ToggleBlock(userId));
}