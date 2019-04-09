using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class PsychologistRepository : Repository<Psychologist>, IPsychologistRepository
    {
        public PsychologistRepository(PsychoContext context) : base(context)
        {
        }
    }
}
