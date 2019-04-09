using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class WorkScheduleRepository : Repository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(PsychoContext context) : base(context)
        {
        }
    }
}
