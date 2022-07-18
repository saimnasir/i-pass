using FluentValidation;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Interfaces;
using System.Threading.Tasks;

namespace IPass.Application.CommonDomain.Validators
{
    public class AccountValidationValidator : AbstractValidator<ApplicationUser>, IAccountValidationValidator
    {
        async Task IPatikaValidator<ApplicationUser>.ValidateAsync(ApplicationUser user)
        {
            if (!user.PhoneNumberConfirmed) 
                throw new AccountNotVerifiedException();
            
            if (!user.IsActivationCodeValidated)
                throw new AccountNotVerifiedException();
           
            await Task.FromResult(0);
        }
    }
}