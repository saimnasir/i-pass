using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class SendActivationCodeSmsValidator : ISendActivationCodeSmsValidator
    {
        IPhoneNumberValidator PhoneNumberValidator { get; }       

        public SendActivationCodeSmsValidator(IPhoneNumberValidator phoneNumberValidator)
        {
            PhoneNumberValidator = phoneNumberValidator;
        }

        public async Task ValidateAsync(SendActivationCodeSmsValidatorInput input)
        {
            await PhoneNumberValidator.ValidateAsync(input.PhoneNumber);
         
            var now = DateTime.Now;
            if (input.ActivationCodeExpireDate > now)
            {
                var diffInSeconds = (int)(input.ActivationCodeExpireDate - now).TotalSeconds;
                throw new PreviousActivationCodeNotExpiredException(diffInSeconds);
            }
          
            await Task.CompletedTask;
        }
    }
}
