using System;
namespace Psycho.DTO.Core
{
    public class CreateReportDTO
    {
        public int PsychologistId { get; set; }
        public string PsychologistName { get; set; }

        public int AuthorizedUserId { get; set; }
        public string AuthorizedUserName { get; set; }

        public string Message { get; set; }

        public bool IsChecked { get; set; }
    }
}
