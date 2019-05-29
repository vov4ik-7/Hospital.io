using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Core
{
    public class UserChatDTO
    {
        //List messages
        public List<Chat> chat { get; set; } = new List<Chat>();

        //man to communicate
        public User User { get; set; } = new Psychologist();
    }
}
