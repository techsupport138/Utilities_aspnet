namespace Utilities_aspnet.Controllers.Admin;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdminUserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public AdminUserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<UserReadDto>>>> GetProfiles()
    {
        try
        {
            GenericResponse i = await _userRepository.GetUsers();
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileByUsername(string username)
    {
        try
        {
            GenericResponse i = await _userRepository.GetProfileByUserName(username);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<UserReadDto?>>> GetProfileById(string id)
    {
        try
        {
            GenericResponse i = await _userRepository.GetProfileById(id);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse>> CreateProfile(CreateProfileDto dto)
    {
        try
        {
            GenericResponse i = await _userRepository.CreateUser(dto);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPut]
    public async Task<ActionResult<GenericResponse>> UpdateProfile(string userId, UpdateProfileDto dto)
    {
        try
        {
            GenericResponse i = await _userRepository.UpdateUser(dto, userId);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }

    [HttpPut]
    public async Task<ActionResult<GenericResponse>> DeleteProfile(string userId)
    {
        try
        {
            GenericResponse i = await _userRepository.DeleteUser(userId);
            return StatusCode(i.Status.value(), i);
        }
        catch (Exception)
        {
            return StatusCode(UtilitiesStatusCodes.Unhandled.value());
        }
    }
}