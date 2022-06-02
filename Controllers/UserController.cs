namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    [HttpPost("LoginWithEmail")]
    public async Task<ActionResult<GenericResponse>> LoginWithEmail(LoginWithEmailDto dto) {
        GenericResponse i = await _userRepository.LoginWithEmail(dto);
        return Result(i);
    }

    [HttpPost("GetMobileVerificationCodeForLogin")]
    public async Task<ActionResult<GenericResponse>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
        GenericResponse i = await _userRepository.GetMobileVerificationCodeForLogin(dto);
        return Result(i);
    }

    [HttpPost("VerifyMobileForLogin")]
    public async Task<ActionResult<GenericResponse>> VerifyMobileForLogin(VerifyMobileForLoginDto dto) {
        GenericResponse i = await _userRepository.VerifyMobileForLogin(dto);
        return Result(i);
    }

    [HttpGet("GetProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<UserReadDto>>> ReadProfile() {
        try {
            GenericResponse i = await _userRepository.GetProfile(User.Identity!.Name!);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value(),
                new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.Unhandled, "یه مشکلی پیش اومده"));
        }
    }

    [HttpPut("UpdateProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> UpdateProfile(UpdateProfileDto dto) {
        try {
            dto.Id = User.Identity.Name;
            GenericResponse i = await _userRepository.UpdateUser(dto);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("GetProfileByUsername/{username}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileByUsername(string username) {
        try {
            GenericResponse i = await _userRepository.GetProfileByUserName(username);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Create(CreateUserDto dto) {
        try {
            GenericResponse i = await _userRepository.CreateUser(dto);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> Read() {
        try {
            GenericResponse i = await _userRepository.GetUsers();
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> ReadById(string id) {
        try {
            GenericResponse i = await _userRepository.GetProfileById(id);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Update(UpdateProfileDto dto) {
        try {
            GenericResponse i = await _userRepository.UpdateUser(dto);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> Delete(string id) {
        try {
            GenericResponse i = await _userRepository.DeleteUser(id);
            return Result(i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }
}