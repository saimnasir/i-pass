using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class EnvironmentTypeRepository : GenericRepository<EnvironmentType, MyMemoryDbContext, Guid>, IEnvironmentTypeRepository
    {
        public EnvironmentTypeRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<EnvironmentType> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<EnvironmentType>();

    }
}
