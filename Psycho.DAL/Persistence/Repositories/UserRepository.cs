using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PsychoContext context) : base(context)
        {
        }
    }
}
