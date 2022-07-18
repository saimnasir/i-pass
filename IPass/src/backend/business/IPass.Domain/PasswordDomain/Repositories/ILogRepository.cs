using Patika.Shared.Entities.Loggy.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace IPass.Domain.PasswordDomain.Repositories
{
    public interface ILogRepository : IGenericRepository<Log, Guid>
    {
        Task<LogDetail> AddDetail(Guid logId, LogDetail logDetail);
    }
}
