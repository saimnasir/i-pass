using IPass.Shared.DTO.Common;
using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.IdentityServer.Shared.Dto;
using Patika.Shared.DTO;
using Patika.Shared.DTO.Identity;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.AccountDomain
{
    public interface IAccountApplicationService : IApplicationService
    {
        Task<TokenResultDto> BasicLoginAsync(BasicLoginInputDto input);
        Task<TokenResultDto> BasicRegisterAsync(UserRegistrationInputDto input);
        Task<TokenResultDto> ValidateAccountAsync(ValidateAccountInputDto input);
        Task<TokenResultDto> GetRefreshTokenAsync(RefreshTokenInputDto input);
        Task CreateUserAfterRegistration(CreateUserAfterRegistrationInputDto input);
    }
}
