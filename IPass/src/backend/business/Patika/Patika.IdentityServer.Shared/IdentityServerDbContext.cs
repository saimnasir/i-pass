using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patika.Shared.Entities.Identity;

namespace Patika.IdentityServer.Shared
{
	public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<WrongPasswordAttempt> WhereWrongPasswordAttempts => Set<WrongPasswordAttempt>();
        public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();

        public IdentityServerDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}