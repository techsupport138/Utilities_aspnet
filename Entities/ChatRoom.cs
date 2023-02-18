using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Entities
{
    public class ChatRoom : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<string>? Users { get; set; }
        public string? Creator { get; set; }
        public List<ChatMessage>? Messages { get; set; }
    }
}
