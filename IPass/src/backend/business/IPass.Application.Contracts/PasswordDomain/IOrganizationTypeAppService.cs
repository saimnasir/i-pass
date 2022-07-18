using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IOrganizationTypeAppService : IApplicationService
    {
        Task<object> AddAsync(CreateOrganizationTypeInputDto input);
        Task<object> UpdateAsync(OrganizationTypeDto input);
        Task<SingleResponse<OrganizationTypeDto>> GetAsync(IdInputDto<Guid> input);
        Task<ListResponse<OrganizationTypeDto>> GetListAsync(SearchInputDto input);
    }
}
