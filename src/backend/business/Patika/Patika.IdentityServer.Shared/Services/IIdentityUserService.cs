using Patika.IdentityServer.Shared.Dto;

namespace Patika.IdentityServer.Shared.Services
{
    public interface IIdentityUserService
    {
        Task CreateUserAsync(ApplicationUserInputDto input, string role = Consts.ANONYMOUS_ROLE);
        Task AddRoleToUserAsync(string id, string role);
        Task RemoveRoleFromUserAsync(string id, string role);
    }
}