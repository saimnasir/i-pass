using IPass.Application.Contracts.AccountDomain.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class ProfileAlreadyCompletedValidator : IProfileAlreadyCompletedValidator
    {
        public async Task ValidateAsync(bool isProfileCompleted)
        {
            if (isProfileCompleted)
            {
                throw new UserProfileAlreadyCompletedException();
            }
            await Task.FromResult(0);
        }
    }
}
