using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Patika.IdentityServer.Shared.Services
{
    public interface IIntiateServer
    {
        Task Intiate(IServiceCollection serviceProvider, PasswordOptions? passwordOptions = null);
    }
}