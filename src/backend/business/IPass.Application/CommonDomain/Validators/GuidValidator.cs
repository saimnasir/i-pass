using FluentValidation;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Application.CommonDomain.Validators
{
    public class GuidValidator : AbstractValidator<GuidValidatorInput>, IGuidValidator
    {
        async Task IPatikaValidator<GuidValidatorInput>.ValidateAsync(GuidValidatorInput input)
        {
            if (input.Id == new Guid())
                throw new GuidInvalidException(input.FieldName); 

            await Task.FromResult(0);
        }
    }
}
