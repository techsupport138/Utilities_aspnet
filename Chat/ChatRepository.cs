﻿namespace Utilities_aspnet.Chat;

public interface IChatRepository {
    Task<GenericResponse<ChatReadDto?>> Create(ChatCreateUpdateDto model);
    Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByUserId(string id);
    Task<GenericResponse<IEnumerable<ChatReadDto>?>> Read();
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
            var user = await _context.Set<UserEntity>().Include(x => x.Media).Select(x => new {x.Id, x.FullName, x.Media})
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
}