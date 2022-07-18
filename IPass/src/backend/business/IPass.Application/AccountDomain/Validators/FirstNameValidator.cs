using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.Consts;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class FirstNameValidator : IFirstNameValidator
    {
      
        IRegexValidator RegexValidator { get; }
        public FirstNameValidator( IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        public async Task ValidateAsync(string name)
        {
            // min 2 length
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.GetMinLengthPattern(2),
                Exception = new FirstNameMinLengthException(),
            });

            // only letters
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.GetOnlyLettersPattern(),
                Exception = new FirstNameJustLettersException(),
            });

            // not start with "ğ,Ğ"
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.GetNotStartWithPattern("Ğğ"),
                Exception = new FirstNameStartsWithInvalidCharacterException(),
            });

            // Ad verisi içinde 3 veya daha fazla sesli harf yan yana bulunmamalıdır.           
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.GetVowelSequencialRepetitionPattern(),
                Exception = new FirstNameVowelSequencialRepetitionException(),
                MustMatch = true
            });

            // Ad verisi içinde 4 veya daha fazla sessiz harf yan yana bulunmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.GetConsonantSequencialRepetitionPattern(),
                Exception = new FirstNameConsonantSequencialRepetitionException(),
                MustMatch = true
            });

            // Ad verisi içinde aynı harften 3 veya daha fazlası yan yana bulunmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Input = name,
                Pattern = RegexPatterns.SequencialRepetitionPattern,
                Exception = new FirstNameLetterSequencialRepetitionException(),
                MustMatch = true
            });


        }
    }
}
