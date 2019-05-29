using System;
namespace Psycho.DTO.Core
{
    public class ReportDTO
    {
        public string Message { get; set; }

        public bool IsAnonymous { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
