using Microsoft.EntityFrameworkCore;
using Patika.Shared.Interfaces;
using System;

namespace Patika.EF.Shared
{
    public abstract class Repository<T, TDbContext> : GenericRepository<T, TDbContext, Guid>, IRepository<T> where T : class, IEntity<Guid>, new() where TDbContext : DbContext
    {
        protected Repository(DbContextOptions<TDbContext> options) : base(options)
        {
        }
    }
}