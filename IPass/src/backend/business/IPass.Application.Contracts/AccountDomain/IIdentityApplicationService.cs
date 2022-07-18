using IPass.Shared.DTO.Common;
using IPass.Shared.DTO.Identity;
using Patika.Application.Contracts;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.AccountDomain
{
    public interface IIdentityApplicationService : IApplicationService
    {
        Task<SendActivationCodeOutputResponse> SendAccountValidationSmsAsync(SendActivationCodeInputDto input);
        Task<ValidateAccountOutputResponse> ValidateAccountAsync(ValidateAccountInputDto input);
        //Task CreateUserProfileAsync(CreateUserProfileInputDto input, string token);
       // Task UpdateUserProfileAsync(UpdateUserProfileInputDto input, string token);
        Task<UserRegistrationOutputResponse> RegisterUserAsync(UserRegistrationInputDto input);
        Task<string> RegisterApplicationAsync(ApplicationRegistrationInputDto input);
        ApplicationUserDto ApplicationUserMapToDtoAsync(ApplicationUser user);
        Task ResetPassword(ResetPasswordInputDto input);
        Task<ApplicationUser> GetByPhoneNumberAsync(string phoneNumber);
        Task AddRoleToUserAsync(ApplicationUser user, params string[] roles);
    }
}
