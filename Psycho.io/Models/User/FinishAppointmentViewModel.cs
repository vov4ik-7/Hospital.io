using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.io.Models.User
{
    public class FinishAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public List<Service> Services { get; set; }
    }
}
