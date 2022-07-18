using Patika.Shared.Entities.Identity;
using Patika.Shared.Interfaces;
using System.Threading.Tasks;

namespace Patika.Shared.Identity.Repositories
{
    public interface IWrongPasswordAttemptRepository : IRepository<WrongPasswordAttempt>
    {
        Task<bool> IsUserBlockedAsync(string userId);
    }
}