using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class AppointmentResultRepository : Repository<AppointmentResult>, IAppointmentResultRepository
    {
        public AppointmentResultRepository(PsychoContext context) : base(context)
        {
        }
    }
}
