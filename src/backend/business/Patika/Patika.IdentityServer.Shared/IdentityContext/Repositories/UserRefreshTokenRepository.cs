using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Identity.Repositories;
using Patika.Shared.Interfaces;

namespace Patika.IdentityServer.Shared.IdentityContext.Repositories
{
    public class UserRefreshTokenRepository : Repository<UserRefreshToken, IdentityServerDbContext>, IRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(DbContextOptions<IdentityServerDbContext> config) : base(config) { }

        public async Task<UserRefreshToken?> GetRefreshTokenAsync(string userId, string refreshToken)
        {
            using var ctx = GetContext();
            return await ctx.UserRefreshTokens.Where(x => x.UserId == userId && x.RefreshToken == refreshToken).FirstOrDefaultAsync();
        }

        protected override IdentityServerDbContext GetContext() => new(DbOptions);

        protected override IQueryable<UserRefreshToken> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<UserRefreshToken>();
    }
}