using System;
namespace Psycho.DTO.Core
{
    public class CreatePsychologistDTO
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }

        public DateTime HireDate { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public CreatePsychologistDTO()
        {
        }
    }
}
