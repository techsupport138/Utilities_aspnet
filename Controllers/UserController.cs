namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
	private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository) => _userRepository = userRepository;

	[HttpPost("Register")]
	public async Task<ActionResult<GenericResponse>> Register(RegisterDto dto) => Result(await _userRepository.Register(dto));

	[HttpPost("LoginWithPassword")]
	public async Task<ActionResult<GenericResponse>> LoginWithPassword(LoginWithPasswordDto dto) => Result(await _userRepository.LoginWithPassword(dto));

	[HttpPost("CheckUserName/{userName}")]
	public async Task<ActionResult<GenericResponse>> CheckUserName(string userName) => Result(await _userRepository.CheckUserName(userName));

	[HttpPost("GetVerificationCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto)
		=> Result(await _userRepository.GetVerificationCodeForLogin(dto));

	[HttpPost("VerifyCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> VerifyCodeForLogin(VerifyMobileForLoginDto dto) => Result(await _userRepository.VerifyCodeForLogin(dto));

	[HttpPost("GetTokenForTest/{mobile}")]
	public async Task<ActionResult<GenericResponse>> GetTokenForTest(string mobile) => Result(await _userRepository.GetTokenForTest(mobile));
	
	[HttpDelete("Logout")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Logout() => Result(await _userRepository.Logout());
	
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserEntity>>>> Filter(UserFilterDto dto) => Result(await _userRepository.Filter(dto));

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<GenericResponse<UserEntity?>>> ReadById(string id, bool showVotes = false)
		=> Result(await _userRepository.ReadById(id, showVotes: showVotes));

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<UserEntity>>> Update(UserCreateUpdateDto dto) => Result(await _userRepository.Update(dto));

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Delete(string id) => Result(await _userRepository.Delete(id));

	[HttpDelete("DeleteFromTeam/{teamId:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> DeleteFromTeam(Guid teamId) => Result(await _userRepository.RemovalFromTeam(teamId));
}