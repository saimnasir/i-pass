using IPass.Domain.PasswordDomain.Repositories;
using IPass.Shared.Services;
using Patika.Shared.Entities.Loggy.Entities;
using Patika.Shared.Enums;
using System;
using System.Threading.Tasks;

namespace IPass.EFRepositories.Services
{
    public class LogWriter : ILogWriter
    {
        ILogRepository LogRepository { get; }

        public LogWriter(ILogRepository logRepository)
        {
            LogRepository = logRepository;
        }

        public async Task AddLogDetail(Guid logId, LogDetail detail)
        {
            var log = await LogRepository.GetByIdAsync(logId);
            if (log != null)
            {
                detail.LogId = log.Id;
                await LogRepository.AddDetail(log.Id, detail);
            }
        }

        public async Task<Log> CreateLog(string applicationName, LogStatus intialStatus = LogStatus.Started)
        {
            var log = new Log
            {
                StartDateTime = DateTime.Now,
                Status = intialStatus,
                ApplicationName = applicationName
            };

            await LogRepository.InsertOneAsync(log);

            return log;
        }

        public async Task FinishLog(Guid logId, LogStatus finalStatus = LogStatus.Success)
        {
            var log = await LogRepository.GetByIdAsync(logId);
            log.EndDateTime = DateTime.Now;
            log.Status = finalStatus;
            await LogRepository.UpdateOneAsync(log);
        }
    }
}
