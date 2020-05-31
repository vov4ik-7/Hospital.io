
namespace Psycho.DAL.Core.Domain
{
    public class AppointmentResult
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        public int? NextAppointmentId { get; set; }
        public virtual Appointment NextAppointment { get; set; }

        public string ServicesJson { get; set; }
    }
}
