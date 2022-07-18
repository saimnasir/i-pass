using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class MemoryTypeRepository : GenericRepository<MemoryType, MyMemoryDbContext, Guid>, IMemoryTypeRepository
    {
        public MemoryTypeRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<MemoryType> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<MemoryType>();

    }
}
