using IPass.Application;
using IPass.Application.Contracts.AccountDomain;
using IPass.Shared.DTO.Common;
using IPass.Shared.DTO.Identity;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared.DTO;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Identity.Repositories;

namespace Patika.AuthenticationServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IdentityController : GenericApiController
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public IIdentityApplicationService IdentityApplicationService { get; }
        public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }

        public IdentityController(
            ILogWriter logWriter,
            Configuration configuration, 
            UserManager<ApplicationUser> userManager, 
            IIdentityApplicationService identityApplicationService,
            IUserRefreshTokenRepository userRefreshTokenRepository) : base(logWriter, configuration)
        {
            UserManager = userManager;
            IdentityApplicationService = identityApplicationService;
            UserRefreshTokenRepository = userRefreshTokenRepository;
        }


        [Route("identity/validate-account")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponseDTO<TokenResultDto>>> ValidateAccountAsync(ValidateAccountInputDto input)
        {
            return await WithLogging(input, async () =>
            {
                var response = await IdentityApplicationService.ValidateAccountAsync(input);
                if (response.IsSuccess) {
                    var user = await IdentityApplicationService.GetByPhoneNumberAsync(input.PhoneNumber);
                    var token = await AuthorizationController.MakeTokenAsync(user, UserManager, Configuration);
                    await AuthorizationController.SetUserRefreshToken(UserRefreshTokenRepository, user, token, Configuration);
                    return token;
                }
                throw new UserNotFoundException();
            });
        }


        [Route("identity/send-activation-code-sms")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponseDTO<SendActivationCodeOutputResponse>>> SendActivationCodeSmsAsync(SendActivationCodeInputDto input)
        {
            return await WithLogging(input, async () =>
            {
                var response = await IdentityApplicationService.SendAccountValidationSmsAsync(input);

                return response;
            });
        }

        //[Route("identity/update-profile")]
        //[HttpPost]
        //public async Task<ActionResult<GeneralResponseDTO<object>>> UpdateUserProfile(UpdateUserProfileInputDto input)
        //{
        //    return await WithLogging<object>(input, async () =>
        //    {
        //        await IdentityApplicationService.UpdateUserProfileAsync(input, GetToken());
        //        return true;
        //    });
        //}
    }
}
