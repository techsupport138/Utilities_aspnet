namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
	private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository) {
		_userRepository = userRepository;
	}

	[HttpPost("Register")]
	public async Task<ActionResult<GenericResponse>> Register(RegisterDto dto) {
		GenericResponse i = await _userRepository.Register(dto);
		return Result(i);
	}

	[HttpPost("LoginWithPassword")]
	public async Task<ActionResult<GenericResponse>> LoginWithPassword(LoginWithPasswordDto dto) {
		GenericResponse i = await _userRepository.LoginWithPassword(dto);
		return Result(i);
	}

	[HttpPost("CheckUserName/{userName}")]
	public async Task<ActionResult<GenericResponse>> CheckUserName(string userName) {
		GenericResponse i = await _userRepository.CheckUserName(userName);
		return Result(i);
	}

	[HttpPost("GetVerificationCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> GetVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
		GenericResponse i = await _userRepository.GetVerificationCodeForLogin(dto);
		return Result(i);
	}

	[HttpPost("VerifyCodeForLogin")]
	public async Task<ActionResult<GenericResponse>> VerifyCodeForLogin(VerifyMobileForLoginDto dto) {
		GenericResponse i = await _userRepository.VerifyCodeForLogin(dto);
		return Result(i);
	}

	[HttpPost("GetTokenForTest/{mobile}")]
	public async Task<ActionResult<GenericResponse>> GetTokenForTest(string mobile) {
		GenericResponse i = await _userRepository.GetTokenForTest(mobile);
		return Result(i);
	}

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Create(UserCreateUpdateDto dto) {
		GenericResponse i = await _userRepository.Create(dto);
		return Result(i);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpPost("Filter")]
	public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> Filter(UserFilterDto dto) {
		GenericResponse i = await _userRepository.Read(dto);
		return Result(i);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<GenericResponse<UserReadDto?>>> ReadById(string id) {
		GenericResponse i = await _userRepository.ReadById(id);
		return Result(i);
	}

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse<UserReadDto>>> Update(UserCreateUpdateDto dto) {
		GenericResponse<UserReadDto?> i = await _userRepository.Update(dto);
		return Result(i);
	}

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> Delete(string id) {
		GenericResponse i = await _userRepository.Delete(id);
		return Result(i);
	}

	[HttpDelete("DeleteFromTeam/{teamId:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<GenericResponse>> DeleteFromTeam(Guid teamId) {
		GenericResponse i = await _userRepository.RemovalFromTeam(teamId);
		return Result(i);
	}
}