using System;
namespace Psycho.DAL.Core.Domain
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }

        public int AuthorizedUserId { get; set; }
        public virtual AuthorizedUser AuthorizedUser { get; set; }

        public DateTime? StartDataTime { get; set; }

        public DateTime? EndDataTime { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual AppointmentResult Result { get; set; }
    }
}
