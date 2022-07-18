using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using Patika.Shared.Entities.Loggy.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class LogRepository : GenericRepository<Log, MyMemoryDbContext, Guid>, ILogRepository
    {
        public LogRepository(DbContextOptions<MyMemoryDbContext> config) : base(config) { }

        public async Task<LogDetail> AddDetail(Guid logId, LogDetail logDetail)
        {
            logDetail.LogId = logId;
            logDetail.Created = DateTime.Now;
            using var ctx = GetContext();
            ctx.Set<LogDetail>().Add(logDetail);
            await ctx.SaveChangesAsync();
            return logDetail;
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<Log> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<Log>().Include(x => x.Details);
    }
}
