using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using IPass.Shared.Services;
using Patika.Application;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using Patika.Shared.Enums;
using Patika.Shared.Extensions;
using Patika.Shared.Services;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.Services
{
	public class PhoneNumberExistanceValidatorOverHttp : ApplicationService, IPhoneNumberExistanceValidator
	{
		public PhoneNumberExistanceValidatorOverHttp(
			Configuration configuration,
			ILogWriter logWriter,
			ClientAuthenticationParams authenticationParams) : base(logWriter, configuration)
		{
			HttpClientService = new HttpClientService(configuration.AuthServerUrl, authenticationParams);
		}

		public async Task ValidateAsync(PhoneNumberExistanceValidatorInput input)
		{
			await HttpClientService.AcquireTokenAsync();
			var res = await HttpClientService.HttpPostJson<GeneralResponseDTO<object>>("identity/validate/is-phone-number-exists", input);
			if (res.ResultCode != LogStatus.Success)
			{
				throw res.ToBaseException();
			}
		}
	}
}