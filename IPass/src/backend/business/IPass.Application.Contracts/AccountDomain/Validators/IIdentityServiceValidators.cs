using IPass.Shared.DTO.Identity;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.AccountDomain.Validators
{
    public interface IIdentityServiceValidators
    {
        Task ValidateRegistrationInputAsync(UserRegistrationInputDto input);
        Task ValidateUpdateUserProfileInputAsync(UpdateUserProfileInputDto input, ApplicationUser user);
    }
}