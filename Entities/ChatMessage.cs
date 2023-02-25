using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Utilities;

namespace Utilities_aspnet.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string? FromUserId { get; set; } = null!;
        public string? ToUserId { get; set; } = null!;
        public Guid? ToGroupId { get; set; }

        [StringLength(2000)]
        public string? MessageText { get; set; } = null!;
        public bool? ReadPrivateMessage { get; set; } = false;
        public List<SeenMessage>? SeenStatus { get; set; } = new List<SeenMessage>();
        public List<ChatReaction>? Emojies { get; set; } = new List<ChatReaction>();
        public string? RepliedTo { get; set; }
        public List<string>? UsersMentioned { get; set; }
        public string? ReferenceId { get; set; }
        public ReferenceIdType? ReferenceIdType { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public ProductEntity? ProductEntity { get; set; }
        public CategoryEntity? CategoryEntity { get; set; }
        public UserEntity? UserEntity { get; set; }

    }

    public class SeenMessage : BaseEntity
    {
        public bool IsSeen { get; set; } = false;
        public Guid? User { get; set; }
    }

    public class ChatReaction : BaseEntity
    {
        public Reaction? Reaction { get; set; }
        public Guid? UserId { get; set; }
    }


    public class ChatMessageInputDto
    {
        public string FromUserId { get; set; } = null!;
        public string? ToUserId { get; set; } = null!;
        public Guid? ToGroupId { get; set; }

        [StringLength(2000)]
        public string? MessageText { get; set; } = null;
        public string? RepliedTo { get; set; }
        public string? ReferenceId { get; set; }
        public ReferenceIdType? ReferenceIdType { get; set; }
        public IFormFile? File { get; set; }
    }

    public class ChatMessageDeleteDto
    {
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
    }

    public class ChatMessageEditDto
    {
        public Guid MessageId { get; set; }
        public string FromUserId { get; set; } = null!;
        public string? ToUserId { get; set; } = null!;
        public Guid? ToGroupId { get; set; }

        [StringLength(2000)]
        public string MessageText { get; set; } = null!;
        public string? RepliedTo { get; set; }
    }
}
