using IPass.Shared.DTO.Identity.Validators;
using Patika.Shared.Interfaces;

namespace IPass.Application.Contracts.AccountDomain.Validators
{
    public interface IUserNameExistanceValidator : IPatikaValidator<UserNameExistanceValidatorInput>
    {

    }
}
