namespace Utilities_aspnet.Repositories;

public interface IChatRepository {
	Task<GenericResponse<ChatReadDto?>> Create(ChatCreateUpdateDto model);
	Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByUserId(string id, Guid? productId);
	Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByProductId(string userId, Guid productId);
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
		ProductEntity? productEntity = new();
		if (model.ProductId != null) productEntity = await _context.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == model.ProductId);
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (user == null) return new GenericResponse<ChatReadDto?>(null, UtilitiesStatusCodes.BadRequest);
		ChatEntity conversation = new() {
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
			FromUserId = userId!,
			ToUserId = model.UserId,
			MessageText = model.MessageText,
			ProductId = model.ProductId,
			ReadMessage = false
		};
		await _context.Set<ChatEntity>().AddAsync(conversation);
		await _context.SaveChangesAsync();
		ChatReadDto conversations = new() {
			Id = conversation.Id,
			DateTime = conversation.CreatedAt,
			MessageText = conversation.MessageText,
			FullName = user.FullName,
			ProfileImage = "",
			UserId = conversation.ToUserId,
			Product = productEntity,
			Send = true
		};
		return new GenericResponse<ChatReadDto?>(conversations);
	}
	
	public async Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByUserId(string id, Guid? productId) {
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		UserEntity? user = await _context.Set<UserEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id);
		ProductEntity product = new();
		if (productId != null) await _context.Set<ProductEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == productId);
		if (user == null) return new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.BadRequest);
		List<ChatEntity> conversation = await _context.Set<ChatEntity>()
			.Where(c => c.ToUserId == userId && c.FromUserId == id).Include(x => x.Media).ToListAsync();

		foreach (ChatEntity? item in conversation.Where(item => item.ReadMessage == false)) {
			item.ReadMessage = true;
			await _context.SaveChangesAsync();
		}

		IEnumerable<ChatEntity> conversationToUser = await _context.Set<ChatEntity>()
			.Where(x => (x.FromUserId == userId && x.ToUserId == id) && productId != null ? x.ProductId == productId : x.ProductId == null)
			.Include(x => x.Media).ToListAsync();

		conversation.AddRange(conversationToUser);
		List<ChatReadDto> conversations = conversation.Select(x => new ChatReadDto {
			Id = x.Id,
			DateTime = x.CreatedAt,
			MessageText = x.MessageText,
			FullName = user.FullName,
			PhoneNumber = user.PhoneNumber,
			AppUserName = user.AppUserName,
			Product = product,
			ProfileImage = "",
			UserId = id,
			Media = x.Media,
			Send = x.ToUserId == id
		}).OrderByDescending(x => x.DateTime).ToList();

		return !conversations.Any()
			? new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.NotFound)
			: new GenericResponse<IEnumerable<ChatReadDto>?>(conversations);
	}

	public async Task<GenericResponse<IEnumerable<ChatReadDto>?>> ReadByProductId(string uId, Guid productId) {
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		UserEntity? user = await _context.Set<UserEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == uId);
		ProductEntity? product = await _context.Set<ProductEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == productId);
		if (user == null || product == null) return new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.BadRequest);
		List<ChatEntity> conversation = await _context.Set<ChatEntity>()
			.Where(c => c.ToUserId == userId && c.FromUserId == uId).Include(x => x.Media).ToListAsync();

		foreach (ChatEntity? item in conversation.Where(item => item.ReadMessage == false)) {
			item.ReadMessage = true;
			await _context.SaveChangesAsync();
		}

		IEnumerable<ChatEntity> conversationToUser = await _context.Set<ChatEntity>()
			.Where(x => x.FromUserId == userId && x.ToUserId == uId && x.ProductId == productId).Include(x => x.Media).ToListAsync();

		conversation.AddRange(conversationToUser);
		List<ChatReadDto> conversations = conversation.Select(x => new ChatReadDto {
			Id = x.Id,
			DateTime = x.CreatedAt,
			MessageText = x.MessageText,
			FullName = user.FullName,
			PhoneNumber = user.PhoneNumber,
			AppUserName = user.AppUserName,
			ProfileImage = "",
			UserId = uId,
			Media = x.Media,
			Send = x.ToUserId == uId
		}).OrderByDescending(x => x.DateTime).ToList();

		return !conversations.Any()
			? new GenericResponse<IEnumerable<ChatReadDto>?>(null, UtilitiesStatusCodes.NotFound)
			: new GenericResponse<IEnumerable<ChatReadDto>?>(conversations);
	}

	public async Task<GenericResponse<IEnumerable<ChatReadDto>?>> Read() {
		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		List<string> toUserId = await _context.Set<ChatEntity>()
			.Where(x => x.FromUserId == userId).Include(x => x.Media).Select(x => x.ToUserId).ToListAsync();
		List<string> fromUserId = await _context.Set<ChatEntity>()
			.Where(x => x.ToUserId == userId).Include(x => x.Media).Select(x => x.FromUserId).ToListAsync();
		toUserId.AddRange(fromUserId);
		List<ChatReadDto> conversations = new();
		IEnumerable<string> userIds = toUserId.Distinct();

		foreach (string? item in userIds) {
			var user = await _context.Set<UserEntity>()
				.Include(x => x.Media).Select(x => new {x.Id, x.FullName, x.PhoneNumber}).FirstOrDefaultAsync(x => x.Id == item);
			ChatEntity? conversation = await _context.Set<ChatEntity>()
				.Where(c => c.FromUserId == item && c.ToUserId == userId || c.FromUserId == userId && c.ToUserId == item).OrderByDescending(c => c.CreatedAt)
				.Include(y => y.Media)
				.Take(1).FirstOrDefaultAsync();
			int? countUnReadMessage = _context.Set<ChatEntity>().Where(c => c.FromUserId == item && c.ToUserId == userId).Count(x => x.ReadMessage == false);
			conversations.Add(new ChatReadDto {
				Id = conversation!.Id,
				DateTime = conversation.CreatedAt,
				MessageText = conversation.MessageText,
				FullName = user?.FullName,
				PhoneNumber = user?.PhoneNumber,
				ProfileImage = "",
				UserId = conversation.ToUserId == item ? conversation.ToUserId : conversation.FromUserId,
				Send = conversation.ToUserId == item,
				UnReadMessages = countUnReadMessage
			});
		}

		return new GenericResponse<IEnumerable<ChatReadDto>?>(conversations.OrderByDescending(x => x.DateTime));
	}
}