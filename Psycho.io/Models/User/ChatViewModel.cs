using System.Collections.Generic;
using Psycho.DTO.Core;

namespace Psycho.io.Models.User
{
    public class ChatViewModel
    {
        public DAL.Core.Domain.User OnChatWithOpen { get; set; }
        public List<UserChatDTO> Chats { get; set; }
    }
}
