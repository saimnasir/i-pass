using Patika.Shared.Entities.Loggy.Entities;
using Patika.Shared.Enums;
using System;
using System.Threading.Tasks;

namespace IPass.Shared.Services
{
    public class NullLogWriter : ILogWriter
    {
        public Task AddLogDetail(Guid jobId, LogDetail detail)
            => Task.CompletedTask;

        public Task<Log> CreateLog(string applicationName, LogStatus intialStatus = LogStatus.Started)
            => Task.FromResult(new Log() { });

        public Task FinishLog(Guid jobId, LogStatus finalStatus = LogStatus.Success)
            => Task.CompletedTask;
    }
}