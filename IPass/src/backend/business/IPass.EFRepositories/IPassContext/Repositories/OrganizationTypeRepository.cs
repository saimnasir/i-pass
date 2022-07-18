using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class OrganizationTypeRepository : GenericRepository<OrganizationType, MyMemoryDbContext, Guid>, IOrganizationTypeRepository
    {
        public OrganizationTypeRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<OrganizationType> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<OrganizationType>();

    }
}
