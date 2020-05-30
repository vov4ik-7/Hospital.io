using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Psycho.DAL
{
    public static class Extensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = dbSet.Any(predicate);
            if (!exists)
            {
                dbSet.Add(entity);
                return entity;
            }

            return null;
        }
    }
}
