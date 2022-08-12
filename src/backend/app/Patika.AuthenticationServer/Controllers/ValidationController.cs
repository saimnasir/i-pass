using IPass.Application;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Patika.Shared.Entities;
using Patika.Shared.Enums;
using Patika.Shared.Exceptions;

namespace Patika.AuthenticationServer.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ValidationController : GenericApiController
	{
		IPhoneNumberExistanceValidator PhoneNumberExistanceValidator { get; }
        IUserNameExistanceValidator UserNameExistanceValidator { get; }

        public ValidationController(IPhoneNumberExistanceValidator phoneNumberExistanceValidator,
            IUserNameExistanceValidator userNameExistanceValidator,
			Configuration configuration,
			ILogWriter logWriter
			) : base(logWriter, configuration)
		{
		    PhoneNumberExistanceValidator = phoneNumberExistanceValidator;
            UserNameExistanceValidator = userNameExistanceValidator;
        }

		[HttpPost("~/identity/validate/is-phone-number-exists")]
		public async Task<IActionResult> IsPhoneNumberExistsAsync(PhoneNumberExistanceValidatorInput input)
		{
            try
            {
                await PhoneNumberExistanceValidator.ValidateAsync(input);
                return Json(new { Message = "", Result = true, ResultCode = $"{LogStatus.Success}", JobId = input.LogId ?? Guid.Empty.ToString() });
            }
            catch (BaseApplicationException ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new
                    {
                        code = ex.Code,
                        message = ex.Message,
                        innerException = ex.InnerException?.Message ?? "",
                        source = ex.Source
                    },
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId ?? Guid.Empty.ToString(),
                    result = new { }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new
                    {
                        code = new GeneralException(ex).Code,
                        message = ex.Message,
                        innerException = ex.InnerException?.Message ?? "",
                        source = ex.Source
                    },
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId,
                    result = new { }
                });
            }
        }	
        
        [HttpPost("~/identity/validate/is-user-name-exists")]
		public async Task<IActionResult> IsUserNameExistsAsync(UserNameExistanceValidatorInput input)
		{
            try
            {
                await UserNameExistanceValidator.ValidateAsync(input);
                return Json(new { Message = "", Result = true, ResultCode = $"{LogStatus.Success}", JobId = input.LogId ?? Guid.Empty.ToString() });
            }
            catch (BaseApplicationException ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new
                    {
                        code = ex.Code,
                        message = ex.Message,
                        innerException = ex.InnerException?.Message ?? "",
                        source = ex.Source
                    },
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId ?? Guid.Empty.ToString(),
                    result = new { }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new
                    {
                        code = new GeneralException(ex).Code,
                        message = ex.Message,
                        innerException = ex.InnerException?.Message ?? "",
                        source = ex.Source
                    },
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId,
                    result = new { }
                });
            }
        }
	}
}
