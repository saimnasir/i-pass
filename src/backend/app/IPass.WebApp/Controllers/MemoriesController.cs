using IPass.Application;
using IPass.Application.Contracts.PasswordDomain;
using IPass.Shared.Consts;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using Patika.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.WebApp.Controllers
{
    [Route("api/[controller]/")]
    [Authorize(Policy = Consts.USER_POLICY)]
    public class MemoriesController : GenericApiController
    {
        IMemoryAppService MemoryAppService { get; }

        public MemoriesController(
            IMemoryAppService passwordAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            MemoryAppService = passwordAppService;
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<FinalResponseDTO<ListResponse<MemoryDto>>>> GetListAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortType sortType, [FromQuery] string searchText, bool? decode)
        {
            var input = new SearchMemoriesInputDto
            {
                SearchText = searchText,
                Page = page ?? 1,
                PageSize = pageSize ?? 20,
                Decode = decode ?? false,
                SortBy = sortBy,
                SortType = sortType
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await MemoryAppService.GetListAsync(input);
            });
        }


        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<object>>> AddAsync([FromBody] CreateMemoryInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryAppService.AddAsync(input);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> GetAsync(Guid id, [FromQuery] bool? decode)
        {
            var input = new GetMemoryByIdInputDto(id)
            {
                Decode = decode??false
            };

            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryAppService.GetAsync(input);
            });
        }


        // GET api/values
        [HttpGet("history/{id}")]
        public async Task<ActionResult<FinalResponseDTO<ListResponse<MemoryDto>>>> GetHistoriesAsync(Guid id, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortType sortType, [FromQuery] string searchText, bool? decode)
        {
            var input = new SearchMemoryHisyoriesInputDto
            {
                Id = id,
                SearchText = searchText,
                Page = page ?? 1,
                PageSize = pageSize ?? 20,
                Decode = decode ?? false,
                SortBy = sortBy,
                SortType = sortType
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await MemoryAppService.GetHistoriesAsync(input);
            });
        }

        [HttpPut]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateAsync([FromBody] MemoryDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await MemoryAppService.UpdateAsync(input);
            });
        }
    }
}
