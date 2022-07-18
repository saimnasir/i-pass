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
    public class EnvironmentTypesController : GenericApiController
    {
        IEnvironmentTypeAppService EnvironmentTypeAppService { get; }

        public EnvironmentTypesController(
            IEnvironmentTypeAppService passwordAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            EnvironmentTypeAppService = passwordAppService;
        }

        // GET api/values
        [HttpGet]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<ListResponse<EnvironmentTypeDto>>>> GetListAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string searchText)
        {
            var input = new SearchInputDto
            {
                SearchText = searchText,
                Page = page.HasValue ? page.Value : 1,
                PageSize = pageSize.HasValue ? pageSize.Value : 20,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await EnvironmentTypeAppService.GetListAsync(input);
            });
        }

        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<object>>> AddAsync([FromBody] CreateEnvironmentTypeInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await EnvironmentTypeAppService.AddAsync(input);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> GetAsync( Guid id)
        {
            var input = new IdInputDto<Guid>(id);

            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await EnvironmentTypeAppService.GetAsync(input);
            });
        }

        [HttpPut]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateAsync([FromBody] EnvironmentTypeDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await EnvironmentTypeAppService.UpdateAsync(input);
            });
        }
    }
}
