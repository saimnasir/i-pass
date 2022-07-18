using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IOrganizationAppService : IApplicationService
    {
        Task<object> AddAsync(CreateOrganizationInputDto input);
        Task<object> UpdateAsync(OrganizationDto input);
        Task<SingleResponse<OrganizationDto>> GetAsync(IdInputDto<Guid> input);
        Task<ListResponse<OrganizationDto>> GetListAsync(SearchInputDto input);
    }
}
