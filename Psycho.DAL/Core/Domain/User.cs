using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Psycho.DAL.Core.Domain
{
    public enum Gender : byte
    {
        Male = 1, Female = 2, Other = 3
    }

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public string Address { get; set; }

        public Gender? Gender { get; set; }

        public bool Blocked { get; set; }

        public virtual ICollection<Chat> ChatSender { get; set; } = new HashSet<Chat>();

        public virtual ICollection<Chat> ChatReceiver { get; set; } = new HashSet<Chat>();
    }

    public class Psychologist : User
    {
        public string Position { get; set; }

        public DateTime HireDate { get; set; }

        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();
    }

    public class AuthorizedUser : User
    {
        public int? Age { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();
    }

    public class AnonymousUser : User
    {

    }
}
