using IPass.Application.Contracts.AccountDomain.Validators;

namespace IPass.Application.AccountDomain.Validators
{
    public class AccountApplicationServiceValidator : IAccountApplicationServiceValidator
    {
        IPhoneNumberExistanceValidator PhoneNumberExistanceValidator { get; }
        IValidateActivationCodeValidator ValidateActivationCodeValidator { get; }
        IEmailValidator EmailValidator { get; }
        IFirstNameValidator FirstNameValidator { get; }
        ILastNameValidator LastNameValidator { get; }
        IBirthDateValidator BirthDateValidator { get; }
        ISendActivationCodeSmsValidator SendActivationCodeSmsValidator { get; }

        public AccountApplicationServiceValidator(
            IPhoneNumberExistanceValidator phoneNumberExistanceValidator,
            IValidateActivationCodeValidator validateActivationCodeValidator,
            IEmailValidator emailValidator,
            IFirstNameValidator firstNameValidator,
            ILastNameValidator lastNameValidator,
            IBirthDateValidator birthDateValidator,
            ISendActivationCodeSmsValidator sendActivationCodeSmsValidator)
        {
            PhoneNumberExistanceValidator = phoneNumberExistanceValidator;
            ValidateActivationCodeValidator = validateActivationCodeValidator;
            EmailValidator = emailValidator;
            FirstNameValidator = firstNameValidator;
            LastNameValidator = lastNameValidator;
            BirthDateValidator = birthDateValidator;
            SendActivationCodeSmsValidator = sendActivationCodeSmsValidator;
        }

      
		

    }
}
