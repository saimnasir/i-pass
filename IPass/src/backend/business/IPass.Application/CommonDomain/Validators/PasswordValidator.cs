using FluentValidation;
using Patika.Shared.Consts;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Interfaces;
using Patika.Shared.DTO.Validators;
using System.Threading.Tasks;
using IPass.Application.Contracts.CommonDomain.Validators;

namespace IPass.Application.CommonDomain.Validators
{
    public class PasswordValidator : AbstractValidator<PasswordValidatorInput>, IPasswordValidator
    {
        public IRegexValidator RegexValidator { get; }
        public PasswordValidator(IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        async Task IPatikaValidator<PasswordValidatorInput>.ValidateAsync(PasswordValidatorInput input)
        {
            if (string.IsNullOrEmpty(input.Password))
                throw new PasswordRequiredException();

            if (string.IsNullOrEmpty(input.ConfirmPassword))
                throw new ConfirmPasswordRequiredException();

            if (!input.Password.Equals(input.ConfirmPassword))
                throw new ConfirmPasswordFailedException();

            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = input.Password,
                Pattern = RegexPatterns.Password,
                Exception = new PasswordFormatInvalidException()
            });

            await Task.FromResult(0);
        }

    }
}
