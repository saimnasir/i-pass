using IPass.Domain.CommonDomain.Entities;
using IPass.Domain.CommonDomain.Repositories;
using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class UserRepository : GenericRepository<User, MyMemoryDbContext, Guid>, IUserRepository
    {
        public UserRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        public async Task AddOTPHistoryByPhoneAsync(string phoneNumber)
        {
            var ctx = GetContext();
            var user = await GetUserWithPhonenumberAsync(ctx, phoneNumber);
            user.OTPHistories.Add(new OTPHistory
            {
                SentAt = DateTime.Now,
                UserId = user.Id
            });
            await UpdateOneAsync(user);
        } 

        public async Task<int> GetSentTokenCountAsync(string phoneNumber)
        {
            var ctx = GetContext();
            var user = await GetUserWithPhonenumberAsync(ctx, phoneNumber);

            var today = DateTime.Now.Date;
            return user.OTPHistories.Where(m => m.SentAt.Date == today).ToList().Count;
        }
        private async Task<User> GetUserWithPhonenumberAsync(MyMemoryDbContext ctx, string phoneNumber)
        {

            var user = await GetDbSetWithIncludes(ctx).SingleOrDefaultAsync(m => m.PhoneNumber == phoneNumber);
            if (user == null)
                throw new Exception("User Not Found");
            return user;
        }

        public async Task<Guid?> GetIdByPhoneNumberAsync(string phoneNumber)
        {
            using var ctx = GetContext();
            var userId = await ctx.Users.Where(m => m.PhoneNumber == phoneNumber).Select(m => m.Id).FirstOrDefaultAsync();
            return userId;
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<User> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<User>()
            .Include(x => x.OTPHistories);

    }
}
