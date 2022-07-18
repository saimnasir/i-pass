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
    public class ProfileController : GenericApiController
    {
        IProfileAppService ProfileAppService { get; }

        public ProfileController(
            IProfileAppService profileAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            ProfileAppService = profileAppService;
        }
         
        [HttpGet]
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
    }
}
