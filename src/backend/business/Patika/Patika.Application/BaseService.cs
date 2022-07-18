using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Patika.Shared.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Patika.Application
{
    public abstract class BaseService
    {
        public ILogWriter LogWriter { get; }
        public Configuration Configuration { get; }



        public BaseService(ILogWriter logger, Configuration configuration)
        {
            LogWriter = logger;
            Configuration = configuration;
        }

        public async Task<T> WithLogging<T>(IDTO input, Type type, Func<Task<T>> tryPart, [CallerMemberName] string method = "") where T : class
        {
            await LogWriter.AddMethodStartLogAsync(input, type, input, callerName: method);
            var res = await tryPart();
            await LogWriter.AddMethodEndLogAsync(input, type, input, output: res, callerName: method);
            return res;
        }
        public async Task WithLogging(IDTO input, Type type, Func<Task> tryPart, [CallerMemberName] string method = "")
        {
            await LogWriter.AddMethodStartLogAsync(input, type, input, callerName: method);
            await tryPart();
            await LogWriter.AddMethodEndLogAsync(input, type, input, callerName: method);
        } 
    }
}