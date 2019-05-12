using System;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Core
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }

        //Psychologist
        public string Position { get; set; }

        public DateTime? HireDate { get; set; }

        //AuthorizedUser
        public int? Age { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

    }
}
