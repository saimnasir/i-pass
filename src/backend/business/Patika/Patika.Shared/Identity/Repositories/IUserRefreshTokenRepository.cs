using Patika.Shared.Entities.Identity;
using Patika.Shared.Interfaces;
using System.Threading.Tasks;

namespace Patika.Shared.Identity.Repositories
{
    public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken>
    {
        Task<UserRefreshToken?> GetRefreshTokenAsync(string userId, string refreshToken);
    }
}