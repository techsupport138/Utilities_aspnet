
namespace Utilities_aspnet.Conversation
{

    public interface IConversationRepository
    {
        Task<GenericResponse<IEnumerable<ConversationsDto>>> SendConversation(AddConversationDto model, string userId);
        Task<GenericResponse<IEnumerable<ConversationsDto>>> GetConversationByUserId(string id, string userId);
    }

    public class ConversationRepository : IConversationRepository
    {
        private readonly DbContext _context;
        public ConversationRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<GenericResponse<IEnumerable<ConversationsDto>>> SendConversation(AddConversationDto model, string userId)
        {
            var user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == model.UserId);
            var myUser = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return new GenericResponse<IEnumerable<ConversationsDto>>(null, UtilitiesStatusCodes.BadRequest);
            var conversation = new ConversationEntity
            {
                CreatedAt = DateTime.Now,
                FromUserId = userId,
                ToUserId = model.UserId,
                MessageText = model.MessageText
            };
            _context.Set<ConversationEntity>().Add(conversation);
            await _context.SaveChangesAsync();

            return new GenericResponse<IEnumerable<ConversationsDto>>(GetConversationByUserId(model.UserId, userId).Result.Result);
        }

        public async Task<GenericResponse<IEnumerable<ConversationsDto>>> GetConversationByUserId(string id, string userId)
        {
            var user = await _context.Set<UserEntity>().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return new GenericResponse<IEnumerable<ConversationsDto>>(null, UtilitiesStatusCodes.BadRequest);
            var conversation = await _context.Set<ConversationEntity>()
                .Where(c => c.ToUserId == userId && c.FromUserId == id).ToListAsync();
            var ToUserName = await _context.Set<ConversationEntity>().Where(x => x.FromUserId == userId && x.ToUserId == id).ToListAsync();

            foreach (var toUser in ToUserName)
            {
                conversation.Add(toUser);
            }
            var conversations = conversation.Select(x => new ConversationsDto
            {
                Id = x.Id,
                DateTime = (DateTime)x.CreatedAt,
                MessageText = x.MessageText,
                Name = user.FullName,
                ImageUrl = "",
                UserId = id,
                Send = x.ToUserId == id
            }).OrderByDescending(x => x.Id).ToList();

            if (conversations.Count < 1)
            {
                return new GenericResponse<IEnumerable<ConversationsDto>>(null, UtilitiesStatusCodes.NotFound);
            }

            return new GenericResponse<IEnumerable<ConversationsDto>>(conversations);

        }

    }


}
