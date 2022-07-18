﻿using IPass.Application;
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
    public class OrganizationTypesController : GenericApiController
    {
        IOrganizationTypeAppService OrganizationTypeAppService { get; }

        public OrganizationTypesController(
            IOrganizationTypeAppService passwordAppService,
            ILogWriter logWriter,
            Configuration configuration) : base(logWriter, configuration)
        {
            OrganizationTypeAppService = passwordAppService;
        }

        // GET api/values
        [HttpGet]
        [Authorize(Roles = $"{Consts.USER_ROLE}")]
        public async Task<ActionResult<FinalResponseDTO<ListResponse<OrganizationTypeDto>>>> GetListAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string searchText)
        {
            var input = new SearchInputDto
            {
                SearchText = searchText,
                Page = page.HasValue ? page.Value : 1,
                PageSize = pageSize.HasValue ? pageSize.Value : 20,
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await OrganizationTypeAppService.GetListAsync(input);
            });
        }

        [HttpPost]
        public async Task<ActionResult<FinalResponseDTO<object>>> AddAsync([FromBody] CreateOrganizationTypeInputDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationTypeAppService.AddAsync(input);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponseDTO<object>>> GetAsync( Guid id)
        {
            var input = new IdInputDto<Guid>(id);

            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationTypeAppService.GetAsync(input);
            });
        }

        [HttpPut]
        public async Task<ActionResult<FinalResponseDTO<object>>> UpdateAsync([FromBody] OrganizationTypeDto input)
        {
            return await WithLoggingFinalResponse<object>(input, async () =>
            {
                return await OrganizationTypeAppService.UpdateAsync(input);
            });
        }
    }
}