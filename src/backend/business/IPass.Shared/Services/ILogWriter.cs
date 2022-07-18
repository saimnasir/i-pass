using Patika.Shared.Entities.Loggy.Entities;
using Patika.Shared.Enums;
using System;
using System.Threading.Tasks;

namespace IPass.Shared.Services
{
    public interface ILogWriter
    {
        Task<Log> CreateLog(string applicationName, LogStatus intialStatus = LogStatus.Started);
        Task AddLogDetail(Guid logId, LogDetail logDetail);
        Task FinishLog(Guid logId, LogStatus finalStatus = LogStatus.Success);
    }
}
