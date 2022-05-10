using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities_aspnet.User.Data;
using Utilities_aspnet.User.Dtos;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.ApiControllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    [HttpPost("GetMobileVerificationCodeForLogin")]
    public async Task<ActionResult<GenericResponse>> GetMobileVerificationCodeForLogin(
        GetMobileVerificationCodeForLoginDto dto) {
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
    public async Task<ActionResult<GenericResponse<UserReadDto>>> GetProfile() {
        try {
            GenericResponse i = await _userRepository.GetProfile(User.Identity!.Name!);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception e) {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value(),
                new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.Unhandled, "یه مشکلی پیش اومده"));
        }
    }

    [HttpGet("GetProfileByUserName")]
    public async Task<ActionResult<GenericResponse<UserReadDto>>> GetUserByUserName(string userName) {
        GenericResponse i = await _userRepository.GetProfileByUserName(userName);
        return StatusCode(i.Status.value(), i);
    }

    [HttpGet("GetProfileById")]
    public async Task<ActionResult<GenericResponse<UserReadDto>>> GetUserById(string id) {
        GenericResponse i = await _userRepository.GetProfileById(id);
        return StatusCode(i.Status.value(), i);
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