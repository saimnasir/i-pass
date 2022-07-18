using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class MemoryRepository : GenericRepository<Memory, MyMemoryDbContext, Guid>, IMemoryRepository
    {
        public MemoryRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        public async Task<IEnumerable<Memory>> GetHistoriesAsync(Guid id)
        {
            var ctx = GetContext();
            var res = await ctx.Memories.TemporalAll().Where(g => g.Id == id).ToListAsync();
            foreach (var memory in res)
            {
                memory.Organization = await ctx.Organizations.SingleOrDefaultAsync(o => o.Id == memory.OrganizationId);
                memory.MemoryType = await ctx.MemoryTypes.SingleOrDefaultAsync(o => o.Id == memory.MemoryTypeId);
                memory.EnvironmentType = await ctx.EnvironmentTypes.SingleOrDefaultAsync(o => o.Id == memory.EnvironmentTypeId);
            }
            return res;
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<Memory> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<Memory>().Include(s => s.Organization).Include(s => s.MemoryType).Include(s => s.EnvironmentType);

    }
}
