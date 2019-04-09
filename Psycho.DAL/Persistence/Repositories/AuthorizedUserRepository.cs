using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class AuthorizedUserRepository : Repository<AuthorizedUser>, IAuthorizedUserRepository
    {
        public AuthorizedUserRepository(PsychoContext context) : base(context)
        {
        }
    }
}
