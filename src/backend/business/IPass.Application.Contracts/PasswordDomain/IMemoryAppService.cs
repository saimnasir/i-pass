using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IMemoryAppService : IApplicationService
    {
        Task<object> AddAsync(CreateMemoryInputDto input);
        Task<object> UpdateAsync(MemoryDto input);
        Task<SingleResponse<MemoryDto>> GetAsync(GetMemoryByIdInputDto input);
        Task<ListResponse<MemoryDto>> GetListAsync(SearchMemoriesInputDto input);
        Task<ListResponse<MemoryDto>> GetHistoriesAsync(SearchMemoryHisyoriesInputDto input);
    }
}
