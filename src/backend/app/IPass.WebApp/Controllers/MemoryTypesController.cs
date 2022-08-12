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
    [Authorize(Roles = $"{Consts.USER_ROLE}")]
    public class MemoryTypesController : GenericApiController
    {
        IMemoryTypeAppService MemoryTypeAppService { get; }

        public MemoryTypesController(
            IMemoryTypeAppService passwordAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            MemoryTypeAppService = passwordAppService;
        }

        // GET api/values
        [HttpGet] 
        public async Task<ActionResult<FinalResponseDTO<ListResponse<MemoryTypeDto>>>> GetListAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string searchText)
        {
            var input = new SearchInputDto
            {
                SearchText = searchText,
                Page = page ?? 1,
                PageSize = pageSize ?? 20,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await MemoryTypeAppService.GetListAsync(input);
            });
        }

        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<object>>> AddAsync([FromBody] CreateMemoryTypeInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryTypeAppService.AddAsync(input);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> GetAsync( Guid id)
        {
            var input = new IdInputDto<Guid>(id);

            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryTypeAppService.GetAsync(input);
            });
        }

        [HttpPut]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateAsync([FromBody] MemoryTypeDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryTypeAppService.UpdateAsync(input);
            });
        }
    }
}
