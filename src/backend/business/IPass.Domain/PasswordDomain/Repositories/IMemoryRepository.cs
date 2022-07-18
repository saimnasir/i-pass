using IPass.Domain.PasswordDomain.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPass.Domain.PasswordDomain.Repositories
{
    public interface IMemoryRepository : IGenericRepository<Memory, Guid> 
    {
        Task<IEnumerable<Memory>> GetHistoriesAsync(Guid id);
    }
}
