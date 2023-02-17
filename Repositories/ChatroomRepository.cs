using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Repositories
{
    public interface IChatroomRepository
    {
        Task CreateChatroom(string chatrooomName, Guid userId);
        Task EditChatroom(string chatrooomName, Guid userId);
        Task DeleteChatroom(Guid chatroomId, Guid userId);
        Task<List<ChatRoom>> GetChatroomsByName(string chatroomName);
        Task AddUserToChatroom(Guid chatroomId, Guid userId);
        Task AddMessageToChatroom(Guid roomId, ChatMessageInputDto message);
        Task<List<ChatMessage>> GetChatroomMessages(Guid chatroomId);
        Task EditGroupMessage(Guid roomId, ChatMessageEditDto message);
        Task DeleteGroupMessage(Guid roomId, ChatMessageDeleteDto message);
    }
    public class ChatroomRepository : IChatroomRepository
    {
        private readonly DbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ChatroomRepository(DbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task AddMessageToChatroom(Guid roomId, ChatMessageInputDto message)
        {
            var room = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == roomId);
            if (room == null)
                return;

            var messageToAdd = new ChatMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                FromUserId = message.FromUserId,
                ToUserId = message.ToUserId,
                ToGroupId = message.ToGroupId,
                MessageText = message.MessageText,
                RepliedTo = message.RepliedTo,
                ReferenceId = message.ReferenceId ?? string.Empty,
                ReferenceIdType = message.ReferenceIdType,
            };

            messageToAdd.UsersMentioned = UsersListInMessage(message.MessageText);

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

            room.Messages.Add(messageToAdd);
            await _context.SaveChangesAsync();

            if (messageToAdd.UsersMentioned.Count > 0)
            {
                //Todo: push notifications to mentioned users.
            }

        }

        public async Task AddUserToChatroom(Guid chatroomId, Guid userId)
        {
            var room = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == chatroomId);
            if (room != null)
            {
                room.Users.Add(userId);
                await _context.SaveChangesAsync();
            }

        }

        public async Task CreateChatroom(string chatrooomName, Guid userId)
        {
            var chatroomToAdd = new ChatRoom
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                UpdatedAt = DateTime.Now,
                Name = chatrooomName,
                Creator = userId,
                Users = new List<Guid> { userId }
            };
            _context.Set<ChatRoom>().Add(chatroomToAdd);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteChatroom(Guid chatroomId, Guid userId)
        {
            var chatroom = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == chatroomId);
            if (chatroom != null)
            {
                if (chatroom.Creator == userId)
                {
                    _context.Set<ChatRoom>().Remove(chatroom);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroupMessage(Guid roomId, ChatMessageDeleteDto message)
        {
            var room = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == roomId);
            if (room != null)
            {
                var messageToDelete = room.Messages.FirstOrDefault(x => x.Id == message.MessageId);
                if (messageToDelete == null)
                    return;

                if (messageToDelete.Id == message.UserId || messageToDelete.Id == room.Creator)
                {
                    room.Messages.Remove(messageToDelete);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task EditChatroom(string chatrooomName, Guid userId)
        {
            var chatroom = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Name == chatrooomName);

            if (chatroom != null)
            {
                if (chatroom.Creator == userId)
                {
                    chatroom.Name = chatrooomName;
                    chatroom.UpdatedAt = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditGroupMessage(Guid roomId, ChatMessageEditDto message)
        {
            var room = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == roomId);
            if (room != null)
            {
                var messageToEdit = room.Messages.FirstOrDefault(x => x.Id == message.MessageId);
                if (messageToEdit == null)
                    return;

                if (messageToEdit.FromUserId.ToString() == message.FromUserId)
                {
                    messageToEdit.UpdatedAt = DateTime.Now;
                    messageToEdit.MessageText = message.MessageText;
                }
            }
        }

        public async Task<List<ChatMessage>> GetChatroomMessages(Guid chatroomId)
        {
            var room = await _context.Set<ChatRoom>().FirstOrDefaultAsync(x => x.Id == chatroomId);
            return room.Messages;

        }

        public async Task<List<ChatRoom>> GetChatroomsByName(string chatroomName)
        {
            var rooms = await _context.Set<ChatRoom>().Where(x => x.Name == chatroomName).ToListAsync();
            return rooms;
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
