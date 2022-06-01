namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("LoginFormWithEmail")]
    public async Task<ActionResult<GenericResponse>> LoginFormWithEmail(LoginWithEmailDto dto)
    {
        GenericResponse i = await _userRepository.LoginFormWithEmail(dto);
        return Result(i);
    }

    [HttpPost("LoginWithEmail")]
    public async Task<ActionResult<GenericResponse>> LoginWithEmail(LoginWithEmailDto dto)
    {
        GenericResponse i = await _userRepository.LoginWithEmail(dto);
        return Result(i);
    }

    [HttpPost("GetMobileVerificationCodeForLogin")]
    public async Task<ActionResult<GenericResponse>> GetMobileVerificationCodeForLogin(GetMobileVerificationCodeForLoginDto dto)
    {
        GenericResponse i = await _userRepository.GetMobileVerificationCodeForLogin(dto);
        return Result(i);
    }

    [HttpPost("VerifyMobileForLogin")]
    public async Task<ActionResult<GenericResponse>> VerifyMobileForLogin(VerifyMobileForLoginDto dto)
    {
        GenericResponse i = await _userRepository.VerifyMobileForLogin(dto);
        return Result(i);
    }

    [HttpGet("GetProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse<UserReadDto>>> ReadProfile()
    {
        try
        {
            GenericResponse i = await _userRepository.GetProfile(User.Identity!.Name!);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value(),
                new GenericResponse<UserReadDto>(null, UtilitiesStatusCodes.Unhandled, "یه مشکلی پیش اومده"));
        }
    }

    [HttpPut("UpdateProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> UpdateProfile(UpdateProfileDto dto)
    {
        try
        {
            dto.Id = User.Identity.Name;
            GenericResponse i = await _userRepository.UpdateUser(dto);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }


    [HttpGet("GetUsers")]
    public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> GetUsers()
    {
        try
        {
            GenericResponse i = await _userRepository.GetUsers();
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("GetProfileByUsername/{username}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileByUsername(string username)
    {
        try
        {
            GenericResponse i = await _userRepository.GetProfileByUserName(username);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet("GetProfileById/{id}")]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileById(string id)
    {
        try
        {
            GenericResponse i = await _userRepository.GetProfileById(id);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPost("CreateUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> CreateUser(CreateProfileDto dto)
    {
        try
        {
            GenericResponse i = await _userRepository.CreateUser(dto);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPut("UpdateUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> UpdateUser(UpdateProfileDto dto)
    {
        try
        {
            GenericResponse i = await _userRepository.UpdateUser(dto);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpDelete("DeleteUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<GenericResponse>> DeleteUser(string userId)
    {
        try
        {
            GenericResponse i = await _userRepository.DeleteUser(userId);
            return Result(i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }
}