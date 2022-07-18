using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using Patika.Shared.Consts;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class ValidateActivationCodeValidator : IValidateActivationCodeValidator
    {
        public IPhoneNumberValidator PhoneNumberValidator { get; }
        public IRegexValidator RegexValidator { get; }
        public ValidateActivationCodeValidator(IPhoneNumberValidator phoneNumberValidator, IRegexValidator regexValidator)
        {
            PhoneNumberValidator = phoneNumberValidator;
            RegexValidator = regexValidator;
        }

        public async Task ValidateAsync(ValidateActivationCodeValidatorInput input)
        {
            if(input.ActivationCode == string.Empty)
            {
                throw new Exception("Aktivasyon kodu boş geçilemez.");
            }
            //check activation code format
            await RegexValidator.ValidateAsync(new Patika.Shared.DTO.Validators.RegexValidatorInput
            {
                Exception = new ActivationCodeInvalidException(),
                Input = input.ActivationCode,
                Pattern = RegexPatterns.ActivationCode
            });

            // check phone number
            await PhoneNumberValidator.ValidateAsync(input.PhoneNumber);

            //check expire date
            var now = DateTime.Now;
            if (input.ActivationCodeExpireDate <= now)
            {
                var diffInSeconds = (int)(input.ActivationCodeExpireDate - now).TotalSeconds;
                throw new ActivationCodeTimeOutException(diffInSeconds);
            }
            // check try count
            if (input.ActivationCodeTryCount > input.ActivationCodeMaxTryCount)
            {
                var diffInSeconds = (int)(input.ActivationCodeExpireDate - now).TotalSeconds;
                throw new ActivationCodeTryCountExceedException(diffInSeconds, input.ActivationCodeTryCount + 1);
            }
            await Task.FromResult(0);
        }
    }
}
