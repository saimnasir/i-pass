using FluentValidation;
using Patika.Shared.Consts;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Interfaces;
using Patika.Shared.DTO.Validators;
using System.Threading.Tasks;
using IPass.Application.Contracts.CommonDomain.Validators;

namespace IPass.Application.CommonDomain.Validators
{
    public class PhoneNumberValidator : AbstractValidator<string>, IPhoneNumberValidator
    {
        public IRegexValidator RegexValidator { get; }
        public PhoneNumberValidator(IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        async Task IPatikaValidator<string>.ValidateAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new PhoneNumberRequiredException();

            phoneNumber = phoneNumber.Replace(" ", "");
            if (phoneNumber.Length != 10)
                throw new PhoneNumberInvalidLengthException();

            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = phoneNumber,
                Pattern = RegexPatterns.PhoneNumber,
                Exception = new PhoneNumberInvalidCharactersException()
            });

            await Task.FromResult(0);
        }


    }
}
