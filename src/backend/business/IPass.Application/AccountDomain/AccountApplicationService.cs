using IPass.Application.Contracts.AccountDomain;
using Patika.Application;
using Patika.Shared.Entities;
using Patika.Shared.Services;
using System;
using System.Threading.Tasks;
using Patika.Shared.DTO.Identity;
using IPass.Application.Contracts.AccountDomain.Validators;
using Patika.Shared.Extensions;
using System.Text;
using IPass.Domain.CommonDomain.Repositories;
using Patika.Shared.Interfaces;
using Patika.Shared.Entities.Identity;
using IPass.Shared.Services;
using IPass.Shared.Extensions;
using IPass.Shared.DTO.Common;
using Patika.IdentityServer.Shared.Dto;
using Patika.Shared.DTO;
using Patika.Shared.Enums;
using IPass.Domain.CommonDomain.Entities;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.DTO.CommonDomain;

namespace IPass.Application.AccountDomain
{
    public class AccountApplicationService : ApplicationService, IAccountApplicationService
    {
        IUnitOfWorkHostWithInterface UnitOfWorkHostWithInterface { get; }
        IAccountApplicationServiceValidator AccountApplicationServiceValidator { get; }
        IUserRepository UserRepository { get; }

        public AccountApplicationService(
            IUnitOfWorkHostWithInterface unitOfWorkHostWithInterface,
            IAccountApplicationServiceValidator accountApplicationServiceValidator,
            IUserRepository userRepository,
            Configuration configuration,
            ILogWriter logWriter,
            ClientAuthenticationParams authenticationParams
            ) : base(configuration, new GeneralMappingProfile(), logWriter)
        {
            HttpClientService = new HttpClientService(configuration.GatewayUrl, authenticationParams);
            UnitOfWorkHostWithInterface = unitOfWorkHostWithInterface;
            AccountApplicationServiceValidator = accountApplicationServiceValidator;
            UserRepository = userRepository;
        }

        public async Task<TokenResultDto> BasicLoginAsync(BasicLoginInputDto input)
        {
            var userId = await UserRepository.GetIdByUserNameAsync(input.UserName);

            if (userId is null)
                throw new Exception("User Not Found");

            var res = await HttpClientService.HttpPostJson<TokenResultDto>("identity/connect/token", input);

            return res;
        }

        public async Task<TokenResultDto> BasicRegisterAsync(UserRegistrationInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var res = await HttpClientService.HttpPostJson<GeneralResponseDTO<TokenResultDto>>("identity/register", input);
                await LogWriter.AddCodeMileStoneLogAsync(input, "res: identity/register", GetType(), output: res);
                if (res.ResultCode != LogStatus.Success)
                {
                    await LogWriter.AddExceptionLogAsync(input, res.Exception, GetType());
                    throw res.Exception;
                }
                else
                {
                    ApplicationUser = Mapper.Map<ApplicationUser>(await GetApplicationInfoFromAuthServerAsync(res.Result));

                    await CreateUserAfterRegistration(new CreateUserAfterRegistrationInputDto
                    {
                        LogId = input.LogId,
                    });
                }

                return res.Result;
            });
        }

        public Task<TokenResultDto> ValidateAccountAsync(ValidateAccountInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResultDto> GetRefreshTokenAsync(RefreshTokenInputDto input)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUserDto> GetApplicationInfoFromAuthServerAsync(TokenResultDto res)
        {
            await HttpClientService.SetTokenAsync(res.Token);
            var userInfo = await HttpClientService.HttpGetAs<ApplicationUserDto>("identity/application-user");
            return userInfo;
        }

        public async Task CreateUserAfterRegistration(CreateUserAfterRegistrationInputDto input)
        {
            var id = new Guid(ApplicationUser.Id);
            var userExists = await UserRepository.GetByIdAsync(id);
            if (userExists == null)
            {
                var user = new User
                {
                    Id = new Guid(ApplicationUser.Id),
                    FirstName = ApplicationUser.FirstName,
                    LastName = ApplicationUser.LastName,
                    UserName = ApplicationUser.UserName,

                };
                await UserRepository.InsertOneAsync(user);
            }
        }
    }
}
