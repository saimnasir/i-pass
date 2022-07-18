using Patika.IdentityServer.Shared.Dto;

namespace Patika.IdentityServer.Shared.Services
{
    public interface IIdentityScopeService
    {
        Task<IEnumerable<IdentityScopeOutputDto>> GetAllAsync();
        Task<IEnumerable<string>> GetAllNamesAsync();
        Task AddScopeAsync(IdentityScopeInputDto input);
        Task AddResourceIntoScopeAsync(string scopeId,string resource);
        Task RemoveScopeByIdAsync(string scopeId);
        Task RemoveScopeByNameAsync(string name);
        Task UpdateScopeAsync(IdentityScopeInputDto input);
    }
}