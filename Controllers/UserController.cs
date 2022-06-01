namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    [HttpPost("GetMobileVerificationCodeForLogin")]
    public async Task<ActionResult<GenericResponse>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto) {
        GenericResponse i = await _userRepository.GetMobileVerificationCodeForLogin(dto);
        return StatusCode(i.Status.value(), i);
    }

    [HttpPost("VerifyMobileForLogin")]
    public async Task<ActionResult<GenericResponse>> VerifyMobileForLogin(VerifyMobileForLoginDto dto) {
        GenericResponse i = await _userRepository.VerifyMobileForLogin(dto);
        return StatusCode(i.Status.value(), i);
    }

    [HttpGet("GetProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<UserReadDto>>> ReadProfile() {
        try {
            GenericResponse i = await _userRepository.GetProfile(User.Identity!.Name!);
            return StatusCode(i.Status.value(), i);
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
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }


    [HttpGet("GetProfiles")]
    public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> GetProfiles() {
        try {
            GenericResponse i = await _userRepository.GetUsers();
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("GetProfileByUsername/{username}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileByUsername(string username) {
        try {
            GenericResponse i = await _userRepository.GetProfileByUserName(username);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("GetProfileById/{id}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileById(string id) {
        try {
            GenericResponse i = await _userRepository.GetProfileById(id);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult<GenericResponse>> CreateUser(CreateProfileDto dto) {
        try {
            GenericResponse i = await _userRepository.CreateUser(dto);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<ActionResult<GenericResponse>> UpdateUser(UpdateProfileDto dto) {
        try {
            GenericResponse i = await _userRepository.UpdateUser(dto);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpDelete("DeleteUser")]
    public async Task<ActionResult<GenericResponse>> DeleteUser(string userId) {
        try {
            GenericResponse i = await _userRepository.DeleteUser(userId);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }
}