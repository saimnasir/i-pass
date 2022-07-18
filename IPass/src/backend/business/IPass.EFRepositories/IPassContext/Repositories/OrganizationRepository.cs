using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization, MyMemoryDbContext, Guid>, IOrganizationRepository
    {
        public OrganizationRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<Organization> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<Organization>().Include(s => s.ParentOrganization).Include(s => s.OrganizationType);

    }
}
