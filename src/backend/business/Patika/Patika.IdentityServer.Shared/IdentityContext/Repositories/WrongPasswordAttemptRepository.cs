using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Identity.Repositories;
using Patika.Shared.Interfaces;

namespace Patika.IdentityServer.Shared.Repositories
{
    public class WrongPasswordAttemptRepository : Repository<WrongPasswordAttempt, IdentityServerDbContext>, IWrongPasswordAttemptRepository, IRepository<WrongPasswordAttempt>
    {
        public WrongPasswordAttemptRepository(DbContextOptions<IdentityServerDbContext> config) : base(config) { }

        public async Task<bool> IsUserBlockedAsync(string userId)
        {
            var todayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var tommorow = todayStart.AddDays(1);
            using var ctx = GetContext();
            var attempCount = await ctx.WhereWrongPasswordAttempts.Where(m => 
            m.UserId == userId &&
            m.AttemptTime >= todayStart &&
            m.AttemptTime < tommorow
            ).CountAsync();

            return attempCount >= 3;
        }

        protected override IdentityServerDbContext GetContext() => new(DbOptions);

        protected override IQueryable<WrongPasswordAttempt> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<WrongPasswordAttempt>();
    }
}
