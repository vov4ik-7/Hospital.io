using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Psycho.DAL.Core.Domain
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        public Role(string name) : base(name) { }
    }
}
