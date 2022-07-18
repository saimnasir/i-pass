using System;

namespace IPass.Application.Contracts.AccountDomain.Validators
{
    public interface IBirthDateValidator
    {
        void Validate(DateTime input);
    }
}
