using Utilities_aspnet.User;

namespace Utilities_aspnet.Chat;

public interface IChatRepository {
    Task<GenericResponse<ChatReadDto?>> Create(ChatCreateUpdateDto model);
    Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByUserId(string id);
    Task<GenericResponse<IEnumerable<ChatReadDto>?>> Read();
    Task<GenericResponse<ChatGroupReadDto?>> CreateGroup(ChatGroupCreateDto model);
    Task<GenericResponse<ChatGroupReadDto?>> AddMemebrToGroup(AddMemberToGroup dto);
}

public class ChatRepository : IChatRepository {
    private readonly DbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChatRepository(DbContext context, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<ChatReadDto?>> Create(ChatCreateUpdateDto model) {
        UserEntity? user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == model.UserId);
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        UserEntity? myUser = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) return new GenericResponse<ChatReadDto?>(null, UtilitiesStatusCodes.BadRequest);
        ChatEntity? conversation = new ChatEntity() {
            CreatedAt = DateTime.Now,
            FromUserId = userId,
            ToUserId = model.UserId,
            MessageText = model.MessageText,
        };
        await _context.Set<ChatEntity>().AddAsync(conversation);
        await _context.SaveChangesAsync();
        ChatReadDto? conversations = new ChatReadDto {
            Id = conversation.Id,
            DateTime = (DateTime) conversation.CreatedAt,
            MessageText = conversation.MessageText,
            FullName = user.FullName,
            ProfileImage = "",
            UserId = conversation.ToUserId,
            Send = true
        };
        return new GenericResponse<ChatReadDto?>(conversations);
    }

    public async Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByUserId(string id) {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        UserEntity? user = await _context.Set<UserEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.BadRequest);
        List<ChatEntity>? conversation = await _context.Set<ChatEntity>()
            .Where(c => c.ToUserId == userId && c.FromUserId == id).ToListAsync();
        List<ChatEntity>? conversationToUser = await _context.Set<ChatEntity>()
            .Where(x => x.FromUserId == userId && x.ToUserId == id).ToListAsync();

        foreach (ChatEntity? toUser in conversationToUser) conversation.Add(toUser);
        List<ChatReadDto>? conversations = conversation.Select(x => new ChatReadDto {
            Id = x.Id,
            DateTime = (DateTime) x.CreatedAt,
            MessageText = x.MessageText,
            FullName = user.FullName,
            ProfileImage = "",
            UserId = id,
            Send = x.ToUserId == id
        }).OrderByDescending(x => x.Id).ToList();

        if (conversations.Count < 1)
            return new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.NotFound);

        return new GenericResponse<IEnumerable<ChatReadDto>?>(conversations);
    }

    public async Task<GenericResponse<IEnumerable<ChatReadDto>?>> Read() {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<string>? ToUserId = await _context.Set<ChatEntity>().Where(x => x.FromUserId == userId)
            .Select(x => x.ToUserId).ToListAsync();
        List<string>? FromUserId = await _context.Set<ChatEntity>().Where(x => x.ToUserId == userId)
            .Select(x => x.FromUserId).ToListAsync();
        foreach (string? fromUser in FromUserId) ToUserId.Add(fromUser);
        List<ChatReadDto>? conversations = new();
        IEnumerable<string>? userIds = ToUserId.Distinct();

        foreach (string? item in userIds) {
            var user = await _context.Set<UserEntity>().Include(x => x.Media).Select(x => new {x.Id, x.FullName})
                .FirstOrDefaultAsync(x => x.Id == item);
            ChatEntity? conversation = await _context.Set<ChatEntity>()
                .Where(c => c.FromUserId == item && c.ToUserId == userId || c.FromUserId == userId && c.ToUserId == item)
                .OrderByDescending(c => c.CreatedAt).Take(1).FirstOrDefaultAsync();
            conversations.Add(new ChatReadDto {
                Id = conversation.Id,
                DateTime = (DateTime) conversation.CreatedAt,
                MessageText = conversation.MessageText,
                FullName = user.FullName,
                ProfileImage = "",
                UserId = conversation.ToUserId == item ? conversation.ToUserId : conversation.FromUserId,
                Send = conversation.ToUserId == item
            });
        }

        if (conversations.Count < 1)
            return new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.NotFound);
        return new GenericResponse<IEnumerable<ChatReadDto>?>(conversations.OrderByDescending(x => x.DateTime));
    }

    public async Task<GenericResponse<ChatGroupReadDto?>> CreateGroup(ChatGroupCreateDto model)
    {
        ChatGroupEntity? group = new ChatGroupEntity()
        {
            CreatedAt = DateTime.Now,
            OwnerId = (Guid)model.OwnerId , 
            Description = model.Description ,
            Private = model.Private ,
            Title = model.Title 
        };
        await _context.Set<ChatGroupEntity>().AddAsync(group);
        await _context.SaveChangesAsync();
        ChatGroupReadDto? groupInfo = new ChatGroupReadDto
        {
            Id = group.Id,
            Description = group.Description ,
            LastMessage = null ,
            Logo = null ,
            OwnerId = group.OwnerId ,
            DateTime = (DateTime)group.CreatedAt,
            Private = group.Private ,
            Messages = null ,
            Title = group.Title
        };

        return new GenericResponse<ChatGroupReadDto?>(groupInfo);
    }

    public async Task<GenericResponse<ChatGroupReadDto?>> AddMemebrToGroup(AddMemberToGroup dto)
    {
        if (dto.MemberIds is null) return new GenericResponse<ChatGroupReadDto?>(null, UtilitiesStatusCodes.BadRequest);
        ChatGroupEntity? group = await _context.Set<ChatGroupEntity>().Include(x=> x.ChatGroupMembers).FirstOrDefaultAsync(x => x.Id == dto.GroupId);
        if (group == null) return new GenericResponse<ChatGroupReadDto?>(null, UtilitiesStatusCodes.BadRequest);

        var oldMemberIds = group.ChatGroupMembers?.Select(x => x.MemebrId).ToList();

        foreach (var memebrId in dto.MemberIds.Where(x => !oldMemberIds.Contains(x)))
        {
            var newChatGroupMemeber = new ChatGroupMemberEntity
            {
                ChatGroupId = dto.GroupId ,
                MemebrId = memebrId , 
                CreatedAt = DateTime.Now
            };

            await _context.Set<ChatGroupMemberEntity>().AddAsync(newChatGroupMemeber);
        }

        await _context.SaveChangesAsync();

        ChatGroupReadDto? groupInfo = new ChatGroupReadDto
        {
            Id = group.Id,
            Description = group.Description,
            LastMessage = null,
            Logo = null,
            OwnerId = group.OwnerId,
            DateTime = (DateTime)group.CreatedAt,
            Private = group.Private,
            Messages = null,
            Title = group.Title
        };

        return new GenericResponse<ChatGroupReadDto?>(groupInfo);
    }
}