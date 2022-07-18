using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Domain.AccountDomain.Exceptions;
using System;

namespace IPass.Application.AccountDomain.Validators
{
    public class BirthDateValidator : IBirthDateValidator
    {
        public BirthDateValidator()
        {
        }

        public void Validate(DateTime input)
        {
            if(input == DateTime.MinValue)
            {
                throw new NullReferenceException();
            }

            var today = DateTime.Today;

            var age = today.Year - input.Year;

            if (input.Date > today.AddYears(-age)) age--;

            if(age < 15 || age > 100)
            {
                throw new InappropriateAgeException();
            }
        }
    }
}
