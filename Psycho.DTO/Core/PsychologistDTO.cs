using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Core
{
    public class PsychologistDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Gender? Gender { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }

        public DateTime? HireDate { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public PsychologistDTO()
        {
        }

        public List<string> ToList()
        {
            List<string> list = new List<string>
            {
                $"{FirstName} {LastName}", Email, Phone, Position, $"{HireDate?.Month}/{HireDate?.Day}/{HireDate?.Year}"
            };

            return list;
        }

        public List<string> ToGeneralList()
        {
            List<string> list = new List<string>
            {
                $"{FirstName} {LastName}", Email, Phone, Position, "Working hours"
            };

            return list;
        }
    }
}
