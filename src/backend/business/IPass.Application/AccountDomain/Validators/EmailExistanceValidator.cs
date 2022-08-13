using FluentValidation;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using Microsoft.AspNetCore.Identity;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain;
using System.Linq;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class EmailExistanceValidator : AbstractValidator<string>, IEmailExistanceValidator
    {
        public UserManager<ApplicationUser> UserManager { get; }
        IEmailValidator EmailValidator { get; }

        public EmailExistanceValidator(UserManager<ApplicationUser> userManager, IEmailValidator emailValidator)
        {
            UserManager = userManager;
            EmailValidator = emailValidator;
        }

        public async Task ValidateAsync(EmailExistanceValidatorInput input)
        {
            await EmailValidator.ValidateAsync(input.Email); 

            var isExists = await Task.FromResult(UserManager.Users.Any(u => u.Email == input.Email));
            if (isExists)
            {
                throw new EmailAlreadyUsedException();
            } 
        }
    }
}
