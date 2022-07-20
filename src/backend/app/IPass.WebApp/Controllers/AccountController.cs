using IPass.Application;
using IPass.Application.Contracts.AccountDomain;
using IPass.Application.Contracts.PasswordDomain;
using IPass.Shared.Consts;
using IPass.Shared.DTO.Common;
using IPass.Shared.DTO.CommonDomain;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patika.IdentityServer.Shared.Dto;
using Patika.Shared.DTO;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using System.Threading.Tasks;

namespace IPass.WebApp.Controllers
{
    [Route("api/[controller]/")]
    public class AccountController : GenericApiController
    {
        IAccountApplicationService AccountApplicationService { get; }
        IProfileAppService ProfileAppService { get; }

        public AccountController(
            IAccountApplicationService accountApplicationService,
            IProfileAppService profileAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            AccountApplicationService = accountApplicationService;
            ProfileAppService = profileAppService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> BasicLoginServicemanAsync(BasicLoginInputDto input)
        {
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await AccountApplicationService.BasicLoginAsync(input);
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> BasicRegisterAsync(UserRegistrationInputDto input)
        {
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await AccountApplicationService.BasicRegisterAsync(input);
            });
        }

        [HttpPost("otp/forgot-password")]
        public async Task<ActionResult<FinalResponseDTO<object>>> SendValidationCodeAsync(SendActivationCodeInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                //await AccountApplicationService.SendActivationCodeAsync(new SendActivationCodeInputDto { PhoneNumber = input.PhoneNumber, JobLogId = input.JobLogId });
                return true;
            });
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<FinalResponseDTO<object>>> ChangePasswordAsync(ResetPasswordInputDto input)
        {
            input.LogId = (await LogWriter.CreateLog(Configuration.ApplicationName)).Id.ToString();
            //var x = await AccountApplicationService.ResetForgottenPasswordAsync(input);
            //return x;
            return null;
        }

        [Route("otp/activation-code")]
        [HttpPost]
        [Authorize(Policy = Consts.USER_POLICY)]
        public async Task<ActionResult<FinalResponseDTO<object>>> SendActivationCodeSmsAsync()
        {
            var input = new SendActivationCodeInputDto
            {
                PhoneNumber = (await GetUser()).PhoneNumber
            };
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                AccountApplicationService.ApplicationUser = await GetUser();
                //await AccountApplicationService.SendActivationCodeAsync(input);
                return true;
            });
        }


        [Route("activation-code")]
        [HttpPost]
        [Authorize(Policy = Consts.USER_POLICY)]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> ValidateActivationCodeAsync(ValidateAccountInputDto input)
        {
            return await WithLoggingFinalResponse(input, async () =>
            {
                AccountApplicationService.ApplicationUser = await GetUser();
                return await AccountApplicationService.ValidateAccountAsync(input);
            });
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> GetRefreshTokenAsync(RefreshTokenInputDto input)
        {
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await AccountApplicationService.GetRefreshTokenAsync(input);
            });
        }

        [HttpGet("profile")]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<SingleResponse<ProfileDto>>>> GetProfileAsync()
        {
            var input = new DTO();
            return await WithLoggingFinalResponse(input, async () =>
            {
                ProfileAppService.ApplicationUser = await GetUser();
                return await ProfileAppService.GetProfileAsync(input);
            });
        }

        [HttpPut("profile")]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateProfileAsync(UserDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                ProfileAppService.ApplicationUser = await GetUser();
                await ProfileAppService.UpdateProfileAsync(input);
                return true;
            });
        }


        [HttpGet("google")]
        [AllowAnonymous]
        public IActionResult LoginWithGoogleAsync(bool allowToCommunicate)
        {
            return Redirect($"{Configuration.GatewayUrl}/identity/authorize/google-login?callback={Configuration.GatewayUrl}/api/account/ext-callback?allowToCommunicate={allowToCommunicate}");
        }

        [HttpGet("facebook")]
        [AllowAnonymous]
        public IActionResult LoginWithFacebookAsync(bool allowToCommunicate)
        {
            return Redirect($"{Configuration.GatewayUrl}/identity/authorize/facebook-login?callback={Configuration.GatewayUrl}/api/account/ext-callback?allowToCommunicate={allowToCommunicate}");
        }

        [HttpGet("ext-callback")]
        [AllowAnonymous]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> ServicemanExternalCallbackAsync(
         string t = null,
         bool allowToCommunicate = false,
         int e = 0,
         string r = "",
         bool acs = false,
         bool acv = false,
         bool pc = false)
        {
            var input = new CreateServicemanAfterRegistrationInputDto
            {
                AllowCommunication = allowToCommunicate,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                AccountApplicationService.ApplicationUser = await GetUser(t);
                await AccountApplicationService.CreateUserAfterRegistration(input);
                return new TokenResultDto
                {
                    Token = t,
                    ExpiresIn = e,
                    RefreshToken = r,
                    IsActivationCodeSent = acs,
                    IsActivationCodeValidated = acv,
                    IsProfileCompleted = pc,
                    IsContractsAccepted = false
                };
            });
        }

        /*

        [HttpGet("google")]
        [AllowAnonymous]
        public IActionResult LoginWithGoogleAsync(string callBack)
        {
            return Redirect($"{Configuration.GatewayUrl}/identity/authorize/google-login?callback={callBack}");
        }


        [HttpGet("external-token")]
        [AllowAnonymous]
        public async Task<ActionResult<FinalResponseDTO<TokenResultDto>>> GetUserTokenAsync(
           string t = null,
           bool allowToCommunicate = true,
           int e = 0,
           string r = "",
           bool acs = false,
           bool acv = false,
           bool pc = false)
        {
            var input = new CreateServicemanAfterRegistrationInputDto
            {
                AllowCommunication = allowToCommunicate,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                AccountApplicationService.ApplicationUser = await GetUser(t);
                await AccountApplicationService.CreateUserAfterRegistration(input);
                return new TokenResultDto
                {
                    Token = t,
                    ExpiresIn = e,
                    RefreshToken = r,
                    IsActivationCodeSent = acs,
                    IsActivationCodeValidated = acv,
                    IsProfileCompleted = pc,
                    IsContractsAccepted = false
                };
            });
        }
        */
    }
}
