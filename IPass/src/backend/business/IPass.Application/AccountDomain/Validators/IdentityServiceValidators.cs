using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Shared.DTO.Identity;
using IPass.Shared.DTO.Identity.Validators;
using Patika.Shared.DTO.Identity;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class IdentityServiceValidators : IIdentityServiceValidators
    {
        Configuration Configuration { get; }
        ISendActivationCodeSmsValidator SendActivationCodeSmsValidator { get; }
        IPhoneNumberValidator PhoneNumberValidator { get; }
        IPasswordValidator PasswordValidator { get; }
        IPhoneNumberExistanceValidator PhoneNumberExistanceValidator { get; }
        IValidateActivationCodeValidator ValidateActivationCodeValidator { get; }
        IEmailValidator EmailValidator { get; }
        IFirstNameValidator FirstNameValidator { get; }
        ILastNameValidator LastNameValidator { get; }
        IProfileAlreadyCompletedValidator ProfileAlreadyCompletedValidator { get; }

        public IdentityServiceValidators(
            Configuration configuration,
            ISendActivationCodeSmsValidator sendActivationCodeSmsValidator,
            IPhoneNumberValidator phoneNumberValidator,
            IPasswordValidator passwordValidator,
            IPhoneNumberExistanceValidator phoneNumberExistanceValidator,
            IValidateActivationCodeValidator validateActivationCodeValidator,
            IEmailValidator emailValidator,
            IFirstNameValidator firstNameValidator,
            ILastNameValidator lastNameValidator,
            IProfileAlreadyCompletedValidator profileAlreadyCompletedValidator)
        {
            Configuration = configuration;
            SendActivationCodeSmsValidator = sendActivationCodeSmsValidator;
            PhoneNumberValidator = phoneNumberValidator;
            PasswordValidator = passwordValidator;
            PhoneNumberExistanceValidator = phoneNumberExistanceValidator;
            ValidateActivationCodeValidator = validateActivationCodeValidator;
            EmailValidator = emailValidator;
            FirstNameValidator = firstNameValidator;
            LastNameValidator = lastNameValidator;
            ProfileAlreadyCompletedValidator = profileAlreadyCompletedValidator;
        }

      

        public async Task ValidateRegistrationInputAsync(UserRegistrationInputDto input)
        {
            await PhoneNumberValidator.ValidateAsync(input.PhoneNumber);

            await PasswordValidator.ValidateAsync(new PasswordValidatorInput
            {
                Password = input.Password,
                ConfirmPassword = input.ConfirmPassword,
            });


            await PhoneNumberExistanceValidator.ValidateAsync(new PhoneNumberExistanceValidatorInput
            {
                PhoneNumber = input.PhoneNumber
            });
        }


        public async Task ValidateUpdateUserProfileInputAsync(UpdateUserProfileInputDto input, ApplicationUser user)
        {
            await ProfileAlreadyCompletedValidator.ValidateAsync(user.IsProfileCompleted);
            await EmailValidator.ValidateAsync(input.Email);
            await FirstNameValidator.ValidateAsync(input.FirstName);
            await LastNameValidator.ValidateAsync(input.LastName);
        }

     
    }

}
