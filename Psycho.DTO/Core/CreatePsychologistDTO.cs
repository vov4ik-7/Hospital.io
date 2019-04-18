using System;
namespace Psycho.DTO.Core
{
    public class CreatePsychologistDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }

        public string HireDate { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public CreatePsychologistDTO()
        {
        }
    }
}
