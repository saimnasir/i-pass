using IPass.Domain.CommonDomain.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace IPass.Domain.CommonDomain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<int> GetSentTokenCountAsync(string phoneNumber);
        Task AddOTPHistoryByPhoneAsync(string phoneNumber);
        Task<Guid?> GetIdByPhoneNumberAsync(string phoneNumber);
    }
}