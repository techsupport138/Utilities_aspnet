namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class ConversationController : BaseApiController {
    private readonly IConversationRepository _conversationRepository;

    public ConversationController(IConversationRepository conversationRepository) {
        _conversationRepository = conversationRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GenericResponse<IEnumerable<ConversationsDto>?>>> Read()
    {
        GenericResponse<IEnumerable<ConversationsDto>?> i = await _conversationRepository.GetConversatios();
        return Result(i);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<GenericResponse<IEnumerable<ConversationsDto>?>>> ReadById(string userId)
    {
        GenericResponse<IEnumerable<ConversationsDto>?> i = await _conversationRepository.GetConversationByUserId(userId);
        return Result(i);
    }


    [HttpPost]
    public async Task<ActionResult<GenericResponse<ConversationsDto?>>> Create(AddConversationDto model)
    {
        GenericResponse<ConversationsDto?> i = await _conversationRepository.SendConversation(model);
        return Result(i);
    }
}