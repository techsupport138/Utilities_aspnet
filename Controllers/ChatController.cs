namespace Utilities_aspnet.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class ChatController : BaseApiController {
	private readonly IChatRepository _chatRepository;

	public ChatController(IChatRepository chatRepository) => _chatRepository = chatRepository;

	[HttpGet]
	public async Task<ActionResult<GenericResponse<IEnumerable<ChatReadDto>?>>> Read() => Result(await _chatRepository.Read());

	[HttpGet("{userId}/{productId:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<ChatReadDto>?>>> ReadById(string userId, Guid? productId)
		=> Result(await _chatRepository.ReadByUserId(userId, productId));

	[HttpPost]
	public async Task<ActionResult<GenericResponse<ChatReadDto?>>> Create(ChatCreateUpdateDto model) => Result(await _chatRepository.Create(model));

	[HttpPost("CreateGroupChat")]
	public async Task<ActionResult<GenericResponse<GroupChatEntity?>>> CreateGroupChat(GroupChatCreateUpdateDto dto)
		=> Result(await _chatRepository.CreateGroupChat(dto));

	[HttpPut("UpdateGroupChat")]
	public async Task<ActionResult<GenericResponse<GroupChatEntity?>>> UpdateGroupChat(GroupChatCreateUpdateDto dto)
		=> Result(await _chatRepository.UpdateGroupChat(dto));

	[HttpPost("CreateGroupChatMessage")]
	public async Task<ActionResult<GenericResponse<GroupChatEntity?>>> CreateGroupChatMessage(GroupChatMessageCreateUpdateDto dto)
		=> Result(await _chatRepository.CreateGroupChatMessage(dto));

	[HttpGet("ReadMyGroupChats")]
	public ActionResult<GenericResponse<GroupChatMessageEntity?>> ReadMyGroupChats() => Result(_chatRepository.ReadMyGroupChats());

	[HttpGet("ReadGroupChatMessages/{id:guid}")]
	public ActionResult<GenericResponse<GroupChatMessageEntity?>> ReadGroupChatMessages(Guid id) => Result(_chatRepository.ReadGroupChatMessages(id));
}