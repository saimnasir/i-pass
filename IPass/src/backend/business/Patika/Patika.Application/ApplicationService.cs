using AutoMapper;
using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Patika.Application.Contracts;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain; 
using Patika.Shared.Interfaces;
using Patika.Shared.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Patika.Application
{
    public abstract class ApplicationService : BaseService, IApplicationService
    {
        MappingProfile MappingProfile { get; set; }

        protected IMapper Mapper => MappingProfile.Mapper;
        protected HttpClientService HttpClientService { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ApplicationService(Configuration configuration) : this(configuration, new GeneralMappingProfile(), new NullLogWriter())
        {
        }

        public ApplicationService(ILogWriter logWriter, Configuration configuration) : this(configuration, new GeneralMappingProfile(), logWriter)
        { }

        public Task SetApplicationUserAsync(ApplicationUser user)
        {
            ApplicationUser = user;
            return Task.CompletedTask;
        }

        public ApplicationService(
            Configuration configuration,
            MappingProfile mappingProfile,
            ILogWriter logWriter) : base(logWriter, configuration)
        {
            MappingProfile = mappingProfile;
            HttpClientService = new HttpClientService(configuration.GatewayUrl);
        }

        protected async Task AddCodeMileStoneLogAsync(IDTO dto, string message, object input = null, object output = null, [CallerMemberName] string callerName = "")
        {
            await LogWriter.AddCodeMileStoneLogAsync(dto, message, GetType(), input, output, callerName);
        }

        public T MapTo<T, S>(S from) => Mapper.Map<S, T>(from);
        public T MapTo<T>(object from) => Mapper.Map<T>(from);


        public async Task<TokenResultDto> GetTokenAsync(string token)
        {
            try
            {
                await HttpClientService.SetTokenAsync(token);
                var res = await HttpClientService.HttpGetAs<TokenResultDto>("identity/token");
                return res;
            }
            catch (Exception)
            {
                // add log
                throw;
            }
        }

        public async Task<ApplicationUserDto> GetApplicationUser(string token)
        {
            try
            {
                await HttpClientService.SetTokenAsync(token);
                var res = await HttpClientService.HttpGetAs<ApplicationUserDto>("identity/application-user");
                return res;
            }
            catch (Exception)
            {
                // add log
                throw new UserNotFoundException();
            }
        }
    }
}