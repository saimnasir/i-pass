using IPass.Application;
using IPass.Application.Contracts.PasswordDomain;
using IPass.Shared.Consts;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using System.Threading.Tasks;

namespace IPass.WebApp.Controllers
{
    [Route("api/[controller]/")]
    public class PinCodesController : GenericApiController
    {
        IProfileAppService ProfileAppService { get; }

        public PinCodesController(
            IProfileAppService profileAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            ProfileAppService = profileAppService;
        }

        // POST api/pincodes/check
        [HttpPost("check")]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<SingleResponse<PinCodeDto>>>> CheckAsync([FromBody] CheckPinCodeInputDto input)
        {
            return await WithLoggingFinalResponse(input, async () =>
            {
                ProfileAppService.ApplicationUser = await GetUser();
                return await ProfileAppService.CheckPinCodeAsync(input);
            });
        }

        // POST api/pincodes/
        [HttpPost]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<SingleResponse<PinCodeDto>>>> CreateAsync([FromBody] CreatePinCodeInputDto input)
        { 
            return await WithLoggingFinalResponse(input, async () =>
            {
                ProfileAppService.ApplicationUser = await GetUser();
                return await ProfileAppService.CreatePinCodeAsync(input);
            });
        }
    }
}
