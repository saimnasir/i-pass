using IPass.Application;
using IPass.Application.Contracts.PasswordDomain;
using IPass.Shared.Consts;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.WebApp.Controllers
{
    [Route("api/[controller]/")]
    public class OrganizationsController : GenericApiController
    {
        IOrganizationAppService OrganizationAppService { get; }

        public OrganizationsController(
            IOrganizationAppService passwordAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            OrganizationAppService = passwordAppService;
        }

        // GET api/values
        [HttpGet]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<ListResponse<OrganizationDto>>>> GetListAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string searchText)
        {
            var input = new SearchInputDto
            {
                SearchText = searchText,
                Page = page.HasValue ? page.Value : 1,
                PageSize = pageSize.HasValue ? pageSize.Value : 20,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await OrganizationAppService.GetListAsync(input);
            });
        }

        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<object>>> AddAsync([FromBody] CreateOrganizationInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationAppService.AddAsync(input);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> GetAsync( Guid id)
        {
            var input = new IdInputDto<Guid>(id);

            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationAppService.GetAsync(input);
            });
        }

        [HttpPut]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateAsync([FromBody] OrganizationDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationAppService.UpdateAsync(input);
            });
        }
    }
}
