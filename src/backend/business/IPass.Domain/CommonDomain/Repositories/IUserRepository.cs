using IPass.Domain.CommonDomain.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace IPass.Domain.CommonDomain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<int> GetSentTokenCountAsync(string userName);
        Task AddOTPHistoryByPhoneAsync(string userName);
        Task<Guid?> GetIdByUserNameAsync(string userName);
    }
}