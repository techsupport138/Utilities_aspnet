using Microsoft.AspNetCore.Authorization;

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
        catch (Exception e) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value(),
                new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.Unhandled, "یه مشکلی پیش اومده"));
        }
    }

    [HttpPut("UpdateProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> UpdateProfile(UpdateProfileDto dto) {
        try {
            GenericResponse i = await _userRepository.UpdateUser(dto, User.Identity.Name);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception e) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }
}