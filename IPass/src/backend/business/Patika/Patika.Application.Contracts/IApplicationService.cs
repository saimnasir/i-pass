using Patika.Shared.Entities.Identity;
using System.Threading.Tasks;

namespace Patika.Application.Contracts
{
    public interface IApplicationService
    {        
        Task SetApplicationUserAsync(ApplicationUser user);
        ApplicationUser ApplicationUser { get; set; }
        // Task<TokenResultDto> GetTokenAsync(string token);
        // Task<ApplicationUserDto> GetApplicationUser(string token);
    }
}