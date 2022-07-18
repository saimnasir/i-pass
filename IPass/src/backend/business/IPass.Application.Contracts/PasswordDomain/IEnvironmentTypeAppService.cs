using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IEnvironmentTypeAppService : IApplicationService
    {
        Task<object> AddAsync(CreateEnvironmentTypeInputDto input);
        Task<object> UpdateAsync(EnvironmentTypeDto input);
        Task<SingleResponse<EnvironmentTypeDto>> GetAsync(IdInputDto<Guid> input);
        Task<ListResponse<EnvironmentTypeDto>> GetListAsync(SearchInputDto input);
    }
}
