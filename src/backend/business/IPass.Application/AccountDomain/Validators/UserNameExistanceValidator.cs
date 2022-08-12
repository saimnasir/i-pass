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
    public class UserNameExistanceValidator : AbstractValidator<UserNameExistanceValidatorInput>, IUserNameExistanceValidator
    {
        public UserManager<ApplicationUser> UserManager { get; }
        IUserNameValidator UserNameValidator { get; }

        public UserNameExistanceValidator(UserManager<ApplicationUser> userManager, IUserNameValidator userNameValidator)
        {
            UserManager = userManager;
            UserNameValidator = userNameValidator;
        }

        public async Task ValidateAsync(UserNameExistanceValidatorInput input)
        {
            await UserNameValidator.ValidateAsync(input.UserName);

            var userName = input.UserName.Replace(" ", "");

            var isExists = await Task.FromResult(UserManager.Users.Any(u => u.UserName == userName && (input.Email == null || input.Email != u.Email)));
            if (isExists)
            {
                throw new UserNameAlreadyUsedException();
            } 
        }
    }
}
