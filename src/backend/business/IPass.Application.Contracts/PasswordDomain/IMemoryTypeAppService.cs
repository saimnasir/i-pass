using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IMemoryTypeAppService : IApplicationService
    {
        Task<object> AddAsync(CreateMemoryTypeInputDto input);
        Task<object> UpdateAsync(MemoryTypeDto input);
        Task<SingleResponse<MemoryTypeDto>> GetAsync(IdInputDto<Guid> input);
        Task<ListResponse<MemoryTypeDto>> GetListAsync(SearchInputDto input);
    }
}
