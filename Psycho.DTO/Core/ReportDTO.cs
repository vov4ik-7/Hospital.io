using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Core
{
    public class ReportDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public bool IsAnonymous { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Psychologist Psycho { get; set; } = new Psychologist();

        public AuthorizedUser User { get; set; } = new AuthorizedUser();

        public List<string> ToList()
        {
            List<string> temp = new List<string>()
            {
                $"{Psycho.FirstName} {Psycho.LastName}", $"{User.FirstName} {User.LastName}", Message
            };

            return temp;
        }
    }
}
