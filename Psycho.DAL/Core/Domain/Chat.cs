﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho.DAL.Core.Domain
{
    public class Chat
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public virtual User Sender { get; set; }

        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
