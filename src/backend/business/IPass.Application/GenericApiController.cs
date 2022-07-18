using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared;
using Patika.Shared.DTO;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Enums;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Extensions;
using Patika.Shared.Interfaces;
using Patika.Shared.Services;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IPass.Application
{
    [ApiController]
    public abstract class GenericApiController : Controller
    {
        protected HttpClientService HttpClientService { get; set; }

        protected ILogWriter LogWriter { get; }
        protected Configuration Configuration { get; }
        SharedMappingProfile SharedMappingProfile;

        public GenericApiController(ILogWriter logWriter, Configuration configuration)
        {
            LogWriter = logWriter;
            Configuration = configuration;
            HttpClientService = new HttpClientService(configuration.GatewayUrl);
            SharedMappingProfile = new SharedMappingProfile();
        }

        protected async Task<ActionResult<GeneralResponseDTO<T>>> WithLogging<T>(IDTO input, Func<Task<T>> tryPart, Func<IDTO, Task> catchPart = null, Func<GeneralResponseDTO<T>, Task> catchReturn = null, [CallerMemberName] string callerName = "") where T : class
        {
            var logId = string.IsNullOrEmpty(input.LogId) || input.LogId == Guid.Empty.ToString() ?
                (await LogWriter.CreateLog(LogWriterExtensions.ApplicationName)).Id
                : new Guid(input.LogId);
            input.LogId = logId.ToString();

            try
            {
                var res = await tryPart();
                await LogWriter.FinishLog(logId);
                return GeneralResponseDTO<T>.SuccessResponse(res, jobId: logId);
            }
            catch (Exception ex)
            {
                await LogWriter.AddExceptionLogAsync(input, ex, GetType(), callerName);
                await LogWriter.FinishLog(logId, LogStatus.Exception);
                if (catchPart != null)
                {
                    await catchPart(input);
                }
                var res = ex.ToExceptionGeneralResponse<T>(logId);
                if (catchReturn != null)
                {
                    await catchReturn(res.Value);
                }
                return res;
            }
        }

        protected async Task<ActionResult<FinalResponseDTO<T>>> WithLoggingFinalResponse<T>(IDTO input, Func<Task<T>> tryPart, Func<IDTO, Task> catchPart = null, Func<GeneralResponseDTO<T>, Task> catchReturn = null, [CallerMemberName] string callerName = "") where T : class
        {
            bool isValid = Guid.TryParse(input.LogId, out _);
            if (!isValid)
                input.LogId = "";
            var logId = string.IsNullOrEmpty(input.LogId) || input.LogId == Guid.Empty.ToString() ?
                (await LogWriter.CreateLog(LogWriterExtensions.ApplicationName)).Id
                : new Guid(input.LogId);
            input.LogId = logId.ToString();

            try
            {
                var res = await tryPart();
                await LogWriter.FinishLog(logId);
                var x = new FinalResponseDTO<T>(res)
                {
                    JobId = logId
                };
                return new ActionResult<FinalResponseDTO<T>>(x);
            }
            catch (Exception ex)
            {
                await LogWriter.AddExceptionLogAsync(input, ex, GetType(), callerName);
                await LogWriter.FinishLog(logId, LogStatus.Exception);
                if (catchPart != null)
                {
                    await catchPart(input);
                }
                var res = ex.ToExceptionGeneralResponse<T>(logId);
                if (catchReturn != null)
                {
                    await catchReturn(res.Value);
                }
                return new ActionResult<FinalResponseDTO<T>>(new FinalResponseDTO<T>(res.Value));
            }
        }
        protected string GetToken()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString();
                return token;
            }
            catch
            {
                throw new TokenInvalidException();
            }
        }

        protected async Task<ApplicationUser> GetUser()
        {
            return await GetUser(GetToken());
        }

        protected string GetUserId() => User.FindFirstValue(ClaimTypes.Name);

        protected async Task<ApplicationUser> GetUser(string token)
        {
            try
            {
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                //check redis for user
                // user found => return user;
                //user NOT found => getuser
                if (string.IsNullOrEmpty(token))
                {
                    throw new TokenInvalidException();
                }
                await HttpClientService.SetTokenAsync(token);
                var res = await HttpClientService.HttpGetAs<ApplicationUserDto>("identity/application-user");
                var user = SharedMappingProfile.Mapper.Map<ApplicationUser>(res);
                // add user redis
                return user;
            }
            catch
            {
                throw new TokenInvalidException();
            }
        }

        protected bool HasRole(string role)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value.Equals(role, StringComparison.OrdinalIgnoreCase));
            return claim != null;
        }

    }
}
