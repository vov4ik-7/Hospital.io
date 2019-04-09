using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class AnonymousUserRepository : Repository<AnonymousUser>, IAnonymousUserRepository
    {
        public AnonymousUserRepository(PsychoContext context) : base(context)
        {
        }
    }
}
