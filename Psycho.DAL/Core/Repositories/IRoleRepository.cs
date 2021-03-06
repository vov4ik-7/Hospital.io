﻿using System;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role TryGetByName(string name);
    }
}
