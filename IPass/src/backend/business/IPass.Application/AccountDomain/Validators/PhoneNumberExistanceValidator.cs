using FluentValidation;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Shared.DTO.Identity.Validators;
using Microsoft.AspNetCore.Identity;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain;
using System.Linq;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class PhoneNumberExistanceValidator : AbstractValidator<PhoneNumberExistanceValidatorInput>, IPhoneNumberExistanceValidator
    {
        public UserManager<ApplicationUser> UserManager { get; }
        IPhoneNumberValidator PhoneNumberValidator { get; }

        public PhoneNumberExistanceValidator(UserManager<ApplicationUser> userManager, IPhoneNumberValidator phoneNumberValidator)
        {
            UserManager = userManager;
            PhoneNumberValidator = phoneNumberValidator;
        }

        public async Task ValidateAsync(PhoneNumberExistanceValidatorInput input)
        {
            await PhoneNumberValidator.ValidateAsync(input.PhoneNumber);

            var phoneNumber = input.PhoneNumber.Replace(" ", "");

            var isExists = await Task.FromResult(UserManager.Users.Any(u => u.PhoneNumber == phoneNumber && (input.Email == null || input.Email != u.Email)));
            if (isExists)
            {
                throw new PhoneAlreadyUsedException();
            } 
        }
    }
}
