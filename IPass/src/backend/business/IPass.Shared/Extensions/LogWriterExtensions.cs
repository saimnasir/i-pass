using IPass.Shared.Services;
using Patika.Shared.DTO;
using Patika.Shared.Entities.Loggy.Entities;
using Patika.Shared.Enums;
using Patika.Shared.Extensions;
using Patika.Shared.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IPass.Shared.Extensions
{
    public static class LogWriterExtensions
    {
        public static string ApplicationName { get; set; }
        public static async Task AddCodeMileStoneLogAsync(this ILogWriter Logger, IDTO dto, string message, Type type, object input = null, object output = null, [CallerMemberName] string callerName = "", bool writeToConsole = true)
        {
            await CreateLogIfNotCreated(Logger, dto);
            await Logger.AddLogDetail(GetLogId(dto), new LogDetail
            {
                Module = type.FullName,
                Method = callerName,
                Description = message,
                InputAsJson = input?.ToJson() ?? "",
                OutputAsJson = output?.ToJson() ?? "",
                LogType = LogType.CodeMilestone
            });
            if (writeToConsole)
            {
                Console.WriteLine(message);
            }
        }

        private static Guid GetLogId(IDTO dto)
        {
            return new Guid(dto.LogId);
        }

        public static async Task AddMethodStartLogAsync(this ILogWriter Logger, IDTO dto, Type type, object input = null, object output = null, [CallerMemberName] string callerName = "")
        {
            await Logger.AddCodeMileStoneLogAsync(dto, "START", type, input, output, callerName, writeToConsole: false);
        }

        public static async Task AddMethodEndLogAsync(this ILogWriter Logger, IDTO dto, Type type, object input = null, object output = null, [CallerMemberName] string callerName = "")
        {
            await Logger.AddCodeMileStoneLogAsync(dto, "END", type, input, output, callerName, writeToConsole: false);
        }

        public static async Task AddWarningLogAsync(this ILogWriter Logger, IDTO dto, string message, Type type, object input = null, object output = null, [CallerMemberName] string callerName = "")
        {
            await CreateLogIfNotCreated(Logger, dto);
            await Logger.AddLogDetail(GetLogId(dto), new LogDetail
            {
                Module = type.FullName,
                Method = callerName,
                Description = message,
                InputAsJson = input?.ToJson() ?? "",
                OutputAsJson = output?.ToJson() ?? "",
                LogType = LogType.Warning
            });
            Console.WriteLine(message);
        }

        private static async Task CreateLogIfNotCreated(ILogWriter Logger, IDTO dto)
        {
            if ( string.IsNullOrEmpty(dto.LogId))
            {
                dto.LogId = (await Logger.CreateLog(ApplicationName)).Id.ToString();
            }
        }


        public static async Task AddExceptionLogAsync(this ILogWriter Logger, IDTO dto, Exception ex, Type type, [CallerMemberName] string callerName = "")
        {
            var exAsResponse = (GeneralResponseDTO<object>)ex;
            await CreateLogIfNotCreated(Logger, dto);
            var output = new
            {
                ex.Message,
                InnerMessage = ex.InnerException?.Message,
                InnerInnerMessage = ex.InnerException?.InnerException?.Message,
                ex.StackTrace
            }.ToJson();
            await Logger.AddLogDetail(GetLogId(dto), new LogDetail
            {
                Module = type.FullName,
                Method = callerName,
                Description = $"{exAsResponse.Exception?.Code ?? "[No Exception Code]"} - {exAsResponse.Exception?.Message ?? "[No message]"}",
                InputAsJson = dto?.ToJson() ?? "",
                OutputAsJson = output,
                LogType = LogType.Exception
            });
            Console.WriteLine(output);
        }
    }
}
