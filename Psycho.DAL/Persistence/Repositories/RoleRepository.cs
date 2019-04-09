using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public PsychoContext PsychoContext
        {
            get { return Context as PsychoContext; }
        }

        public RoleRepository(PsychoContext context) : base(context)
        {
        }

        public Role TryGetByName(string name)
        {
            return PsychoContext.Roles.FirstOrDefault(r => r.Name == name);
        }
    }
}
