using FluentValidation;
using Patika.Shared.Interfaces;
using Patika.Shared.DTO.Validators;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IPass.Application.Contracts.CommonDomain.Validators;

namespace IPass.Application.CommonDomain.Validators
{
    public class RegexValidator : AbstractValidator<RegexValidatorInput>, IRegexValidator
    {
        async Task IPatikaValidator<RegexValidatorInput>.ValidateAsync(RegexValidatorInput input)
        {
            if(input.Input == null)
                input.Input = string.Empty;

            var match = Regex.Match(input.Input, input.Pattern) 
                ?? throw new System.Exception("RegexMatchTimeoutException");
            if (match.Success == input.MustMatch)
            {
                throw input.Exception;
            }
            await Task.FromResult(0);
        }
    }
}
