using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(PsychoContext context) : base(context)
        {
        }
    }
}
