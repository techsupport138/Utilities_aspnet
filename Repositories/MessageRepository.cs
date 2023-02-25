using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Repositories
{
    public interface IMessageRepository
    {
        Task AddEmojiToMessage(Reaction emoji, Guid messageId, Guid userId);
        Task<GenericResponse> AddPrivateMessage(ChatMessageInputDto message);
        Task<List<ChatMessage>> GetPrivateMessages(string firstUser, string secondUser);
        Task EditPrivateMessages(string messageText, Guid messageId);
        Task DeletePrivateMessage(Guid messageId);
        Task<bool> SeenMessage(Guid messageId, Guid userId);
        Task<List<ChatMessage>> GetLatestMessage(string userId);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly DbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MessageRepository(DbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task AddEmojiToMessage(Reaction emoji, Guid messageId, Guid userId)
        {
            var message = await _context.Set<ChatMessage>().FirstOrDefaultAsync(x => x.Id == messageId);

            if (message != null)
            {
                message.Emojies.Add(new Entities.ChatReaction { Reaction = emoji, UserId = userId });
                await _context.SaveChangesAsync();
            }

        }

        public async Task<GenericResponse> AddPrivateMessage(ChatMessageInputDto message)
        {
            var messageToAdd = new ChatMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                FromUserId = message.FromUserId,
                ToUserId = message.ToUserId,
                ToGroupId = message.ToGroupId,
                MessageText = message.MessageText,
                RepliedTo = message.RepliedTo,
                ReferenceIdType = message.ReferenceIdType,
                ReferenceId = message.ReferenceId ?? string.Empty,
            };

            //Todo: check if this works and how to save the reference in database?
            if (message.File != null)
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (message.File.Length > 0)
                {
                    string filePath = Path.Combine(new string[] { uploads, message.FromUserId, messageToAdd.CreatedAt.ToString(), message.File.FileName });
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await message.File.CopyToAsync(fileStream);
                    }
                    messageToAdd.FileName = message.File.FileName;
                    messageToAdd.FilePath = filePath;
                }
            }

            messageToAdd.UsersMentioned = UsersListInMessage(message.MessageText);

            await _context.Set<ChatMessage>().AddAsync(messageToAdd);
            await _context.SaveChangesAsync();
            if (messageToAdd.UsersMentioned.Count() != 0)
            {
                //Todo: push notifications for the mentioned users.
            }
            return new GenericResponse<Guid>(messageToAdd.Id);
        }

        public async Task DeletePrivateMessage(Guid messageId)
        {
            var message = await _context.Set<ChatMessage>().FirstOrDefaultAsync(x => x.Id == messageId);
            _context.Remove<ChatMessage>(message);
            await _context.SaveChangesAsync();

        }

        public async Task EditPrivateMessages(string messageText, Guid messageId)
        {
            var message = await _context.Set<ChatMessage>().FirstOrDefaultAsync(x => x.Id == messageId);
            if (message != null)
            {
                message.MessageText = messageText;
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<ChatMessage>> GetLatestMessage(string userId)
        {
            throw new NotImplementedException();

        }

        public async Task<List<ChatMessage>> GetPrivateMessages(string firstUser, string secondUser)
        {
            //Todo: add pagination to messages
            var firstMessageBatch = await _context.Set<ChatMessage>().Where(x => x.FromUserId == firstUser && x.ToUserId == secondUser).ToListAsync();
            var secondMessageBatch = await _context.Set<ChatMessage>().Where(x => x.FromUserId == secondUser && x.ToUserId == firstUser).ToListAsync();

            firstMessageBatch.AddRange(secondMessageBatch);
            var output = firstMessageBatch.OrderBy(x => x.CreatedAt).ToList();

            return output;

        }

        public async Task<bool> SeenMessage(Guid messageId, Guid userId)
        {
            var message = await _context.Set<ChatMessage>().FirstOrDefaultAsync(x => x.Id == messageId);
            if (message != null)
            {
                if (string.IsNullOrEmpty(message.ToGroupId.ToString()))
                {
                    message.ReadPrivateMessage = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    message.SeenStatus.Add(new SeenMessage { IsSeen = true, User = userId });
                    await _context.SaveChangesAsync();
                    return false;
                }

            }
            return false;
        }

        private List<string> UsersListInMessage(string message)
        {
            while (message.Last() == '@')
            {
                message = message.Remove(message.Length - 1);
            }

            var count = message.Count(x => x == '@');

            List<string> mentionList = new List<string>();

            for (int i = 0; i < count; i++)
            {
                try
                {
                    message = message.Remove(0, message.IndexOf("@"));
                    mentionList.Add(message.Substring(1, message.IndexOf(" ")));
                    message = message.Remove(0, message.IndexOf(" "));
                }
                catch (ArgumentOutOfRangeException)
                {
                    mentionList.Add(message.Substring(1, message.Length - 1));
                }
            }

            foreach (var item in mentionList)
            {
                //Todo: checking with username or Id or any other property
                var temp = _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.UserName == item);
                if (temp == null)
                    mentionList.Remove(item);
            }

            return mentionList;
        }

    }
}
