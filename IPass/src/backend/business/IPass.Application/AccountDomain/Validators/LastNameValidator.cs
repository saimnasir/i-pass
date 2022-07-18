using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.Consts;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class LastNameValidator : ILastNameValidator
    {
        IRegexValidator RegexValidator { get; }
        public LastNameValidator(IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        public async Task ValidateAsync(string lastName)
        {
            // min 2 length
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern = RegexPatterns.GetMinLengthPattern(2),
                Exception = new LastNameMinLengthException(),
            });

            // only letters
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern =   RegexPatterns.GetOnlyLettersPattern(),
                Exception = new LastNameJustLettersException(),
            });

            // not start with "ğ,Ğ"
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern = RegexPatterns.GetNotStartWithPattern("Ğğ"),
                Exception = new LastNameStartsWithInvalidCharacterException(),
            });

            // Ad verisi içinde 3 veya daha fazla sesli harf yan yana bulunmamalıdır.
            // @"\b(?=[a-z]*[aeiou]{3})[a-z]+\b"
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern = RegexPatterns.GetVowelSequencialRepetitionPattern(),
                Exception = new LastNameVowelSequencialRepetitionException(),
                MustMatch = true
            });

            // Ad verisi içinde 4 veya daha fazla sessiz harf yan yana bulunmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern = RegexPatterns.GetConsonantSequencialRepetitionPattern(),
                Exception = new LastNameConsonantSequencialRepetitionException(),
                MustMatch = true
            });

            // Ad verisi içinde aynı harften 3 veya daha fazlası yan yana bulunmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = lastName,
                Pattern = RegexPatterns.SequencialRepetitionPattern,
                Exception = new LastNameLetterSequencialRepetitionException(),
                MustMatch = true
            });

        }
    }
}
